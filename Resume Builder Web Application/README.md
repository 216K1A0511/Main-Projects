# 🧾 Resume Builder Web Application

A full-stack **Resume Builder Web Application** developed using **PHP, MySQL, HTML, CSS, JavaScript**, and running on **XAMPP**.  
This project allows users to create, edit, and manage professional resumes with OTP-based authentication.

---

## 🚀 Features

- User Registration & Login
- Email OTP Authentication (PHPMailer)
- Resume Editor with:
  - Personal Details
  - Education
  - Work Experience
  - Projects
  - Skills
  - Certifications
  - Languages
  - Custom Fields
- Admin & User Roles
- Secure Session Handling
- MySQL Database Integration

---

## 🛠️ Tech Stack

- **Frontend:** HTML, CSS, JavaScript
- **Backend:** PHP
- **Database:** MySQL
- **Server:** Apache (XAMPP)
- **Mailer:** PHPMailer
- **Environment:** Windows

---

## ⚙️ Setup Instructions

### 1️⃣ Install XAMPP
```bash
winget install ApacheFriends.Xampp.8.2
```

### 2️⃣ Move Project
Copy the project folder to:
```text
C:\xampp\htdocs\Resume-React
```

### 3️⃣ Start Services
Open **XAMPP Control Panel** and start:
- Apache
- MySQL

### 4️⃣ Database Setup
- Open http://localhost/phpmyadmin
- Create database: `resume_builder`
- Import `schema.sql`

### 5️⃣ Run Application
```text
http://localhost/Resume-React/login1.php
```

---

## 🔐 Default Credentials

**User**
- Username: `testuser`
- Email: `test@example.com`

**Admin**
- Username: `admin`
- Password: `admin123`

---

## 📂 Project Structure

```
Resume-React/
│── includes/
│── vendor/
│── assets/
│── schema.sql
│── login1.php
│── dashboard.php
│── README.md
```

---

## 📜 License

This project is licensed under the **MIT License**.  
You are free to use, modify, and distribute this project with proper attribution.

---

## 👤 Author

**Durgaprasad Bollinkala**  
B.Tech CSE (Data Science)  
Kakinada, India  
