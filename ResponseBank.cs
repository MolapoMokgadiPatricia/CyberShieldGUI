using System.Collections.Generic;

namespace CyberShieldGUI
{
    // Stores all chatbot responses in one place.
    // Responses are short, simple, and beginner-friendly.
    internal static class ResponseBank
    {
        // Maps each cybersecurity topic to a list of response strings.
        // The chatbot randomly selects from these lists to keep answers varied.
        public static readonly Dictionary<string, List<string>> KeywordResponses =
            new Dictionary<string, List<string>>
        {
            {
               
                "password", new List<string>
                {
                    "Password security is one of the most important parts of staying safe online. Use a strong password with at least 12 characters, including capital letters, small letters, numbers, and symbols.",

                    "Protecting your online accounts starts with using unique passwords. Avoid using the same password for email, banking, and social media accounts because one hacked account can put others at risk.",

                    "A password manager is a useful cybersecurity tool that safely stores your passwords and creates strong passwords for you. This makes online security easier and more convenient.",

                    "If you think someone may know your password, change it immediately and enable two-factor authentication for extra protection. Acting quickly can help prevent unauthorized access.",

                    "Strong passwords should be difficult for attackers to guess but easy for you to remember. A passphrase like BlueCoffee!River27 is much safer than simple passwords like Mokgadi123."
                }

            },
            {
               
                "phishing", new List<string>
                {
                    "Phishing is one of the most common cyber threats today. Scammers use fake emails, SMSes, or websites to trick people into sharing passwords, banking details, or personal information.",

                    "Staying cautious online can help protect you from phishing attacks. Avoid clicking links in suspicious messages and rather visit the official website directly yourself.",

                    "Phishing messages often create panic or urgency by saying things like 'your account will be blocked'. Take a moment to stop, think, and verify the message before responding.",

                    "Cybercriminals often use email addresses that look almost identical to real company addresses. Always check for small spelling changes or unusual characters before trusting a sender.",

                    "Unexpected attachments can sometimes contain harmful malware hidden inside files like invoices, CVs, or proof of payment documents. Only open attachments from trusted sources."
                }

            },
            {
                "scam", new List<string>
                {
                    "A scam is a trick used to steal money or personal information. Be careful with messages promising prizes, jobs, loans, or quick money.",
                    "If an offer sounds too good to be true, it is probably a scam. Always verify before sending money or sharing personal details.",
                    "Never share your banking PIN, password, or one-time PIN with anyone. Real banks and companies will not ask for these details.",
                    "Scammers often rush you so you do not think clearly. Take your time and ask someone you trust before making a payment.",
                    "If someone claims to be from your bank, hang up and call the official bank number yourself. Do not use the number they gave you."
                }
            },
            {
                "privacy", new List<string>
                {
                    "Privacy means controlling who can see your personal information. Do not share your ID number, address, or banking details unless necessary.",
                    "Check your social media privacy settings. Avoid posting too much personal information, such as your location, school, home address, or daily routine.",
                    "Only give apps the permissions they need. For example, a calculator app should not need access to your camera, microphone, or location.",
                    "Use strong privacy settings on accounts like Facebook, Instagram, WhatsApp, and Google. This helps reduce identity theft risks.",
                    "Before filling in online forms, ask yourself why the information is needed and whether the website is trustworthy."
                }
            },
            {
                "malware", new List<string>
                {
                    "Malware is harmful software that can damage your device or steal information. Avoid downloading files or apps from unknown websites.",
                    "Keep your phone, computer, and antivirus updated. Updates fix security weaknesses that attackers may try to use.",
                    "Do not click suspicious links or open unknown attachments. Malware can hide inside files that look normal.",
                    "Only install apps from trusted sources like Google Play, the App Store, Microsoft Store, or the official company website.",
                    "Signs of malware include slow performance, strange pop-ups, unknown apps, overheating, or data disappearing quickly."
                }
            },
            {
                "2fa", new List<string>
                {
                    "Two-factor authentication, or 2FA, adds extra protection to your account. It asks for another step after your password, such as a code.",
                    "Turn on 2FA for important accounts like email, banking, and social media. This helps protect you even if your password is stolen.",
                    "Never share a one-time PIN or verification code with anyone. Scammers use these codes to access your accounts.",
                    "Authenticator apps are usually safer than SMS codes because SMS can be targeted through SIM-swap scams."
                }
            },
            {
                "wifi", new List<string>
                {
                    "Be careful on public Wi-Fi at malls, taxis, schools, hotels, and restaurants. Other people on the network may try to view your activity.",
                    "Avoid online banking or entering passwords while using public Wi-Fi. Use mobile data instead when dealing with sensitive information.",
                    "Check the correct Wi-Fi name before connecting. Scammers can create fake hotspots with names that look official.",
                    "Turn off auto-connect so your phone does not automatically join unsafe Wi-Fi networks."
                }
            },
            {
                "vpn", new List<string>
                {
                    "A VPN helps protect your internet connection by encrypting your data. This is useful when using public Wi-Fi.",
                    "A VPN can make it harder for others to see what you are doing online, especially on shared networks.",
                    "Use a trusted VPN provider. Some free VPNs may collect your browsing information and sell it.",
                    "A VPN is helpful, but it does not protect you from every threat. You must still avoid scams, fake links, and weak passwords."
                }
            },
            {
                "ransomware", new List<string>
                {
                    "Ransomware locks or encrypts your files and demands money to unlock them. Paying does not guarantee that your files will return.",
                    "The best protection against ransomware is regular backups. Keep a copy of important files on an external drive or cloud storage.",
                    "Ransomware often spreads through phishing emails, fake downloads, and infected attachments. Be careful with what you open.",
                    "If you suspect ransomware, disconnect from the internet immediately and do not plug in backup drives until the device is checked."
                }
            },
            {
                "social engineering", new List<string>
                {
                    "Social engineering is when attackers trick people instead of hacking systems. They use fear, trust, pressure, or fake stories.",
                    "Always verify someone's identity before sharing private information. This includes people claiming to be from your bank, school, or IT support.",
                    "If a request feels strange or urgent, pause and confirm it through another trusted method before taking action.",
                    "Attackers may pretend to be someone you know. If a message asks for money or codes, call the person first to confirm."
                }
            }
        };

