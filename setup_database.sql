USE CyberShieldDB;

CREATE TABLE Tasks (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(100),
    Description TEXT,
    ReminderDate DATETIME,
    IsCompleted BOOLEAN DEFAULT FALSE
);