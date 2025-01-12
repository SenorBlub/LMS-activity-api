CREATE DATABASE activitydb;
USE activitydb;

CREATE TABLE Activities (
    Id CHAR(36) PRIMARY KEY, 
    Title TEXT NOT NULL, 
    IsPrivate BOOLEAN NOT NULL 
);

CREATE TABLE ActivityContents (
    Id CHAR(36) PRIMARY KEY, 
    ActivityId CHAR(36) NOT NULL,
    ContentId CHAR(36) NOT NULL,
    FOREIGN KEY (ActivityId) REFERENCES Activities(Id) ON DELETE CASCADE
);
