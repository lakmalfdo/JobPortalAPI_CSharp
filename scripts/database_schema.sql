-- Create the Users table
CREATE TABLE Users (
    UserID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    UserRole ENUM('Employer', 'Job Seeker') NOT NULL,
    ProfilePicture VARCHAR(255),
    AuthenticationToken VARCHAR(255)
);

-- Create the Employers table
CREATE TABLE Employers (
    EmployerID INT PRIMARY KEY,
    CompanyName VARCHAR(255) NOT NULL,
    CompanyDescription TEXT,
    CompanyLogo VARCHAR(255)
);

-- Create the Job Seekers table
CREATE TABLE JobSeekers (
    JobSeekerID INT PRIMARY KEY,
    Resume VARCHAR(255),
    CoverLetter VARCHAR(255),
);

-- Create the Job Listings table
CREATE TABLE JobListings (
    JobID INT AUTO_INCREMENT PRIMARY KEY,
    EmployerID INT,
    JobTitle VARCHAR(255) NOT NULL,
    JobDescription TEXT NOT NULL,
    JobRequirements TEXT,
    Salary DECIMAL(10, 2),
    Location VARCHAR(255),
    ApplicationDeadline DATE,
    ApplicationStatus ENUM('Open', 'Closed', 'Filled', 'Other') NOT NULL,
);

-- Create the Applications table
CREATE TABLE Applications (
    ApplicationID INT AUTO_INCREMENT PRIMARY KEY,
    JobID INT,
    JobSeekerID INT,
    ApplicationStatus ENUM('Pending', 'Reviewed', 'Rejected', 'Accepted', 'Other') NOT NULL,
    ApplicationDate DATE,
    AttachedDocuments VARCHAR(255),
    Comments TEXT
);

-- Create the Skills table
CREATE TABLE Skills (
    SkillID INT AUTO_INCREMENT PRIMARY KEY,
    SkillName VARCHAR(255) NOT NULL,
    SkillDescription TEXT
);

-- Create the JobSeekerSkills table
CREATE TABLE JobSeekerSkills (
    JobSeekerSkillID INT AUTO_INCREMENT PRIMARY KEY,
    JobSeekerID INT,
    SkillID INT,
    ProficiencyLevel ENUM('Beginner', 'Intermediate', 'Expert') NOT NULL
);

-- Create the EmployersJobListings table
CREATE TABLE EmployersJobListings (
    EmployersJobListingID INT AUTO_INCREMENT PRIMARY KEY,
    EmployerID INT,
    JobID INT
);

-- Create the Categories table
CREATE TABLE Categories (
    CategoryID INT AUTO_INCREMENT PRIMARY KEY,
    CategoryName VARCHAR(255) NOT NULL,
    CategoryDescription TEXT
);

-- Create the JobCategoryMapping table
CREATE TABLE JobCategoryMapping (
    JobCategoryMappingID INT AUTO_INCREMENT PRIMARY KEY,
    JobID INT,
    CategoryID INT
);

-- Create the Messages table
CREATE TABLE Messages (
    MessageID INT AUTO_INCREMENT PRIMARY KEY,
    SenderID INT,
    ReceiverID INT,
    MessageContent TEXT,
    Timestamp DATETIME
);

-- Create the Notifications table
CREATE TABLE Notifications (
    NotificationID INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT,
    NotificationContent TEXT,
    NotificationType VARCHAR(255),
    Timestamp DATETIME
);
