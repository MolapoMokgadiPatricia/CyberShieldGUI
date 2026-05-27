using System.Collections.Generic;

namespace CyberShieldGUI
{
    // Stores all chatbot responses in one place.
    // These responses are written in simple, beginner-friendly language.
    // Each answer explains the topic, gives an everyday example, and gives practical steps.
    internal static class ResponseBank
    {
        public static readonly Dictionary<string, List<string>> KeywordResponses =
            new Dictionary<string, List<string>>
        {
            {
                "password", new List<string>
                {
                    @"A strong password is like a strong lock on your online account.

A weak password is something easy to guess, such as your name, birthday, phone number, or the word ""password"". A strong password should be long and difficult for another person or computer to guess.

Beginner-friendly example:
Instead of using: Mokgadi123
Use something longer like: BlueSky!Coffee27River

What you should do:
1. Use at least 12 to 16 characters.
2. Mix capital letters, small letters, numbers, and symbols.
3. Do not use your name, birthday, school name, or phone number.
4. Do not use the same password for every account.
5. Write passwords down only if they are stored safely, not left openly on a desk.

Why this matters:
If someone guesses your password, they may read your private messages, steal your photos, access your banking details, or pretend to be you online.",

                    @"Never reuse the same password on many websites.

This means you should not use one password for Facebook, Gmail, banking, school portals, and shopping websites. If one website gets hacked, criminals may try the same password on your other accounts.

Beginner-friendly example:
If your Instagram password is stolen and you use the same password for your email, the criminal may also enter your email account.

What you should do:
1. Use a different password for every important account.
2. Make your email password especially strong because email is used to reset other passwords.
3. Change passwords immediately if you think someone else knows them.
4. Turn on two-factor authentication where possible.

Simple rule:
One account = one unique password.",

                    @"A password manager can help you remember passwords safely.

A password manager is an app that stores your passwords in a protected digital vault. You only need to remember one strong master password. The app can create long, unique passwords for your other accounts.

Beginner-friendly example:
Instead of trying to remember 20 passwords, you remember one master password and the password manager remembers the rest.

What you should do:
1. Choose a trusted password manager.
2. Create one strong master password.
3. Do not share the master password with anyone.
4. Use the password manager to generate strong passwords.
5. Keep your phone or computer locked.

Important note:
Do not store passwords in plain notes, WhatsApp messages, or screenshots because someone who gets your phone may find them easily.",

                    @"Avoid passwords based on personal information.

Attackers often try information that is easy to find about you, such as your birthday, nickname, school name, pet name, favourite team, or phone number.

Beginner-friendly example:
If your birthday is public on social media, a password like Mokgadi2004 is not safe because it can be guessed.

What you should do:
1. Avoid names and dates.
2. Avoid simple patterns like 123456, qwerty, or abc123.
3. Use a phrase made from random words.
4. Add numbers and symbols in places that are not obvious.
5. Update passwords that are too simple.

Simple test:
If someone who knows you could guess it, it is not strong enough.",

                    @"Change your password quickly if you suspect a problem.

You may need to change your password if your account sends messages you did not write, your friends receive strange links from you, your login history shows unknown devices, or you receive password reset emails you did not request.

What you should do:
1. Change the password immediately.
2. Log out of all devices if the app allows it.
3. Turn on two-factor authentication.
4. Check account recovery email and phone number.
5. Warn friends not to click suspicious links from your account.

Why this matters:
Acting quickly can stop the attacker before they steal more information or use your account to scam other people."
                }
            },
            {
                "phishing", new List<string>
                {
                    @"Phishing is when a criminal tricks you into giving away private information.

They may send an email, SMS, WhatsApp message, or social media message that looks real. The message may pretend to be from your bank, school, delivery company, SARS, or a job recruiter.

Beginner-friendly example:
You receive a message saying: ""Your bank account will be blocked. Click here to confirm your details."" The link takes you to a fake website that steals your login details.

Warning signs:
1. The message creates fear or urgency.
2. It asks for your password, PIN, or one-time password.
3. The spelling or grammar looks strange.
4. The link looks unusual.
5. The sender address is slightly different from the real company.

Safe action:
Do not click the link. Open the official website yourself or call the company using the official number.",

                    @"Always check links before clicking them.

A link can look safe but send you to a dangerous website. Criminals often make fake websites that look almost identical to real ones.

Beginner-friendly example:
The message may show: www.mybank.co.za
But the actual link may go to: www.mybank-security-login.com

What you should do:
1. Move your mouse over the link before clicking if you are on a computer.
2. On a phone, press and hold the link to preview it.
3. Check for spelling mistakes in the website address.
4. Do not trust shortened links from unknown people.
5. Type the official website address yourself when dealing with banking or personal data.

Simple rule:
When in doubt, do not click.",

                    @"Be careful with messages that use pressure or threats.

Phishing messages often try to make you panic so that you act quickly without thinking. They may say your account will be closed, your parcel is stuck, your bank card is blocked, or you won a prize.

Why attackers do this:
When people are scared or excited, they are more likely to click links and share information.

What you should do:
1. Stop and breathe before responding.
2. Ask yourself: ""Was I expecting this message?""
3. Check the sender carefully.
4. Contact the organisation directly using official details.
5. Delete or report the suspicious message.

Important:
Real companies do not normally ask for passwords, PINs, or OTPs through email or SMS.",

                    @"Do not open unexpected attachments.

Attachments can hide malware, even if they look like normal documents. A file may pretend to be an invoice, CV, assignment, proof of payment, or delivery note.

Beginner-friendly example:
You receive an email saying ""Please see attached proof of payment"", but you were not expecting any payment. Opening the file could infect your computer.

What you should do:
1. Do not open attachments from unknown senders.
2. Confirm with the sender if you were not expecting the file.
3. Scan downloaded files with antivirus.
4. Avoid enabling macros in Word or Excel files.
5. Delete suspicious attachments.

Simple rule:
Unexpected file = verify first.",

                    @"Check the sender's email address carefully.

Criminals often create email addresses that look almost real. They may replace letters with numbers or add extra words.

Beginner-friendly example:
A real address may be: support@amazon.com
A fake one may be: support@amaz0n-security.com

What you should do:
1. Look at the full email address, not only the display name.
2. Watch for spelling changes.
3. Be careful with free email addresses pretending to be companies.
4. Do not reply with private details.
5. Report the email if your email app allows it.

Remember:
A company logo in an email does not prove the email is real. Logos can be copied."
                }
            },
            {
                "scam", new List<string>
                {
                    @"A scam is a trick used to steal your money, personal information, or account access.

Scammers may contact you through phone calls, SMS, WhatsApp, Facebook, email, or fake websites. They often pretend to be a bank, employer, delivery company, government office, or romantic partner.

Common examples:
1. Fake job offers that ask you to pay a registration fee.
2. Fake investment opportunities promising quick money.
3. Messages saying you won a prize you never entered.
4. People pretending to be family members in an emergency.
5. Fake buyers or sellers on online marketplaces.

What you should do:
1. Do not rush.
2. Do not send money before verifying.
3. Do not share your ID number, bank PIN, password, or OTP.
4. Search for the company using official sources.
5. Ask someone you trust before making a payment.",

                    @"If something sounds too good to be true, it is probably a scam.

Scammers use exciting promises to make people ignore warning signs. They may promise a job, bursary, prize, loan, or investment return that seems unusually easy.

Beginner-friendly example:
A person says you can earn R5 000 a day by only paying R300 to join. This is a major warning sign.

Warning signs:
1. You must pay money first.
2. They refuse to provide official documents.
3. They pressure you to act immediately.
4. They ask for your banking details.
5. They communicate only through WhatsApp and avoid official channels.

Safe action:
Verify the offer using the official company website or phone number.",

                    @"Be careful when someone asks for an OTP.

An OTP means one-time password. It is a temporary code used to prove that you are the real account owner. Criminals may call you pretending to be from your bank and ask for this code.

Important:
No real bank employee should ask you to read out your OTP.

What you should do:
1. Never share an OTP with anyone.
2. End the call if someone asks for it.
3. Contact your bank using the number on your bank card or official app.
4. Change your password if you shared an OTP by mistake.
5. Report suspicious activity immediately.

Simple rule:
Your OTP is like a key. Do not hand it to anyone.",

                    @"Scammers often create urgency to stop you from thinking clearly.

They may say things like ""act now"", ""last chance"", ""your account will be closed"", or ""send money immediately"". This pressure is designed to make you panic.

What you should do:
1. Pause before acting.
2. Check the facts.
3. Speak to a trusted person.
4. Use official contact details.
5. Do not click links or make payments while stressed.

Beginner-friendly example:
If someone says your bank account will be blocked in 10 minutes, do not follow their link. Open your banking app or call your bank directly.",

                    @"Report scams so that other people can be protected.

If you receive a scam message, you should not feel embarrassed. Scammers are trained to manipulate people. Reporting helps stop them.

What you should do:
1. Take screenshots of the scam message.
2. Do not delete important evidence immediately.
3. Report the message on the platform, such as WhatsApp, Facebook, or email.
4. Contact your bank quickly if money or banking details were involved.
5. Warn friends and family, especially older relatives or people who may not know the warning signs.

Remember:
Being scammed does not mean you are foolish. It means the criminal used manipulation. The important thing is to act quickly."
                }
            },
            {
                "privacy", new List<string>
                {
                    @"Online privacy means controlling what personal information you share and who can see it.

Personal information includes your full name, phone number, home address, ID number, school, workplace, photos, location, and banking details.

Why privacy matters:
Criminals can use small pieces of information to guess passwords, answer security questions, open fake accounts, or pretend to be you.

What you should do:
1. Keep social media accounts private where possible.
2. Do not post your ID, bank card, school timetable, or home address.
3. Avoid sharing your live location publicly.
4. Think before posting personal photos or documents.
5. Check who can see your posts.

Simple rule:
Do not post information that you would not want a stranger to use.",

                    @"Review your social media privacy settings.

Many apps allow strangers to see your photos, friends list, phone number, or location unless you change the settings.

What you should check:
1. Who can see your posts?
2. Who can send you friend requests?
3. Who can see your phone number or email?
4. Is your location visible?
5. Can people tag you without approval?

Beginner-friendly example:
If your profile is public, a scammer may learn your school, friends, birthday, and interests. They can use that information to create a believable scam.

Safe action:
Set your profile to private and only accept people you truly know.",

                    @"Be careful with app permissions.

Apps may ask for access to your camera, microphone, contacts, photos, or location. Some permissions are necessary, but others are not.

Beginner-friendly example:
A weather app may need location access, but it probably does not need your contacts or microphone.

What you should do:
1. Open your phone settings.
2. Check app permissions.
3. Remove permissions that are not needed.
4. Delete apps you no longer use.
5. Download apps only from trusted stores.

Why this matters:
Too many permissions can expose private information if an app is unsafe or hacked.",

                    @"Only give personal information when it is truly needed.

Some websites and forms ask for more information than necessary. Before filling in a form, ask why the information is needed and how it will be protected.

Examples of sensitive information:
1. ID number
2. Bank details
3. Home address
4. Phone number
5. Passwords or PINs

What you should do:
1. Check if the website is official.
2. Read the form carefully.
3. Leave optional fields blank if they are not needed.
4. Never enter private details on a suspicious link.
5. Ask the organisation directly if you are unsure.

Simple rule:
Share less. Protect more.",

                    @"Your digital footprint is the trail of information you leave online.

This includes posts, comments, photos, search history, accounts, and information that other people share about you.

Why it matters:
Future employers, schools, scammers, and strangers may find information about you online.

What you should do:
1. Search your own name online sometimes.
2. Delete old posts that reveal too much.
3. Remove accounts you no longer use.
4. Avoid posting when angry or emotional.
5. Think before sharing personal information.

Beginner-friendly reminder:
Once something is online, it can be copied, saved, or shared even if you delete it later."
                }
            },
            {
                "malware", new List<string>
                {
                    @"Malware is harmful software that can damage your device or steal information.

The word malware includes viruses, spyware, ransomware, trojans, and other dangerous programs. Malware can enter your device through fake downloads, infected attachments, unsafe websites, or pirated software.

Beginner-friendly example:
You download a free movie or cracked app, but it secretly installs software that steals your passwords.

Warning signs:
1. Your device becomes very slow.
2. Strange pop-ups appear.
3. Apps open by themselves.
4. Files disappear or change.
5. Your antivirus gives alerts.

What you should do:
1. Stop using suspicious files.
2. Run a full antivirus scan.
3. Uninstall unknown apps.
4. Update your device.
5. Ask for technical help if the problem continues.",

                    @"Keep your device updated.

Updates fix security weaknesses. Criminals look for old devices and apps because they are easier to attack.

Beginner-friendly example:
If your phone has an old security problem and you never update it, malware may use that weakness to enter your device.

What you should do:
1. Turn on automatic updates.
2. Update Windows, Android, iOS, and your apps.
3. Restart your device after updates if required.
4. Remove apps that no longer receive updates.
5. Do not ignore update notifications for too long.

Simple rule:
Updates are not only for new features. They also protect you.",

                    @"Only download software from trusted sources.

Unsafe downloads are one of the easiest ways to get malware. Free cracked software, fake games, and unofficial app files can hide dangerous code.

What you should do:
1. Use Microsoft Store, Google Play Store, Apple App Store, or official websites.
2. Avoid pirated software.
3. Read reviews before installing apps.
4. Check the developer name.
5. Be careful with websites full of pop-ups and download buttons.

Beginner-friendly example:
If a website has many buttons saying ""Download Now"", one of them may be an advert that installs malware.",

                    @"Use antivirus protection.

Antivirus software helps detect and remove harmful programs. It is not perfect, but it adds an important layer of protection.

What you should do:
1. Keep antivirus switched on.
2. Allow it to update regularly.
3. Run a full scan if your device acts strangely.
4. Do not ignore security alerts.
5. Avoid installing two different antivirus programs at the same time because they may conflict.

Important:
Antivirus helps, but safe behaviour is still necessary. Do not click suspicious links just because antivirus is installed.",

                    @"Back up your important files.

A backup is a safe copy of your files. If malware damages your device, a backup can help you recover your photos, assignments, documents, and projects.

Beginner-friendly example:
If ransomware locks your laptop, you can restore your files from a backup instead of losing everything.

What you should do:
1. Copy important files to an external drive or cloud storage.
2. Back up school work regularly.
3. Keep one backup disconnected from your computer.
4. Test that you can open your backup files.
5. Do not rely on only one copy of important work.

Simple rule:
If a file is important, keep more than one copy."
                }
            },
            {
                "2fa", new List<string>
                {
                    @"Two-factor authentication, also called 2FA, adds extra protection to your account.

Normally, you log in with a password. With 2FA, you need a second proof, such as a code from an app, SMS, email, or fingerprint.

Beginner-friendly example:
Your password is like a door key. 2FA is like a second lock on the same door.

Why it helps:
Even if someone steals your password, they may still be blocked because they do not have the second code.

What you should do:
1. Turn on 2FA for email first.
2. Turn on 2FA for banking, social media, and school accounts.
3. Use an authenticator app where possible.
4. Save backup codes safely.
5. Never share 2FA codes with anyone.",

                    @"Authenticator apps are usually safer than SMS codes.

SMS codes can be risky because criminals may try SIM-swapping. SIM-swapping is when a criminal tricks a mobile network into moving your number to their SIM card.

Beginner-friendly example:
If your SMS codes go to a criminal's SIM card, they may access your accounts.

What you should do:
1. Use an authenticator app if the service supports it.
2. Protect your phone with a PIN, password, fingerprint, or face lock.
3. Do not approve login requests you did not start.
4. Keep backup codes in a safe place.
5. Contact your mobile network if your SIM suddenly stops working.

Important:
If you receive a 2FA code you did not request, someone may know your password. Change it immediately.",

                    @"Never share your 2FA code or OTP.

A one-time password is meant only for you. Scammers may pretend to be bank staff, IT support, delivery workers, or customer service and ask for it.

What you should do:
1. Do not read the code to anyone.
2. Do not type the code into a link sent by a stranger.
3. End calls where someone asks for your OTP.
4. Report suspicious requests.
5. Change your password if you accidentally shared a code.

Simple rule:
A real support agent does not need your OTP.",

                    @"Use backup codes carefully.

When you set up 2FA, some websites give backup codes. These help you access your account if your phone is lost.

What you should do:
1. Save backup codes in a safe place.
2. Do not store them in public notes or screenshots.
3. Do not share them with friends.
4. Use each code only when necessary.
5. Generate new backup codes if you think someone saw them.

Beginner-friendly example:
Backup codes are like spare keys. Keep them safe, not under the doormat."
                }
            },
            {
                "wifi", new List<string>
                {
                    @"Public Wi-Fi can be risky.

Public Wi-Fi means internet in places like malls, taxis, libraries, schools, airports, and coffee shops. These networks are convenient, but they may not be secure.

Risks:
1. Someone may watch network traffic.
2. A fake hotspot may pretend to be the real Wi-Fi.
3. Attackers may try to steal login details.
4. Your device may connect automatically without you noticing.

What you should do:
1. Avoid banking on public Wi-Fi.
2. Use mobile data for sensitive activities.
3. Use a VPN if you must use public Wi-Fi.
4. Forget public networks after using them.
5. Turn off auto-connect.

Simple rule:
Public Wi-Fi is okay for browsing, but not ideal for private accounts.",

                    @"Watch out for fake hotspots.

A fake hotspot is a Wi-Fi network created by an attacker to look real. It may use a name similar to a shop, school, hotel, or restaurant.

Beginner-friendly example:
The real network may be: CafeGuest
The fake one may be: CafeGuest_Free

What you should do:
1. Ask staff for the correct Wi-Fi name.
2. Avoid networks with strange names.
3. Do not enter passwords or banking details on suspicious networks.
4. Turn off file sharing on your laptop.
5. Disconnect if the network behaves strangely.

Why this matters:
If you connect to a fake hotspot, the attacker may try to monitor what you do online.",

                    @"Secure your home Wi-Fi.

Your home Wi-Fi should also be protected. If it is weak, neighbours or attackers may use your internet or try to access your devices.

What you should do:
1. Use a strong Wi-Fi password.
2. Change the default router password.
3. Use WPA2 or WPA3 security if available.
4. Do not share your Wi-Fi password with everyone.
5. Restart and update your router when needed.

Beginner-friendly example:
If your Wi-Fi password is 12345678, it is too easy to guess.",

                    @"Turn off auto-connect for public networks.

Auto-connect allows your device to join known networks automatically. This is convenient, but it can be risky if a fake network uses the same name.

What you should do:
1. Open Wi-Fi settings.
2. Select the public network.
3. Turn off auto-join or auto-connect.
4. Forget networks you no longer use.
5. Keep Wi-Fi off when you do not need it.

Simple rule:
Your device should not join unknown networks without your permission."
                }
            },
            {
                "vpn", new List<string>
                {
                    @"A VPN is a tool that helps protect your internet connection.

VPN stands for Virtual Private Network. It creates an encrypted connection, which means your data is changed into a protected form that is difficult for outsiders to read.

Beginner-friendly example:
Using the internet without a VPN on public Wi-Fi is like sending a postcard that others may see. Using a VPN is more like sending the message inside a sealed envelope.

When a VPN is useful:
1. On public Wi-Fi.
2. When travelling.
3. When you want more privacy from network monitoring.
4. When using shared networks.

Important:
A VPN improves privacy, but it does not make you completely invisible or protect you from every scam.",

                    @"Choose a trustworthy VPN.

Not all VPNs are safe. Some free VPNs may collect your browsing data and sell it to advertisers.

What you should check:
1. Does the VPN have a good reputation?
2. Does it clearly explain its privacy policy?
3. Does it avoid logging your activity?
4. Does it have strong security features?
5. Is it from a trusted company?

Beginner-friendly warning:
If a free VPN seems too good to be true, it may be making money from your data.",

                    @"A VPN does not replace safe browsing habits.

Even with a VPN, you can still be tricked by phishing, fake websites, scams, and malware.

What a VPN can help with:
1. Encrypting traffic on public Wi-Fi.
2. Hiding your IP address from some websites.
3. Reducing some tracking.

What a VPN cannot do:
1. It cannot stop you from entering your password on a fake website.
2. It cannot remove malware already on your device.
3. It cannot make weak passwords safe.
4. It cannot guarantee total anonymity.

Simple rule:
Use a VPN together with strong passwords, 2FA, updates, and careful clicking.",

                    @"Use a VPN carefully on public Wi-Fi.

Public Wi-Fi is one of the best times to use a VPN because other people may be on the same network.

What you should do:
1. Connect to the VPN before logging into important accounts.
2. Make sure the VPN shows it is connected.
3. Avoid banking if the network feels suspicious.
4. Disconnect from public Wi-Fi when finished.
5. Keep your VPN app updated.

Beginner-friendly reminder:
A VPN protects the connection, but you must still check websites and avoid suspicious links."
                }
            },
            {
                "ransomware", new List<string>
                {
                    @"Ransomware is a type of malware that locks your files and demands money.

The attacker may say you must pay to get your files back. Paying is risky because criminals may take the money and still not unlock your files.

Beginner-friendly example:
You open a fake attachment, and suddenly your assignments, photos, and documents cannot open. A message demands payment.

What you should do:
1. Disconnect from the internet immediately.
2. Do not pay quickly out of panic.
3. Ask for technical help.
4. Report the incident if it affects work or school.
5. Restore files from a clean backup if you have one.

Best protection:
Regular backups are the strongest defence.",

                    @"Backups protect you from ransomware.

A backup is a copy of your important files. If ransomware locks your computer, you can restore your files from the backup.

What you should do:
1. Keep copies of important files in cloud storage.
2. Keep another copy on an external drive.
3. Disconnect the external drive after backing up.
4. Back up school projects before deadlines.
5. Test your backups sometimes.

Beginner-friendly example:
If your laptop is locked but your assignment is saved in the cloud and on a USB drive, you have a better chance of recovering it.",

                    @"Ransomware often spreads through phishing.

Many ransomware attacks begin when someone clicks a dangerous link or opens an infected attachment.

Warning signs:
1. Unexpected invoices or delivery notes.
2. Files from unknown senders.
3. Messages that pressure you to open something.
4. Downloads from unsafe websites.
5. Cracked software or illegal downloads.

What you should do:
1. Verify attachments before opening them.
2. Keep antivirus active.
3. Update your device.
4. Avoid pirated software.
5. Back up important files.",

                    @"If ransomware attacks, act quickly.

Fast action can reduce damage, especially on networks with many devices.

What you should do:
1. Disconnect Wi-Fi or unplug the network cable.
2. Do not plug in backup drives.
3. Take a photo of the ransom message.
4. Contact someone with IT knowledge.
5. Use a clean device to change important passwords.

Important:
Do not delete everything immediately because evidence may be needed to understand what happened."
                }
            },
            {
                "social engineering", new List<string>
                {
                    @"Social engineering is when criminals manipulate people instead of hacking technology.

They use emotions like fear, trust, curiosity, kindness, or urgency to make people do unsafe things.

Beginner-friendly example:
Someone calls and says they are from your bank. They sound professional and say your account is in danger. Then they ask for your OTP. This is social engineering.

Common tricks:
1. Pretending to be IT support.
2. Pretending to be a bank employee.
3. Pretending to be a friend or family member.
4. Creating fake emergencies.
5. Offering fake prizes or jobs.

What you should do:
Pause, verify, and never share private information quickly.",

                    @"Pretexting is a common social engineering method.

Pretexting means the attacker creates a believable story to trick you.

Beginner-friendly example:
A caller says, ""I am from your network provider. We need your ID number to fix your SIM card."" The story may sound real, but it could be fake.

What you should do:
1. Ask for their name and department.
2. End the call.
3. Contact the organisation using official contact details.
4. Do not use numbers sent by the suspicious person.
5. Never share passwords, PINs, or OTPs.

Simple rule:
A believable story is not proof that the person is real.",

                    @"Be careful when a request feels unusual.

Even if a message comes from someone you know, their account may have been hacked.

Beginner-friendly example:
A friend's WhatsApp asks you for money urgently, but the wording sounds strange. Their account may be compromised.

What you should do:
1. Call the person directly.
2. Ask a question only they would know.
3. Do not send money immediately.
4. Check for unusual language.
5. Report hacked accounts.

Important:
Trust the person, but verify the request.",

                    @"Social engineering works because people want to be helpful.

Criminals may take advantage of kindness by asking you to open a door, share a code, click a link, or send information.

What you should do:
1. Be polite but careful.
2. Follow security rules even if someone seems friendly.
3. Do not let strangers pressure you.
4. Ask a trusted person if unsure.
5. Report suspicious requests.

Beginner-friendly reminder:
Saying ""I need to verify this first"" is not rude. It is safe."
                }
            }
        };

