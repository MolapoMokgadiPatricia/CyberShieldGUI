# CyberShield Cybersecurity Awareness Chatbot

## Overview

CyberShield is a cybersecurity awareness chatbot developed using C# and Windows Presentation Foundation (WPF). The application aims to educate users about cybersecurity threats, online safety, and digital best practices through an interactive chatbot experience.

Part 3 extends the project by introducing task management, MySQL database integration, cybersecurity quizzes, natural language processing enhancements, and activity logging while preserving all functionality from Parts 1 and 2.

---

# Features

## Interactive GUI

* User-friendly WPF graphical interface
* Clean and intuitive chatbot layout
* Personalized greeting system
* Improved navigation between chatbot and task management features

## Cybersecurity Awareness

The chatbot provides educational guidance on:

* Password security
* Phishing attacks
* Online scams
* Malware
* Ransomware
* VPN usage
* Safe Wi-Fi practices
* Two-Factor Authentication (2FA)
* Privacy protection
* Social engineering

---

# Smart Chatbot Functionality

* Keyword recognition
* Randomized responses
* Conversation flow support
* Follow-up question handling
* Sentiment detection
* User memory and recall
* Personalized responses
* Improved Natural Language Processing (NLP)

---

# Task Assistant

The chatbot includes a cybersecurity task manager allowing users to:

* Create new security-related tasks
* View saved tasks
* Mark tasks as completed
* Delete tasks
* Set reminders for important cybersecurity activities

Example tasks include:

* Change passwords
* Enable Two-Factor Authentication
* Scan computer for malware
* Update antivirus software
* Backup important files

---

# MySQL Database Integration

CyberShield uses MySQL to permanently store user tasks.

Database Features:

* Database connection testing
* Persistent task storage
* Add tasks
* View tasks
* Delete tasks
* Update task completion status
* Error handling for database operations

Database:

CyberShieldDB

Table:

Tasks

---

# Cybersecurity Quiz

The application includes an interactive quiz featuring multiple-choice cybersecurity questions.

Features:

* Multiple questions
* Automatic scoring
* Instant feedback
* Educational explanations
* Final score summary

Quiz Topics:

* Password Security
* Phishing
* Malware
* Ransomware
* VPN
* Online Privacy
* Safe Browsing
* Two-Factor Authentication
* Social Engineering
* Cyber Hygiene

---

# Activity Log

The chatbot records user activities such as:

* Task creation
* Task completion
* Task deletion
* Quiz participation
* Reminder creation
* Chatbot interactions

This improves usability and allows users to track their cybersecurity activities.

---

# Multimedia Features

* Audio greeting
* ASCII logo
* Interactive chatbot experience

---

# Technologies Used

* C#
* .NET
* Windows Presentation Foundation (WPF)
* MySQL
* MySQL Workbench
* MySql.Data
* Visual Studio 2026
* GitHub

---

# Project Structure

```text
CyberShieldGUI/
│
├── Models/
│   ├── CyberTask.cs
│   └── QuizQuestion.cs
│
├── Services/
│   ├── ChatBot.cs
│   ├── DatabaseService.cs
│   ├── QuizService.cs
│   ├── ActivityLogService.cs
│   └── ResponseBank.cs
│
├── Audio/
│   └── Greeting.wav
│
├── MainWindow.xaml
├── MainWindow.xaml.cs
├── App.xaml
├── App.xaml.cs
├── CyberShieldGUI.csproj
└── README.md
```

---

# Error Handling

The application includes exception handling for:

* Invalid user input
* Database connection failures
* Empty task fields
* SQL exceptions
* Unexpected application errors

---

# GitHub Repository

Repository:

https://github.com/MolapoMokgadiPatricia/CyberShieldGUI

GitHub Releases:

* v1.3 – Initial Chatbot
* v1.4 – Enhanced Chatbot Features
* v1.5 – Part 3 Final Submission

---

# Educational Purpose

The CyberShield chatbot was developed for educational purposes as part of the PROG6221 Portfolio of Evidence.

The project aims to:

* Promote cybersecurity awareness
* Teach safe online practices
* Encourage responsible digital behaviour
* Demonstrate object-oriented programming principles
* Apply database integration using MySQL
* Showcase GUI application development using WPF

---

# Author

**Mokgadi Patricia Molapo**

Diploma in Information Software Development

Rosebank College
