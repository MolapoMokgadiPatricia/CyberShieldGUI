using System.Collections.Generic;

namespace CyberShieldGUI
{
    // Represents a single quiz question with multiple-choice or true/false options.
    public class QuizQuestion
    {
        public string Question { get; }
        public string[] Options { get; }       // e.g. { "A) ...", "B) ...", "C) ...", "D) ..." }
        public int CorrectIndex { get; }       // 0-based index of the correct option
        public string Explanation { get; }
        public bool IsTrueFalse { get; }

        public QuizQuestion(string question, string[] options, int correctIndex,
                            string explanation, bool isTrueFalse = false)
        {
            Question = question;
            Options = options;
            CorrectIndex = correctIndex;
            Explanation = explanation;
            IsTrueFalse = isTrueFalse;
        }
    }

    // Stores all 12 cybersecurity quiz questions.
    // Questions cover phishing, passwords, malware, ransomware, 2FA, Wi-Fi, privacy, and scams.
    public static class QuizBank
    {
        public static readonly List<QuizQuestion> Questions = new List<QuizQuestion>
        {
            // Question 1 — Phishing
            new QuizQuestion(
                "What should you do if you receive an email asking for your password?",
                new[] { "A) Reply with your password",
                        "B) Delete the email",
                        "C) Report the email as phishing",
                        "D) Ignore it" },
                correctIndex: 2,
                "Reporting phishing emails helps your email provider block the attacker " +
                "and protects other users from receiving the same scam."
            ),

            // Question 2 — Passwords
            new QuizQuestion(
                "Which of the following is the strongest password?",
                new[] { "A) password123",
                        "B) Mokgadi1990",
                        "C) BlueCoffee!River27",
                        "D) qwerty" },
                correctIndex: 2,
                "A strong password uses a mix of upper and lower case letters, numbers, and symbols. " +
                "Passphrases like BlueCoffee!River27 are long, strong, and easier to remember."
            ),

            // Question 3 — 2FA (True/False)
            new QuizQuestion(
                "True or False: SMS-based two-factor authentication is the most secure form of 2FA.",
                new[] { "A) True", "B) False" },
                correctIndex: 1,
                "False. SMS 2FA is vulnerable to SIM-swap scams. Authenticator apps like " +
                "Google Authenticator are much more secure.",
                isTrueFalse: true
            ),

            // Question 4 — Malware
            new QuizQuestion(
                "Which action is most likely to infect your device with malware?",
                new[] { "A) Updating your operating system",
                        "B) Downloading software from an unofficial website",
                        "C) Using a strong password",
                        "D) Enabling two-factor authentication" },
                correctIndex: 1,
                "Downloading software from unofficial sources is one of the most common ways " +
                "malware spreads. Always use official app stores or the vendor's own website."
            ),

            // Question 5 — Ransomware
            new QuizQuestion(
                "If your files are encrypted by ransomware, what is the best course of action?",
                new[] { "A) Pay the ransom immediately",
                        "B) Disconnect from the internet and restore from a clean backup",
                        "C) Restart your computer",
                        "D) Email the attacker to negotiate" },
                correctIndex: 1,
                "Paying the ransom does not guarantee you will get your files back and funds " +
                "criminal activity. The best protection is keeping regular offline backups."
            ),

            // Question 6 — Public Wi-Fi
            new QuizQuestion(
                "Which of the following best protects your data on a public Wi-Fi network?",
                new[] { "A) Using incognito mode in your browser",
                        "B) Connecting to a reputable VPN",
                        "C) Turning off Bluetooth",
                        "D) Lowering your screen brightness" },
                correctIndex: 1,
                "A VPN encrypts all your internet traffic, making it unreadable to anyone " +
                "on the same network. Incognito mode only hides local browsing history."
            ),

            // Question 7 — POPIA (True/False)
            new QuizQuestion(
                "True or False: South Africa's POPIA Act gives citizens the right to request that organisations delete their personal data.",
                new[] { "A) True", "B) False" },
                correctIndex: 0,
                "True. The Protection of Personal Information Act (POPIA) gives South Africans " +
                "the right to access, correct, and request deletion of their personal data.",
                isTrueFalse: true
            ),

            // Question 8 — Social Engineering
            new QuizQuestion(
                "Someone calls claiming to be IT support and asks for your password. What should you do?",
                new[] { "A) Give them the password since they are IT support",
                        "B) Hang up and call IT support back on an official number",
                        "C) Give them a fake password",
                        "D) Ignore the call" },
                correctIndex: 1,
                "Legitimate IT support will never ask for your password. Always verify a caller's " +
                "identity by hanging up and calling back on a number you know is official."
            ),

            // Question 9 — Phishing email address
            new QuizQuestion(
                "Which email address is most likely to be a phishing attempt?",
                new[] { "A) support@absa.co.za",
                        "B) noreply@nedbank.co.za",
                        "C) security@amaz0n-support.net",
                        "D) hello@standardbank.co.za" },
                correctIndex: 2,
                "Phishing emails use domains that closely resemble trusted ones. Notice the '0' " +
                "instead of 'o' in 'amaz0n' and the unofficial '-support.net' domain."
            ),

            // Question 10 — Password Manager
            new QuizQuestion(
                "Why is it recommended to use a password manager?",
                new[] { "A) To use the same strong password everywhere",
                        "B) To generate and store unique passwords for every account",
                        "C) To avoid needing 2FA",
                        "D) To share passwords safely with family" },
                correctIndex: 1,
                "Password managers generate strong unique passwords for each account and store " +
                "them securely, so you do not have to remember them or reuse passwords."
            ),

            // Question 11 — Backups (True/False)
            new QuizQuestion(
                "True or False: Keeping your only backup on the same computer protects you from ransomware.",
                new[] { "A) True", "B) False" },
                correctIndex: 1,
                "False. Ransomware encrypts all files it can reach, including backups on the same " +
                "device. Keep at least one backup offline and stored separately.",
                isTrueFalse: true
            ),

            // Question 12 — Scam warning signs
            new QuizQuestion(
                "Which of the following is a classic warning sign of an online scam?",
                new[] { "A) The offer requires no action from you",
                        "B) The website has a padlock icon in the browser",
                        "C) You are told to act urgently or you will lose a prize",
                        "D) The email is sent from a well-known company name" },
                correctIndex: 2,
                "Creating urgency like 'Act now or lose your prize!' is one of the most common " +
                "scam tactics. It stops you from thinking clearly and verifying the offer."
            )
        };
    }
}
