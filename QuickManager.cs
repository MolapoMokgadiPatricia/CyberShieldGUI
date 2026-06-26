using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyberShieldGUI
{
    // Manages a single quiz session.
    // Shuffles questions, tracks the current question, accepts answers,
    // gives immediate feedback, and produces a final score summary.
    public class QuizManager
    {
        private readonly List<QuizQuestion> shuffledQuestions;
        private readonly ActivityLog activityLog;
        private int currentIndex;
        private int score;

        // True while a quiz is in progress
        public bool IsActive { get; private set; }

        // True when the bot is waiting for the user to answer the current question
        public bool AwaitingAnswer { get; private set; }

        // The current question being asked
        public QuizQuestion CurrentQuestion
            => IsActive && currentIndex < shuffledQuestions.Count
               ? shuffledQuestions[currentIndex]
               : null;

        public int TotalQuestions => shuffledQuestions.Count;
        public int QuestionNumber => currentIndex + 1;

        public int LastScore => score;

        public QuizManager(ActivityLog log)
        {
            activityLog = log;

            // Shuffle the questions so each quiz session is different
            shuffledQuestions = QuizBank.Questions
                .OrderBy(_ => Guid.NewGuid())
                .ToList();
        }

        // Starts the quiz and returns the first question as a formatted string.
        public string Start()
        {
            IsActive = true;
            AwaitingAnswer = true;
            currentIndex = 0;
            score = 0;

            activityLog.Add("Quiz started");
            return FormatCurrentQuestion();
        }

        // Accepts a 0-based answer index (A=0, B=1, C=2, D=3).
        // Returns feedback and either the next question or the final score.
        public string SubmitAnswer(int answerIndex)
        {
            if (!IsActive || !AwaitingAnswer)
                return "No quiz is currently active. Type 'start quiz' to begin.";

            QuizQuestion q = shuffledQuestions[currentIndex];
            bool correct = answerIndex == q.CorrectIndex;

            if (correct) score++;

            string feedback = correct
                ? $"Correct!\n\nExplanation: {q.Explanation}"
                : $"Incorrect. The correct answer was: {q.Options[q.CorrectIndex]}\n\nExplanation: {q.Explanation}";

            currentIndex++;

            // Quiz is finished
            if (currentIndex >= shuffledQuestions.Count)
            {
                IsActive = false;
                AwaitingAnswer = false;
                activityLog.Add($"Quiz completed — score: {score}/{shuffledQuestions.Count}");
                return $"{feedback}\n\n{BuildFinalScore()}";
            }

            AwaitingAnswer = true;
            return $"{feedback}\n\n──────────────────────────\n\n{FormatCurrentQuestion()}";
        }

        // Formats the current question with its options for display in the chat.
        private string FormatCurrentQuestion()
        {
            QuizQuestion q = shuffledQuestions[currentIndex];
            var sb = new StringBuilder();

            sb.AppendLine($"CYBERSECURITY QUIZ  —  Question {currentIndex + 1} of {shuffledQuestions.Count}");
            sb.AppendLine($"Score so far: {score}/{currentIndex}\n");
            sb.AppendLine($"{q.Question}\n");

            foreach (string option in q.Options)
                sb.AppendLine($"   {option}");

            sb.Append(q.IsTrueFalse
                ? "\nType A for True or B for False."
                : "\nType A, B, C, or D to answer.");

            return sb.ToString();
        }

        // Builds the final score message shown at the end of the quiz.
        private string BuildFinalScore()
        {
            int total = shuffledQuestions.Count;
            double percent = (double)score / total * 100;

            string verdict;
            if (percent >= 80)
                verdict = "Outstanding! You are a cybersecurity pro!";
            else if (percent >= 60)
                verdict = "Well done! You have a solid understanding of the basics.";
            else if (percent >= 40)
                verdict = "Good effort! Keep learning to improve your cybersecurity knowledge.";
            else
                verdict = "Keep learning — the more you know, the safer you are online!";

            return $"QUIZ COMPLETE!\n\n" +
                   $"Your final score: {score} out of {total}  ({percent:F0}%)\n\n" +
                   $"{verdict}\n\n" +
                   "Type 'start quiz' to play again, or ask me any cybersecurity question!";
        }

        public void ForceEnd()
        {
            IsActive = false;
            AwaitingAnswer = false;
        }
    }
}