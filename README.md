# Job Portal Backend - ASP.NET Controller-Based RESTful API

Welcome to the ASP.NET Controller-Based RESTful API project for an online job portal. In this tutorial, I will guide you through building the backend for a job portal application. This project demonstrates the efficiency of developing a robust backend using ASP.NET and C#.

### Introduction
In this tutorial, I will create an ASP.NET Controller-Based RESTful API to power the backend of an online job portal. This project allows job seekers to search for job listings, employers to post job openings, and job applicants to submit applications.

## Database Design
## Tables
To kickstart our project, I first designed our database by identifying key tables for the system. Here's a list of tables I've identified for our job portal:

- Users
- Employers
- JobSeekers
- JobListings
- Applications
- Skills
- JobSeekerSkills
- EmployersJobListings
- Categories
- JobCategoryMapping
- Messages
- Notifications
  
This list includes the core tables required for a job portal, but it can be extended by adding more tables to meet specific requirements.

### Database Backend
I created our database using MySQL. However, you can choose any database you prefer, provided you install the relevant Entity Framework package and configure the database connection accordingly. You can find the database schema creation script in the database_schema.sql file under the scripts folder.

## Development

### Project Setup
Create an ASP.NET Core Web API project using Visual Studio Code's "Create .NET Project" button or through the command-line interface (CLI). Ensure that you have the C# Dev Kit extension installed for a seamless development experience.

Organize the project structure with the following folders: "Models," "Services," and "Controllers."

### Models
In the "Models" folder, I created a .cs file for each table I designed earlier. These models represent the database tables and their fields. I defined the data structures and relationships between tables.

### Services
In the "Services" folder, I created services that encapsulate the data access logic using Entity Framework Core. These services interact with the database and provide data to the controllers.

### Controllers
In the "Controllers" folder, I created controllers that handle HTTP requests and responses. I wrote RESTful API code to perform actions such as retrieving job listings, posting job openings, submitting applications, and more.

### DbContext
I created an ApplicationDbContext class under the DbContext folder. This class serves as the entry point to the database, allowing me to interact with the MySQL database using Entity Framework Core.

## Deployment and Testing
You can deploy the job portal backend to your local machine for testing and development. Alternatively, you can host it on your favorite cloud provider for real-world use. Test the API by making requests and ensuring it functions as expected. You can use tools like Postman or write unit tests to validate the API's behavior.

I've built a professional ASP.NET Controller-Based RESTful API for an online job portal. Feel free to extend this project by adding features such as user authentication, real-time notifications, and more to make it even more impressive. Happy coding!
