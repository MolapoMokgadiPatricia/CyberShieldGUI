using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberShieldGUI
{
    // Delegate that any response handler method must match.
    // Returns a string response or null if the handler does not apply.
    internal delegate string? ResponseHandler(string input);

    // Core chatbot logic for Parts 1, 2, and 3.
    // Routes user input through a delegate pipeline.
    // Part 3 adds a new handler at the front of the pipeline for tasks, quiz, and activity log.
    internal class ChatBot
    {
        // Stores the user's name for personalised responses
        private readonly string userName;

        // Remembers the topic the user expressed interest in (null until declared)
        private string? userInterest;

        // Tracks the most recently discussed topic for follow-up handling
        private string? lastTopic;

        // Used for random response selection
        private readonly Random random;

        // Tracks which response index to use next per topic (sequential cycling)
        private readonly Dictionary<string, int> topicResponseIndex;

        // List of delegate handlers tried in order — first non-null result wins
        private readonly List<ResponseHandler> handlerPipeline;

        // Part 3 services
        private readonly ActivityLog  activityLog;
        private readonly TaskManager  taskManager;
        private readonly QuizManager  quizManager;
        private readonly NlpProcessor nlp;

        // Tracks whether the bot is waiting for a yes/no reminder response after adding a task
        private bool    awaitingReminderResponse;
        private string? lastAddedTaskTitle;

        public ChatBot(string name)
        {
            userName           = string.IsNullOrWhiteSpace(name) ? "User" : name;
            userInterest       = null;
            lastTopic          = null;
            random             = new Random();
            topicResponseIndex = new Dictionary<string, int>();

            // Initialise Part 3 services
            activityLog = new ActivityLog();
            taskManager = new TaskManager(activityLog);
            quizManager = new QuizManager(activityLog);
            nlp         = new NlpProcessor();

            // Build the pipeline — Part 3 handler runs first, then Part 2 handlers
            handlerPipeline = new List<ResponseHandler>
            {
                HandlePart3Input,           // NEW: task/quiz/log NLP routing
                HandleFollowUpInput,        // handles "tell me more", "another tip", etc.
                HandleInterestInput,        // handles "I am interested in privacy", etc.
                HandleSentimentAndKeyword,  // handles emotion + cybersecurity topic
                HandleGeneralInput          // handles greetings, purpose, goodbye, fallback
            };
        }

        // Public getters used by the UI
        public string      GetUserName()     => userName;
        public string?     GetUserInterest() => userInterest;
        public string?     GetLastTopic()    => lastTopic;
        public ActivityLog GetActivityLog()  => activityLog;
        public TaskManager GetTaskManager()  => taskManager;

        // Returns the welcome message shown at the start of a session
        public string GetWelcomeMessage()
        {
            return $"Hello, {userName}! I am CyberShield, your personal cybersecurity assistant.\n\n" +
                   "I can help you with the following topics:\n" +
                   "  - Password safety\n" +
                   "  - Phishing scams\n" +
                   "  - Online privacy\n" +
                   "  - Malware and ransomware\n" +
                   "  - Two-factor authentication (2FA)\n" +
                   "  - Wi-Fi and VPN safety\n" +
                   "  - Online scams\n\n" +
                   "  - Add tasks: type 'add task: Enable 2FA'\n" +
                   "  - Take a quiz: type 'start quiz'\n" +
                   "  - View activity log: type 'show activity log'\n\n" +
                   "What would you like to do today?";
        }

        // Validates input then passes it through the delegate pipeline.
        public string ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please type something so I can help you.";

            string normalised = input.ToLower().Trim();

            foreach (ResponseHandler handler in handlerPipeline)
            {
                string? result = handler(normalised);
                if (result != null)
                    return result;
            }

            return $"I did not quite understand that, {userName}. Could you rephrase?\n\n" +
                   "Topics: passwords, phishing, privacy, malware, ransomware, 2FA, Wi-Fi, scams.\n" +
                   "Part 3: 'add task', 'show tasks', 'start quiz', 'show activity log'.";
        }

        // ── PART 3 HANDLER ────────────────────────────────────────────────────────
        private string? HandlePart3Input(string input)
        {
            // ── Waiting for yes/no reminder reply after adding a task ─────────────
            if (awaitingReminderResponse)
            {
                awaitingReminderResponse = false;

                if (input.Contains("yes") || input.Contains("sure") ||
                    input.Contains("remind") || input.Contains("ok"))
                {
                    return $"How many days from today would you like the reminder?\n" +
                           $"Type: 'set reminder task {taskManager.Tasks.Count} in 7 days'";
                }
                else if (input.Contains("no") || input.Contains("nope") ||
                         input.Contains("not now"))
                {
                    return $"No problem! Task '{lastAddedTaskTitle}' has been saved without a reminder.\n" +
                           "Type 'show tasks' to see your task list.";
                }
            }

            // ── Active quiz ───────────────────────────────────────────────────────
            if (quizManager.IsActive && quizManager.AwaitingAnswer)
            {
                // FIX: Allow user to exit the quiz mid-way
                if (input.Contains("stop quiz")  || input.Contains("quit quiz") ||
                    input.Contains("exit quiz")   || input.Contains("end quiz"))
                {
                    int scoreSoFar = quizManager.LastScore;
                    int answered   = quizManager.QuestionNumber - 1;
                    quizManager.ForceEnd();
                    activityLog.Add($"Quiz exited early — {scoreSoFar}/{answered} correct");
                    return $"Quiz ended early.\n\n" +
                           $"You answered {answered} question(s) and got {scoreSoFar} correct.\n\n" +
                           "Type 'start quiz' to play again whenever you are ready!";
                }

                // Accept A/B/C/D answer
                var quizIntent = nlp.Classify(input);

                if (quizIntent == NlpProcessor.Intent.AnswerQuiz)
                {
                    activityLog.Add($"Quiz — answered question {quizManager.QuestionNumber}");
                    return quizManager.SubmitAnswer(nlp.QuizAnswerIndex);
                }

                // Any other input during quiz — remind the user how to answer or exit
                return "You are in the middle of a quiz!\n\n"
                    + $"{quizManager.CurrentQuestion.Question}\n\n"
                    + "Type A, B, C, or D to answer  —  or type 'stop quiz' to exit.";
            }

            // ── Classify all other Part 3 input ──────────────────────────────────
            NlpProcessor.Intent intent = nlp.Classify(input);

            switch (intent)
            {
                case NlpProcessor.Intent.AddTask:
                {
                    lastAddedTaskTitle       = nlp.TaskTitle;
                    string msg               = taskManager.AddTask(nlp.TaskTitle, nlp.TaskDescription);
                    awaitingReminderResponse = true;
                    activityLog.Add($"NLP recognised 'add task' → '{nlp.TaskTitle}'");
                    return msg;
                }

                case NlpProcessor.Intent.ShowTasks:
                    activityLog.Add("Viewed task list");
                    return taskManager.GetTaskListMessage();

                case NlpProcessor.Intent.CompleteTask:
                    activityLog.Add($"NLP recognised 'complete task {nlp.TaskPosition}'");
                    return taskManager.CompleteTask(nlp.TaskPosition);

                case NlpProcessor.Intent.DeleteTask:
                    activityLog.Add($"NLP recognised 'delete task {nlp.TaskPosition}'");
                    return taskManager.DeleteTask(nlp.TaskPosition);

                case NlpProcessor.Intent.SetReminder:
                {
                    int      days         = nlp.ReminderDays > 0 ? nlp.ReminderDays : 1;
                    DateTime reminderDate = DateTime.Today.AddDays(days);
                    int      pos          = nlp.TaskPosition == -1
                                               ? taskManager.Tasks.Count
                                               : nlp.TaskPosition;
                    activityLog.Add($"NLP recognised 'set reminder' — {days} day(s)");
                    return taskManager.SetReminder(pos, reminderDate);
                }

                case NlpProcessor.Intent.StartQuiz:
                    return quizManager.Start();

                case NlpProcessor.Intent.ShowActivityLog:
                    activityLog.Add("Viewed activity log");
                    return activityLog.GetSummary(showAll: false);

                case NlpProcessor.Intent.ShowFullLog:
                    activityLog.Add("Viewed full activity log");
                    return activityLog.GetSummary(showAll: true);

                default:
                    return null;
            }
        }

        // ── PART 2 HANDLERS ───────────────────────────────────────────────────────

        private string? HandleFollowUpInput(string input)
        {
            bool isFollowUp = ResponseBank.FollowUpPhrases.Any(phrase => input.Contains(phrase));
            if (!isFollowUp) return null;

            if (lastTopic == null)
                return $"I am not sure which topic you want more on, {userName}. " +
                       "Try asking something like 'Tell me more about phishing' or 'Another password tip'.";

            return $"Here is more on {lastTopic}:\n\n{GetTopicResponse(lastTopic)}";
        }

        private string? HandleInterestInput(string input)
        {
            bool isInterest = input.Contains("interested in") ||
                              input.Contains("i like") ||
                              input.Contains("i want to know about") ||
                              input.Contains("i'm curious about");

            if (!isInterest) return null;

            foreach (string key in ResponseBank.KeywordResponses.Keys)
            {
                if (input.Contains(key))
                {
                    userInterest = key;
                    lastTopic    = key;
                    return $"Great! I will remember that you are interested in {key}, {userName}. " +
                           $"It is an important part of staying safe online.\n\n" +
                           $"Here is your first {key} tip:\n\n{GetTopicResponse(key)}";
                }
            }

            return $"I will keep your interests in mind, {userName}. " +
                   "Could you be more specific? For example: 'I am interested in privacy'.";
        }

        private string? HandleSentimentAndKeyword(string input)
        {
            string? sentimentResponse = DetectSentiment(input);
            string? keyword           = DetectKeyword(input);
            string? keywordResponse   = keyword != null ? GetTopicResponse(keyword) : null;

            if (sentimentResponse != null && keywordResponse != null)
                return $"{sentimentResponse}\n\n{keywordResponse}";

            if (sentimentResponse != null)
            {
                if (lastTopic != null)
                    return $"{sentimentResponse}\n\n" +
                           $"Since we were discussing {lastTopic}, here is something useful:\n\n" +
                           $"{GetTopicResponse(lastTopic)}";

                return $"{sentimentResponse}\n\nWhat cybersecurity topic can I help you with, {userName}?";
            }

            if (keywordResponse != null)
                return keywordResponse;

            return null;
        }

        private string HandleGeneralInput(string input)
        {
            if (input.Contains("hello") || input.Contains("hi ") ||
                input == "hi" || input.Contains("hey"))
                return $"Hello again, {userName}! What would you like to know about today? " +
                       "Ask me about passwords, phishing, privacy, malware, 2FA, Wi-Fi, or scams.\n\n" +
                       "Or try: 'add task', 'start quiz', or 'show activity log'.";

            if (input.Contains("how are you"))
                return "I am a program designed to keep you safe online — fully operational and ready to help!";

            if (input.Contains("purpose")        || input.Contains("what can you") ||
                input.Contains("what can i ask") || input.Contains("help me")      ||
                input.Contains("what do you do"))
                return ResponseBank.PurposeResponses[random.Next(ResponseBank.PurposeResponses.Count)];

            if (input.Contains("thank") || input.Contains("thanks") || input.Contains("appreciate"))
                return $"You are welcome, {userName}! Staying informed is the first step to staying safe. " +
                       "Is there anything else I can help you with?";

            if (input.Contains("bye")  || input.Contains("goodbye") ||
                input.Contains("exit") || input.Contains("quit")    ||
                input.Contains("see you"))
                return $"Goodbye, {userName}! Stay safe online.";

            if (userInterest != null)
                return $"I did not quite understand that, {userName}. Since you are interested in {userInterest}, " +
                       $"would you like another tip on that?\n\n" +
                       "Or try: 'add task', 'start quiz', or 'show activity log'.";

            return $"I did not quite understand that, {userName}. Could you rephrase?\n\n" +
                   "Topics: passwords, phishing, privacy, malware, ransomware, 2FA, Wi-Fi, scams.\n" +
                   "Part 3: 'add task', 'show tasks', 'start quiz', 'show activity log'.";
        }

        // ── Helpers ───────────────────────────────────────────────────────────────

        private string? DetectSentiment(string input)
        {
            foreach (var sentiment in ResponseBank.SentimentResponses)
                if (input.Contains(sentiment.Key)) return sentiment.Value;
            return null;
        }

        private string? DetectKeyword(string input)
        {
            if (input.Contains("2fa")   || input.Contains("two factor")  ||
                input.Contains("two-factor") || input.Contains("multifactor") ||
                input.Contains("authenticator"))
            { lastTopic = "2fa"; return "2fa"; }

            if (input.Contains("wifi") || input.Contains("wi-fi") ||
                input.Contains("public network") || input.Contains("hotspot") ||
                input.Contains("public wi"))
            { lastTopic = "wifi"; return "wifi"; }

            if (input.Contains("social engineering") ||
                input.Contains("pretexting") || input.Contains("manipulation"))
            { lastTopic = "social engineering"; return "social engineering"; }

            if (input.Contains("ransomware") || input.Contains("ransom"))
            { lastTopic = "ransomware"; return "ransomware"; }

            foreach (string key in ResponseBank.KeywordResponses.Keys)
            {
                if (input.Contains(key))
                { lastTopic = key; return key; }
            }

            return null;
        }

        private string GetTopicResponse(string topic)
        {
            if (!ResponseBank.KeywordResponses.ContainsKey(topic))
                return $"I do not have specific information on '{topic}' yet, {userName}. " +
                       "Try asking about phishing, passwords, malware, privacy, 2FA, or Wi-Fi.";

            List<string> responses = ResponseBank.KeywordResponses[topic];
            string response;

            if (topic == "phishing")
            {
                response = responses[random.Next(responses.Count)];
            }
            else
            {
                if (!topicResponseIndex.ContainsKey(topic))
                    topicResponseIndex[topic] = 0;

                int idx = topicResponseIndex[topic];
                response = responses[idx];
                topicResponseIndex[topic] = (idx + 1) % responses.Count;
            }

            if (userInterest == topic)
                return $"As someone interested in {topic}, here is something worth knowing:\n\n{response}";

            return response;
        }
    }
}
