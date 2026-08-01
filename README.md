# 🏦 Title Deed Management System (TDMS)

> A secure enterprise-grade banking workflow application developed during my internship at **State Bank of India (SBI)** using **ASP.NET Core MVC**, **C#**, **Entity Framework Core**, and **SQL Server**.

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-10.0-512BD4?style=for-the-badge&logo=.net)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver)
![Entity Framework Core](https://img.shields.io/badge/EF_Core-512BD4?style=for-the-badge)
![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3?style=for-the-badge&logo=bootstrap)
![License](https://img.shields.io/badge/Status-Completed-success?style=for-the-badge)

---

## 📖 Overview

The **Title Deed Management System (TDMS)** is a role-based web application designed to digitize the complete lifecycle of title deed management in a banking environment.

The system replaces manual register-based tracking with a secure **Maker–Checker workflow**, ensuring proper authorization, accountability, and traceability for every title deed transaction.

The application was developed as part of my internship at **State Bank of India (SBI)**.

---

# ✨ Key Features

### 👤 Role-Based Authentication

- Secure Login
- Cookie Authentication
- Role-Based Authorization
- Multiple Roles Support

---

### 👥 User Management

- Create Users
- Edit Users
- Activate / Deactivate Users
- Assign Multiple Roles
- Branch & Designation Management

---

### 📄 Title Deed Entry

- Account Search
- Customer Details Fetch
- Collateral Selection
- Title Deed Registration
- Physical Storage Allocation
  - Compactor
  - Rack

---

### ✅ Maker–Checker Workflow

Every major banking operation follows an approval workflow.

```
Maker
      │
      ▼
Checker Approval
      │
      ▼
Status Updated
```

Implemented for:

- Title Deed Entry
- CERSAI Satisfaction
- TD Delivery

---

### 🏦 CERSAI Satisfaction Module

- Eligible Account Search
- Raise CERSAI Satisfaction Request
- Approval by CMM Checker
- Status Tracking

---

### 📦 TD Delivery Module

- Raise Delivery Request
- Eligibility Validation
- Delivery Checker Approval
- Prevent Duplicate Requests

---

### 📜 Delivered Title Deeds

- View Delivered Records
- Historical Lookup
- Delivery Date
- Detailed Information

---

## 🏗 Architecture

The project follows a **Layered (N-Tier) Architecture**.

```
Presentation Layer
(Razor Views + Bootstrap + AJAX)

        │

MVC Controllers

        │

Service Layer
(Business Logic)

        │

Repository Layer
(Data Access)

        │

Entity Framework Core

        │

SQL Server
```

---

# 🧩 Technologies Used

| Technology | Purpose |
|------------|----------|
| ASP.NET Core MVC | Web Framework |
| C# | Backend |
| Entity Framework Core | ORM |
| SQL Server | Database |
| Razor Views | UI |
| Bootstrap | Responsive Design |
| jQuery + AJAX | Dynamic UI |
| LINQ | Data Queries |

---

# 📂 Project Structure

```
TitleDeedManagementSystem
│
├── Controllers
├── Models
├── ViewModels
├── Views
├── Services
├── Repositories
├── Interfaces
├── Helpers
├── Data
├── wwwroot
└── Program.cs
```

---

# 🔐 User Roles

- Branch Admin
- Maker
- Requisition Checker
- Redeposit Checker
- CMM Checker
- Delivery Checker

---

# 🔄 Workflow

```
User Login
      │
      ▼
Title Deed Entry (Maker)
      │
      ▼
Checker Approval
      │
      ▼
CERSAI Satisfaction
      │
      ▼
CMM Checker Approval
      │
      ▼
TD Delivery Request
      │
      ▼
Delivery Checker Approval
      │
      ▼
Delivered Title Deed History
```

---

# 🚀 Highlights

✅ Enterprise Layered Architecture

✅ Repository Pattern

✅ Service Layer Pattern

✅ Dependency Injection

✅ Role-Based Access Control

✅ Cookie Authentication

✅ AJAX-Based Dynamic Forms

✅ Entity Framework Core

✅ SQL Server Integration

✅ Maker–Checker Banking Workflow

---

# 📚 Learning Outcomes

During this internship I gained practical experience in:

- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server
- Layered Architecture
- Repository Pattern
- Dependency Injection
- Banking Workflow Automation
- Role-Based Authentication
- Enterprise Software Development

---

# 👩‍💻 Developed By

**Jahnvi Srivastava**

B.Tech Computer Science & Engineering  
Amity University Mumbai

**Internship Project**

**State Bank of India (SBI)**

2026

---

⭐ If you found this project interesting, consider giving it a star!
