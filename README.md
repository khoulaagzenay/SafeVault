# SafeVault — Secure Web App

This repository collects the final deliverable for the SafeVault secure coding project:
- Input validation & SQL injection prevention
- Authentication & Authorization (RBAC)
- Debugging & resolving vulnerabilities (SQLi, XSS)
- Automated security tests

## Contents
- `app/` — application source (controllers, routes, middleware)
- `db/` — database helpers (parameterized queries)
- `auth/` — authentication + RBAC middleware
- `tests/` — automated tests (Jest + SuperTest)
- `SECURITY_SUMMARY.md` — brief summary of vulnerabilities and fixes
- `README.md` — this file

🤖 How Copilot Helped
1. Install dependencies:
   ```bash
   - Code Suggestions: Copilot accelerated development by suggesting boilerplate code for controllers, models, and validation logic.

- Security Guidance: It flagged insecure patterns (like raw SQL) and recommended safer alternatives such as parameterized queries.

- Debugging Assistance: During login and hashing implementation, Copilot helped troubleshoot issues like incorrect password verification and null reference errors.

- Learning Support: Provided inline documentation and examples for .NET security best practices, including hashing, validation, and session handling.
Activity 1
Step 1 – Project Setup

Create the project

dotnet new mvc -o FinalProject


Set up the form view

Create the model: Models/UserInput.cs

Create the controller: Controllers/FormController.cs

Create the view: Views/Form/Index.cshtml

Configure the database

Add Entity Framework dependencies

Create the model: Models/User.cs

Create the context: Data/AppDbContext.cs

Configure the database in Program.cs

Add the connection string in appsettings.json

Apply migrations

✅ After these steps, the database file FinalProject.db will be generated.

Add unit tests

Create the test project:

dotnet new nunit -o FinalProject.Tests


Create the test file: FinalProject.Tests/Tests/TestInputValidation.cs

Run the tests:

dotnet test

Step 2 – Input Validation

Create Helpers/InputValidator.cs to validate user input.

Create Services/AuthService.cs to handle user registration and storage.

Call AuthService in the FormController.cs submit route.

Step 3 – SQL Query Parameterization

In AuthService, use parameterized SQL queries such as:

INSERT INTO Users (Username, Email, PasswordHash, Role)
VALUES (@Username, @Email, @PasswordHash, @Role)


Use SqliteCommand to safely bind parameters and prevent SQL injection.

Activity 2
Step 2 – Add Login Functionality

Add a Password field to the UserInput model.

Create a PasswordHelper class to handle password hashing and verification.

When registering a user, hash the password.

When logging in, verify the password before granting access.

Step 3 – Implement Role-Based Access Control (RBAC)

Add a Role property to the User model.

Store the username in the session upon login.

Check the user’s role when accessing restricted routes.

Note: Role management should ideally be implemented using JWT (JSON Web Tokens), but it was not functional in this version.
