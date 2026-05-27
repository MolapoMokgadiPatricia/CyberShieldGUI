using System.Collections.Generic;

namespace CyberShieldGUI
{
    // Stores all chatbot responses in one place.
    // Uses Dictionary<string, List<string>> so responses are easy to look up and extend.
    internal static class ResponseBank
    {
        // Maps each cybersecurity topic to a list of response strings.
        // The chatbot cycles or randomly selects from these lists.
        public static readonly Dictionary<string, List<string>> KeywordResponses =
            new Dictionary<string, List<string>>
        {
            {
                "password", new List<string>
                {
                    "Use long passphrases of 14 to 20 characters, mixing random words, numbers, and symbols. They are much harder to brute-force.",
                    "Never reuse passwords across different sites. If one site is breached, attackers will try those same credentials everywhere else.",
                    "Use a trusted password manager like Bitwarden or 1Password to generate and store unique passwords for every account.",
                    "Avoid personal details like your birthday or ID number in passwords — these are the first things attackers try.",
                    "Change passwords immediately if you suspect a breach, and enable breach alerts through services like Have I Been Pwned."
                }
            },
            {
                "phishing", new List<string>
                {
                    "Be cautious of emails asking for personal information. Legitimate companies will never ask for your password via email.",
                    "Always hover over links before clicking to check the real destination URL — it can differ completely from the display text.",
                    "Watch for urgent language like 'Your account will be closed!' — this is designed to make you act without thinking.",
                    "Never download unexpected email attachments, even from people you know. Their account may have been compromised.",
                    "Check the sender's email address carefully — attackers use addresses almost identical to real ones, such as support@amaz0n.com."
                }
            },
            {
                "scam", new List<string>
                {
                    "If an offer seems too good to be true, it almost certainly is. Verify through official channels before acting.",
                    "Never send money or share personal details with someone you have not independently verified.",
                    "Be cautious of calls or messages claiming to be from your bank or SARS — hang up and call the official number yourself.",
                    "Scammers create urgency to stop you from thinking clearly. Always pause and verify before responding.",
                    "Report scams to the South African Fraud Prevention Service (SAFPS) at 0860 101 248 or safps.org.za."
                }
            },
            {
                "privacy", new List<string>
                {
                    "Review your social media privacy settings regularly — even harmless details can be combined to steal your identity.",
                    "South Africa's POPIA gives you the right to request that organisations delete or stop using your personal data.",
                    "Audit which apps have access to your location, camera, and microphone — revoke permissions that are not necessary.",
                    "Use private browsing or a VPN when you do not want websites or your ISP tracking your activity.",
                    "Only provide information that is strictly required on online forms and question why it is being asked."
                }
            },
            {
                "malware", new List<string>
                {
                    "Keep your antivirus and operating system updated — most malware exploits known vulnerabilities that patches already fix.",
                    "Never open email attachments from unknown senders — even a harmless-looking PDF can contain hidden executable code.",
                    "Protect yourself from ransomware with the 3-2-1 backup rule: 3 copies, on 2 different media, with 1 stored offsite.",
                    "Only install software from official sources like the Microsoft Store, Google Play, or the vendor's own website.",
                    "Sudden slowdowns, overheating, or pop-ups may indicate infection — run a full antivirus scan immediately."
                }
            },
            {
                "2fa", new List<string>
                {
                    "Two-factor authentication adds a critical second layer of security. Even if your password is stolen, attackers still cannot get in.",
                    "Use an authenticator app like Google Authenticator or Authy instead of SMS-based 2FA, which is vulnerable to SIM-swapping.",
                    "Enable 2FA on your most important accounts first: email, banking, and social media.",
                    "Never share a one-time PIN with anyone — no bank or legitimate service will ever ask for it over the phone."
                }
            },
            {
                "wifi", new List<string>
                {
                    "Avoid banking or accessing sensitive accounts on public Wi-Fi. These networks are often unsecured and easy to monitor.",
                    "Attackers set up fake hotspots with names similar to real ones. Always verify the correct network name with staff.",
                    "Use a VPN on public networks to encrypt your traffic and prevent eavesdropping.",
                    "Disable the auto-connect feature on your device to prevent it from silently joining malicious hotspots."
                }
            },
            {
                "vpn", new List<string>
                {
                    "A VPN encrypts your internet traffic, making it very difficult for attackers or ISPs to read your data.",
                    "Choose a reputable paid VPN provider — free VPNs often fund themselves by logging and selling your browsing data.",
                    "A VPN is especially important on public Wi-Fi in coffee shops, hotels, or airports.",
                    "A VPN masks your IP address and helps prevent tracking by advertisers and data brokers."
                }
            },
            {
                "ransomware", new List<string>
                {
                    "Ransomware encrypts your files and demands payment. Never pay — it does not guarantee you will get your files back.",
                    "The best defence is regular offline backups stored on a drive that is not always connected to your computer.",
                    "Ransomware commonly spreads through phishing emails and malicious downloads — be cautious about what you click.",
                    "If infected, immediately disconnect from the internet to prevent the ransomware from spreading to other devices."
                }
            },
            {
                "social engineering", new List<string>
                {
                    "Social engineering manipulates people rather than systems. Attackers exploit trust, fear, and urgency.",
                    "Always verify the identity of anyone requesting sensitive information, even if they claim to be IT support or your bank.",
                    "Pretexting is when an attacker creates a believable story to gain your trust. Trust your instincts and verify independently.",
                    "Pause before acting on any unusual request, even from a trusted source — their account may have been compromised."
                }
            }
        };

        // Maps sentiment words to empathetic response strings.
        public static readonly Dictionary<string, string> SentimentResponses =
            new Dictionary<string, string>
        {
            { "worried",     "It is completely understandable to feel worried — cyber threats are real and growing. Let me share some practical tips to help you feel more in control." },
            { "scared",      "There is no need to be scared — knowledge is your best defence online. Let me walk you through what to watch out for." },
            { "frustrated",  "I understand — cybersecurity can feel overwhelming. Let us break it down step by step to make it more manageable." },
            { "confused",    "No worries. Cybersecurity has a lot of jargon. Let me explain things in plain, simple language." },
            { "curious",     "Staying curious and informed is one of the most effective ways to protect yourself online." },
            { "overwhelmed", "It is okay to feel overwhelmed. Let us focus on one topic at a time." },
            { "angry",       "Being targeted by cyber threats is genuinely frustrating. Let me help you take back control." },
            { "unsure",      "It is fine to be unsure — that is exactly why I am here. Let me guide you through what you need to know." },
            { "nervous",     "Feeling nervous about online safety is a healthy instinct. Let me turn that into some practical steps." }
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
            "I am CyberShield, your personal cybersecurity awareness assistant. I am here to educate you on online threats and help you stay safe.",
            "My purpose is to help South Africans understand and avoid cyber threats such as phishing, malware, scams, and identity theft.",
            "I cover password safety, phishing, privacy, malware, ransomware, 2FA, Wi-Fi, VPNs, and online scams. What would you like to explore?"
        };
    }
}