        public static readonly Dictionary<string, string> SentimentResponses =
            new Dictionary<string, string>
        {
            { "worried",     "It is completely understandable to feel worried. Cybersecurity can sound scary, especially when there are many technical words. I will explain it step by step in simple language and give you actions you can actually follow." },
            { "scared",      "There is no need to panic. Feeling scared usually means you care about protecting yourself. Let us focus on simple safety steps that reduce risk." },
            { "frustrated",  "I understand. Cybersecurity can be frustrating when instructions are too technical. I will break the topic into clear, beginner-friendly steps." },
            { "confused",    "No worries. I will avoid difficult jargon and explain the topic as if you are learning it for the first time." },
            { "curious",     "That is a great attitude. Being curious helps you notice risks before they become serious problems." },
            { "overwhelmed", "It is okay to feel overwhelmed. You do not need to learn everything at once. Let us focus on one topic and one practical step at a time." },
            { "angry",       "That is understandable. Cyber threats can feel unfair and invasive. Let us focus on what you can do to regain control and protect your accounts." },
            { "unsure",      "It is fine to be unsure. Online safety is something people learn with practice. I will guide you using simple examples." },
            { "nervous",     "Feeling nervous about online safety is normal. Let us turn that nervous feeling into practical protection steps." }
        };

        public static readonly List<string> FollowUpPhrases = new List<string>
        {
            "tell me more", "more info", "more information", "explain more",
            "elaborate", "another tip", "give me another", "one more",
            "continue", "go on", "what else", "keep going",
            "explain further", "another one", "more please",
            "say more", "expand on that"
        };

        public static readonly List<string> PurposeResponses = new List<string>
        {
            @"I am CyberShield, your beginner-friendly cybersecurity awareness assistant.

My job is to explain online safety in simple language so that anyone can understand, even without IT skills.

You can ask me about:
1. Password safety
2. Phishing
3. Online scams
4. Privacy
5. Malware
6. Ransomware
7. Two-factor authentication
8. Public Wi-Fi
9. VPNs
10. Social engineering

Example questions:
- What is phishing?
- How do I make a strong password?
- How do I know if a message is a scam?
- What should I do if I clicked a suspicious link?",

            @"My purpose is to help you stay safe online by explaining cyber threats in a clear and practical way.

I do not expect you to know technical terms. I explain what the threat means, give an everyday example, and suggest simple steps you can follow.

For example, if you ask about phishing, I will explain what phishing is, how it looks, why it is dangerous, and what to do when you receive a suspicious message.",

            @"I can help you understand common online dangers such as weak passwords, fake links, scams, malware, ransomware, unsafe Wi-Fi, and identity theft.

I am designed for ordinary users, students, families, and anyone who wants to be safer online without needing advanced computer knowledge."
        };
    }
}