        // Maps sentiment words to empathetic response strings.
        public static readonly Dictionary<string, string> SentimentResponses =
            new Dictionary<string, string>
        {
            { "worried",     "It is understandable to feel worried. Let me give you a simple safety tip that can help." },
            { "scared",      "You do not need to panic. I will explain this in a simple way and give you a practical step." },
            { "frustrated",  "I understand. Cybersecurity can feel confusing, but we can take it one step at a time." },
            { "confused",    "No problem. I will explain it in plain language without technical words." },
            { "curious",     "That is a good attitude. Learning about cybersecurity helps you stay safer online." },
            { "overwhelmed", "That is okay. Let us focus on one simple safety step at a time." },
            { "angry",       "That can be frustrating. Let me help you understand what to do next." },
            { "unsure",      "It is okay to be unsure. I will guide you with a simple explanation." },
            { "nervous",     "Feeling nervous is normal. Let me share a clear and practical tip." }
        };

        // Phrases that mean the user wants more information on the current topic.
        public static readonly List<string> FollowUpPhrases = new List<string>
        {
            "tell me more", "more info", "more information", "explain more",
            "elaborate", "another tip", "give me another", "one more",
            "continue", "go on", "what else", "keep going",
            "explain further", "another one", "more please",
            "say more", "expand on that"
        };

        // General purpose/capability responses selected randomly.
        public static readonly List<string> PurposeResponses = new List<string>
        {
            "I am CyberShield, a cybersecurity awareness chatbot. I help you learn how to stay safe online.",
            "I can explain common online threats like scams, phishing, weak passwords, malware, and privacy risks.",
            "You can ask me about password safety, phishing, privacy, scams, malware, Wi-Fi, VPNs, ransomware, or 2FA."
        };
    }
}
