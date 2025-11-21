# Contract Monthly Claim System (CMCS)

> Enterprise-grade claim management platform built with ASP.NET Core 8, streamlining academic lecturer payments through intelligent automation and multi-stage approval workflows.

[![Live Demo](https://img.shields.io/badge/🌐_Live_Demo-Azure-0078D4?style=flat-square)](https://st10434047-cmcs-crh5dhadeqc9a4fm.southafricanorth-01.azurewebsites.net)
[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/License-Academic-green?style=flat-square)](LICENSE)

**[Try Live Demo](https://st10434047-cmcs-crh5dhadeqc9a4fm.southafricanorth-01.azurewebsites.net)** · **[Watch Video Walkthrough](https://youtu.be/kviag0wonsE)** · **[Report Issues](https://github.com/VCCT-PROG6212-2025-G3/CMCS-ST10434047/issues)**

---

## Why CMCS?

Academic institutions struggle with contract lecturer payment claims—manual calculations lead to errors, email-based approvals create bottlenecks, and paper trails vanish. CMCS solves this with automated rate calculations, transparent two-stage approvals, and complete audit trails.

**What makes it different:**
- Eliminates calculation errors through HR-configured hourly rates
- Reduces approval time from weeks to days with structured workflows
- Provides real-time visibility into claim status for all stakeholders
- Maintains comprehensive audit logs for financial accountability

---

## Features

### Core Functionality
- **Intelligent Claim Processing** – Automated payment calculations based on lecturer-specific hourly rates
- **Two-Stage Approval** – Programme Coordinator verification followed by Academic Manager final approval
- **Document Management** – Secure upload and storage of supporting documents (PDF, DOCX, XLSX)
- **Role-Based Access** – Four distinct user types with tailored permissions and workflows
- **Real-Time Tracking** – Live claim status updates with detailed approval history

### User Experience
- **Interactive Dashboards** – Role-specific views with actionable insights using Chart.js visualizations
- **Modern Interface** – Glassmorphism design language with responsive layouts for all devices
- **Bulk Operations** – Coordinators and Managers can process multiple claims efficiently
- **Advanced Reporting** – Export claim data and analytics to Excel/PDF formats

### Technical Excellence
- **Secure Authentication** – ASP.NET Core Identity with role-based authorization
- **Enterprise Architecture** – Clean separation of concerns with MVC pattern
- **Cloud-Ready** – Deployed on Azure App Service with automatic scaling
- **Database Management** – Entity Framework Core with SQL Server for reliable data persistence

---

## Quick Start

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- SQL Server LocalDB (included with Visual Studio)

### Installation

```bash
# Clone the repository
git clone https://github.com/VCCT-PROG6212-2025-G3/CMCS-ST10434047.git
cd CMCS-ST10434047

# Restore dependencies
dotnet restore

# Apply database migrations
dotnet ef database update

# Launch application
dotnet run
```

Open your browser to `https://localhost:5001` and log in with demo credentials below.

---

## Demo Accounts

| Role | Email | Password | Capabilities |
|------|-------|----------|--------------|
| **HR Administrator** | hr@cmcs.com | Password123! | User management, rate configuration, system reports |
| **Academic Manager** | manager@cmcs.com | Password123! | Final claim approvals, payment authorization |
| **Programme Coordinator** | programcoordinator@cmcs.com | Password123! | Claim verification, document review |
| **Lecturer** | lecturer@cmcs.com | Password123! | Claim submission, status tracking |

> **Security Note:** Change default passwords in production environments.

---

## System Architecture

### Technology Stack

**Backend:** C# 11 · .NET 8 · ASP.NET Core MVC · Entity Framework Core  
**Frontend:** Bootstrap 5 · Chart.js · jQuery · Font Awesome  
**Authentication:** ASP.NET Core Identity with role-based authorization  
**Database:** SQL Server LocalDB (development) · Azure SQL (production)  
**Cloud:** Microsoft Azure App Service with continuous deployment  

### Approval Workflow

```
Lecturer Submission
        ↓
Automated Rate Calculation (HR-configured)
        ↓
Programme Coordinator Review
        ├─→ Reject (request revision)
        └─→ Approve & Forward
                ↓
        Academic Manager Review
                ├─→ Reject (request revision)
                └─→ Approve for Payment
```

Each stage includes document validation, calculation verification, and detailed feedback mechanisms.

---

## Project Structure

```
CMCS/
├── Controllers/          # MVC controllers for routing and business logic
│   ├── ClaimsController.cs       # Claim submission and tracking
│   ├── ApprovalController.cs     # Coordinator and Manager workflows
│   ├── HRController.cs           # Admin and rate management
│   └── AccountController.cs      # Authentication and authorization
│
├── Models/              # Domain entities and view models
│   ├── Claim.cs         # Core claim entity with relationships
│   ├── User.cs          # Extended Identity user with roles
│   ├── HourlyRate.cs    # Lecturer payment rate configuration
│   └── Document.cs      # File metadata and storage references
│
├── Services/            # Business logic layer
│   ├── ClaimService.cs       # Claim processing and calculations
│   └── DocumentService.cs    # File handling and validation
│
├── Data/
│   ├── ApplicationDbContext.cs   # EF Core database context
│   └── Migrations/               # Database schema versions
│
├── Views/               # Razor view templates
│   ├── Claims/          # Lecturer claim interfaces
│   ├── Approval/        # Coordinator and Manager dashboards
│   ├── HR/              # Admin control panels
│   └── Shared/          # Layout templates and partials
│
└── wwwroot/             # Static assets
    ├── css/             # Custom styles with glassmorphism
    ├── js/              # Client-side validation and interactivity
    └── uploads/         # Secure document storage
```

---

## User Workflows

### Lecturer Journey
1. Log in and navigate to "Submit Claim"
2. Enter hours worked with automatic calculation preview
3. Upload supporting documents (timesheets, contracts)
4. Review calculated payment amount
5. Submit for Programme Coordinator verification
6. Track real-time status through approval pipeline

### Coordinator Responsibilities
1. Review pending claims in verification dashboard
2. Validate hours worked against supporting documents
3. Verify automated payment calculations
4. Approve valid claims to forward to Academic Manager
5. Reject claims with specific feedback for revision
6. Monitor verification metrics and team performance

### Manager Authority
1. Access final approval queue from Coordinators
2. Review verified claims and approval history
3. Examine historical patterns and anomalies
4. Authorize payments for finance processing
5. Reject for additional review when necessary
6. Generate comprehensive approval reports

### HR Administration
1. Create and manage user accounts with role assignment
2. Configure hourly rates per lecturer
3. Access system-wide analytics and dashboards
4. Export payment data for payroll integration
5. Audit claim history and approval chains
6. Generate financial reports for stakeholders

---

## Key Capabilities

### Automated Calculations
Payment amounts are computed server-side using the formula:  
**Total Payment = Hours Worked × Hourly Rate**

Hourly rates are configured by HR per lecturer, ensuring consistency and eliminating manual calculation errors. Real-time previews provide immediate feedback during claim submission.

### Document Security
- File type validation (PDF, DOCX, XLSX only)
- Size limits and virus scanning integration
- Encrypted storage with claim associations
- Secure download for authorized reviewers
- Automatic cleanup of orphaned files

### Analytics & Reporting
- Claim volume trends with time-series visualizations
- Approval rate analysis by coordinator and manager
- Payment total aggregations by month and department
- Export functionality for Excel and PDF formats
- Interactive Chart.js dashboards with drill-down capabilities

---

## Security & Compliance

| Feature | Implementation |
|---------|----------------|
| Authentication | ASP.NET Core Identity with bcrypt password hashing |
| Authorization | Role-based access control with claims-based policies |
| Transport Security | HTTPS enforced with TLS 1.3 in production |
| Data Protection | Encrypted connection strings and secure session cookies |
| File Upload Security | MIME type validation, extension whitelisting, size limits |
| CSRF Protection | Anti-forgery tokens on all state-changing operations |
| Audit Logging | Comprehensive tracking of all claim modifications |
| Input Validation | Server-side validation with sanitization |

---

## Development

### Running Locally
```bash
# Development mode with hot reload
dotnet watch run

# Production build
dotnet publish -c Release -o ./publish

# Database operations
dotnet ef migrations add MigrationName
dotnet ef database update
```

### Testing Strategy
- Unit tests for business logic in Services layer
- Integration tests for database operations
- End-to-end tests for critical workflows
- Security testing for authentication and authorization

---

## Deployment

**Production Environment:** Azure App Service  
**Database:** Azure SQL Database  
**CI/CD:** GitHub Actions with automated deployment  
**Monitoring:** Application Insights for performance tracking  
**Uptime SLA:** 99.9% availability guarantee  

Access the live application at: [https://st10434047-cmcs-crh5dhadeqc9a4fm.southafricanorth-01.azurewebsites.net](https://st10434047-cmcs-crh5dhadeqc9a4fm.southafricanorth-01.azurewebsites.net)

---

## Roadmap

**Current Version:** 1.0 – Core claim management with two-stage approval

**Upcoming Features:**
- Email notifications for claim status changes and approval requests
- Bulk approval interface for managers handling high volumes
- Advanced analytics with predictive insights and anomaly detection
- REST API for integration with external payroll systems
- Mobile applications for iOS and Android platforms
- Multi-language support for international institutions
- Dark mode theme with user preference persistence

---

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/enhancement-name`)
3. Commit your changes (`git commit -m 'Add new feature'`)
4. Push to your branch (`git push origin feature/enhancement-name`)
5. Open a Pull Request with detailed description

Please ensure code follows existing conventions and includes appropriate documentation.

---

## License

This project is developed for academic purposes as part of PROG6212 at Varsity College.

**Academic Use Permitted:**
- Study and learn from the implementation
- Use as reference for educational projects
- Modify for personal learning and experimentation

---

## Project Information

**Student ID:** ST10434047  
**Module:** PROG6212 – Programming 2B  
**Institution:** Varsity College  
**Academic Year:** 2025  

---

## Acknowledgments

Built with excellent tools from the open-source community:
- Microsoft for .NET 8 and Azure platform
- Bootstrap for responsive design framework
- Chart.js for data visualization components
- Entity Framework Core for data access
- Font Awesome for comprehensive icon library

---

## Support

**Need assistance?**
- [Submit an Issue](https://github.com/VCCT-PROG6212-2025-G3/CMCS-ST10434047/issues)
- [Watch Video Demo](https://youtu.be/kviag0wonsE)
- [Try Live Application](https://st10434047-cmcs-crh5dhadeqc9a4fm.southafricanorth-01.azurewebsites.net)

---

<div align="center">

**Built with ASP.NET Core 8 · Deployed on Azure · Designed for Academic Excellence**

⭐ Star this repository if you find it helpful!

</div>
