# Contract Monthly Claim System (CMCS)

A modern web application that streamlines monthly contract claims for lecturers, featuring a robust two-stage approval workflow and role-based access control.

[![Live Demo](https://img.shields.io/badge/demo-live-success)](https://st10434047-cmcs-crh5dhadeqc9a4fm.southafricanorth-01.azurewebsites.net)
[![Video Demo](https://img.shields.io/badge/video-watch-red)](https://youtu.be/kviag0wonsE)

## Overview

CMCS replaces manual claim processing with a centralized platform where lecturers submit hours worked, coordinators verify submissions, and managers approve payments. Built with ASP.NET Core MVC and Entity Framework, the system ensures accurate calculations, secure document handling, and comprehensive reporting.

## Features

### Core Functionality
- **Automated Calculations** - Claims auto-calculate based on HR-configured hourly rates
- **Two-Stage Approval** - Coordinators verify claims before manager approval
- **Document Management** - Secure upload and storage of supporting files (PDF, DOCX, XLSX)
- **Interactive Reports** - Visual dashboards with Chart.js showing claim trends and approval rates
- **Role-Based Security** - Strict access controls for Lecturers, Coordinators, Managers, and HR

### User Capabilities by Role
- **Lecturers** - Submit claims, track status, upload documentation
- **Coordinators** - Verify submitted claims, request revisions
- **Managers** - Final approval authority, bulk claim processing
- **HR** - User management, hourly rate configuration, system reports

## Technology Stack

| Layer | Technology |
|-------|-----------|
| Backend | ASP.NET Core MVC (C#) |
| Database | Entity Framework Core + SQL Server |
| Frontend | Bootstrap 5, Chart.js, FontAwesome |
| Authentication | ASP.NET Core Identity |

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- Visual Studio 2022 (recommended) or Visual Studio Code
- SQL Server LocalDB (included with Visual Studio)

### Installation

1. Clone the repository
```bash
git clone https://github.com/VCCT-PROG6212-2025-G3/CMCS-ST10434047.git
cd CMCS-ST10434047
```

2. Restore dependencies
```bash
dotnet restore
```

3. Update the database
```bash
dotnet ef database update
```

4. Run the application
```bash
dotnet run
```

5. Navigate to `https://localhost:5001` in your browser

## Test Credentials

Access the system using these pre-configured accounts:

| Role | Name | Email | Password | Responsibilities |
|------|------|-------|----------|-----------------|
| **HR Admin** | System Administrator | hr@cmcs.com | `Password123!` | User management, rate configuration, reports |
| **Academic Manager** | Academic Manager | manager@cmcs.com | `Password123!` | Final claim approval |
| **Programme Coordinator** | System Coordinator | programcoordinator@cmcs.com | `Password123!` | Claim verification |
| **Lecturer** | John Doe | lecturer@cmcs.com | `Password123!` | Submit and track claims |

> **Note:** All test accounts use the password `Password123!` for demonstration purposes.

## Usage Guide

### For Lecturers
1. Log in with your credentials
2. Navigate to "Submit Claim"
3. Enter hours worked and upload supporting documents
4. Submit for verification
5. Track claim status on your dashboard

### For Coordinators
1. Access "Pending Verification" page
2. Review lecturer submissions
3. Verify accuracy and approve or reject with feedback
4. Approved claims move to manager queue

### For Managers
1. View "Pending Approval" claims
2. Review coordinator-verified submissions
3. Approve for payment processing
4. Monitor approval trends in reports

### For HR
1. Manage user accounts and roles
2. Set hourly rates per lecturer
3. Generate system-wide reports
4. Export data for payroll integration

## Project Structure

```
CMCS/
├── Controllers/        # MVC controllers for routing
├── Models/            # Data models and view models
├── Views/             # Razor views and layouts
├── Data/              # EF Core context and migrations
├── wwwroot/           # Static files (CSS, JS, images)
└── Services/          # Business logic and utilities
```

## Key Improvements in v3.0

This final release introduces significant enhancements:

- **Two-Stage Workflow** - Coordinators verify before managers approve
- **HR Portal** - Centralized rate management removes manual entry errors
- **Auto-Calculations** - Client-side calculation with server-side validation
- **Enhanced UI** - Modern glassmorphism design with responsive layouts
- **Advanced Reports** - Interactive charts for data-driven decisions

## Database Schema

The system uses Entity Framework Code-First with migrations for database management. Key entities include:

- **User** - Authentication and role assignments
- **Claim** - Monthly submissions with status tracking
- **Document** - File metadata for uploaded supporting files
- **HourlyRate** - Lecturer-specific payment rates configured by HR

## Security

- Password hashing with ASP.NET Core Identity
- Role-based authorization on all endpoints
- Secure file upload with type validation
- HTTPS enforcement in production
- CSRF protection on form submissions

## Contributing

This project was developed as part of the PROG6212 module. Contributions, issues, and feature requests are welcome for educational purposes.

## License

This project is developed for academic purposes at Varsity College.

## Contact

**Student:** ST10434047  
**Module:** PROG6212  
**Institution:** Varsity College

---

**Live Application:** [View Demo](https://st10434047-cmcs-crh5dhadeqc9a4fm.southafricanorth-01.azurewebsites.net)  
**Video Walkthrough:** [Watch on YouTube](https://youtu.be/kviag0wonsE)
