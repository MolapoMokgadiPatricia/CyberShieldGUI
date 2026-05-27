using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberShieldGUI
{
    // Delegate that any response handler method must match.
    // Returns a string response or null if the handler does not apply.
    internal delegate string? ResponseHandler(string input);

    // Core chatbot logic. Routes user input through a delegate pipeline
    // and handles keyword recognition, sentiment detection, memory, and conversation flow.
    internal class ChatBot
    {
        // Stores the user's name for personalised responses
        private readonly string userName;

        // Remembers the topic the user expressed interest in (null until user declares one)
        private string? userInterest;

        // Tracks the most recently discussed topic for follow-up handling (null at start)
        private string? lastTopic;

        // Used for random response selection
        private readonly Random random;

        // Tracks which response index to use next per topic (sequential cycling)
        private readonly Dictionary<string, int> topicResponseIndex;

        // List of delegate handlers tried in order — first non-null result wins
        private readonly List<ResponseHandler> handlerPipeline;

        public ChatBot(string name)
        {
            userName = string.IsNullOrWhiteSpace(name) ? "User" : name;
            userInterest = null;
            lastTopic = null;
            random = new Random();
            topicResponseIndex = new Dictionary<string, int>();

            // Build the pipeline — order is important, specific handlers run before the fallback
            handlerPipeline = new List<ResponseHandler>
            {
                HandleFollowUpInput,        // handles "tell me more", "another tip", etc.
                HandleInterestInput,        // handles "I am interested in privacy", etc.
                HandleSentimentAndKeyword,  // handles emotion + cybersecurity topic
                HandleGeneralInput          // handles greetings, purpose, goodbye, fallback
            };
        }

        // Public getters used by the UI to display session info
        public string GetUserName() => userName;
        public string? GetUserInterest() => userInterest;
        public string? GetLastTopic() => lastTopic;

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
                   "What would you like to know about today?";
        }

        // Validates input then passes it through the delegate pipeline.
        // The first handler to return a non-null string provides the response.
        public string ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please type something so I can help you.";

            // Normalise input for consistent matching
            string normalised = input.ToLower().Trim();

            // Run each delegate handler in order
            foreach (ResponseHandler handler in handlerPipeline)
            {
                string? result = handler(normalised);
                if (result != null)
                    return result;
            }

            return $"I did not quite understand that, {userName}. Could you rephrase?";
        }

        // Handler 1: Checks if the user wants more information on the current topic.
        // Returns null if no follow-up phrase is detected.
        private string? HandleFollowUpInput(string input)
        {
            bool isFollowUp = ResponseBank.FollowUpPhrases.Any(phrase => input.Contains(phrase));
            if (!isFollowUp) return null;

            if (lastTopic == null)
                return $"I am not sure which topic you want more on, {userName}. " +
                       "Try asking something like 'Tell me more about phishing' or 'Another password tip'.";

            return $"Here is more on {lastTopic}:\n\n{GetTopicResponse(lastTopic)}";
        }

        // Handler 2: Checks if the user is declaring interest in a topic.
        // Stores the topic in memory and personalises future responses.
        // Returns null if no interest phrase is detected.
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
                    lastTopic = key;
                    return $"Great! I will remember that you are interested in {key}, {userName}. " +
                           $"It is an important part of staying safe online.\n\n" +
                           $"Here is your first {key} tip:\n\n{GetTopicResponse(key)}";
                }
            }

            return $"I will keep your interests in mind, {userName}. " +
                   "Could you be more specific? For example: 'I am interested in privacy'.";
        }

        // Handler 3: Detects sentiment and/or cybersecurity keywords.
        // Combines them when both are present for an empathetic and informative reply.
        // Returns null if neither is found.
        private string? HandleSentimentAndKeyword(string input)
        {
            string? sentimentResponse = DetectSentiment(input);
            string? keyword = DetectKeyword(input);
            string? keywordResponse = keyword != null ? GetTopicResponse(keyword) : null;

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

        // Handler 4: Handles greetings, purpose questions, gratitude, and goodbye.
        // Always returns a non-null string — this is the final fallback handler.
        private string HandleGeneralInput(string input)
        {
            if (input.Contains("hello") || input.Contains("hi ") ||
                input == "hi" || input.Contains("hey"))
                return $"Hello again, {userName}! What would you like to know about today? " +
                       "Ask me about passwords, phishing, privacy, malware, 2FA, Wi-Fi, or scams.";

            if (input.Contains("how are you"))
                return "I am a program designed to keep you safe online — fully operational and ready to help!";

            if (input.Contains("purpose") ||
                input.Contains("what can you") ||
                input.Contains("what can i ask") ||
                input.Contains("help me") ||
                input.Contains("what do you do"))
                return ResponseBank.PurposeResponses[random.Next(ResponseBank.PurposeResponses.Count)];

            if (input.Contains("thank") || input.Contains("thanks") || input.Contains("appreciate"))
                return $"You are welcome, {userName}! Staying informed is the first step to staying safe. " +
                       "Is there anything else I can help you with?";

            if (input.Contains("bye") || input.Contains("goodbye") ||
                input.Contains("exit") || input.Contains("quit") ||
                input.Contains("see you"))
                return $"Goodbye, {userName}! Stay safe online.";

            // If the user has a stored interest, reference it in the fallback
            if (userInterest != null)
                return $"I did not quite understand that, {userName}. Since you are interested in {userInterest}, " +
                       $"would you like another tip on that? Or ask about passwords, phishing, malware, privacy, 2FA, Wi-Fi, or scams.";

            return $"I did not quite understand that, {userName}. Could you rephrase? " +
                   "You can ask about passwords, phishing, privacy, malware, ransomware, 2FA, Wi-Fi, or scams.";
        }

        // Checks input for known sentiment words and returns the matching empathetic response.
        private string? DetectSentiment(string input)
        {
            foreach (var sentiment in ResponseBank.SentimentResponses)
            {
                if (input.Contains(sentiment.Key))
                    return sentiment.Value;
            }
            return null;
        }

        // Scans input for known cybersecurity keywords including common variations.
        // Returns the matching key from ResponseBank or null if nothing matches.
        private string? DetectKeyword(string input)
        {
            if (input.Contains("2fa") || input.Contains("two factor") ||
                input.Contains("two-factor") || input.Contains("multifactor") ||
                input.Contains("authenticator"))
            {
                lastTopic = "2fa";
                return "2fa";
            }

            if (input.Contains("wifi") || input.Contains("wi-fi") ||
                input.Contains("public network") || input.Contains("hotspot") ||
                input.Contains("public wi"))
            {
                lastTopic = "wifi";
                return "wifi";
            }

            if (input.Contains("social engineering") ||
                input.Contains("pretexting") ||
                input.Contains("manipulation"))
            {
                lastTopic = "social engineering";
                return "social engineering";
            }

            // Check ransomware before malware so it gets its own response list
            if (input.Contains("ransomware") || input.Contains("ransom"))
            {
                lastTopic = "ransomware";
                return "ransomware";
            }

            foreach (string key in ResponseBank.KeywordResponses.Keys)
            {
                if (input.Contains(key))
                {
                    lastTopic = key;
                    return key;
                }
            }

            return null;
        }

        // Returns the next response for a given topic.
        // Phishing responses are selected randomly for variety.
        // All other topics cycle sequentially so follow-ups always return new info.
        private string GetTopicResponse(string topic)
        {
            if (!ResponseBank.KeywordResponses.ContainsKey(topic))
                return $"I do not have specific information on '{topic}' yet, {userName}. " +
                       "Try asking about phishing, passwords, malware, privacy, 2FA, or Wi-Fi.";

            List<string> responses = ResponseBank.KeywordResponses[topic];
            string response;

            if (topic == "phishing")
            {
                // Random selection keeps phishing tips varied
                response = responses[random.Next(responses.Count)];
            }
            else
            {
                // Sequential cycling so each follow-up gives a new tip
                if (!topicResponseIndex.ContainsKey(topic))
                    topicResponseIndex[topic] = 0;

                int idx = topicResponseIndex[topic];
                response = responses[idx];
                topicResponseIndex[topic] = (idx + 1) % responses.Count;
            }

            // Add personalised prefix if the user declared interest in this topic
            if (userInterest == topic)
                return $"As someone interested in {topic}, here is something worth knowing:\n\n{response}";

            return response;
        }
    }
}
