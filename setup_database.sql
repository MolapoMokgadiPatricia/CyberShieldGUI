-- CyberShield Part 3 — MySQL Database Setup
-- Run this script once in MySQL Workbench before launching the application.

CREATE DATABASE IF NOT EXISTS CyberShieldDB;

USE CyberShieldDB;

CREATE TABLE IF NOT EXISTS Tasks (
    Id           INT          AUTO_INCREMENT PRIMARY KEY,
    Title        VARCHAR(100) NOT NULL,
    Description  TEXT,
    ReminderDate DATETIME     NULL,
    IsCompleted  BOOLEAN      DEFAULT FALSE
);

DESCRIBE Tasks;