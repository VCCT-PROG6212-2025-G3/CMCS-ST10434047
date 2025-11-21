<div align="center">

# 📋 Contract Monthly Claim System (CMCS)

### *Streamlining Academic Claims with Modern Cloud Architecture*

[![Local Demo](https://img.shields.io/badge/demo-live%20on%20device-0089D6?style=for-the-badge&logo=microsoft-azure)]
[![.NET Version](https://img.shields.io/badge/.NET-8.0-blueviolet?style=for-the-badge&logo=.net)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen?style=for-the-badge)](https://st10434047-cmcs-crh5dhadeqc9a4fm.southafricanorth-01.azurewebsites.net/)

**[📖 Documentation](#documentation)** • **[🎥 Video Walkthrough](https://youtu.be/kviag0wonsE)** • **[🐛 Report Issue](https://github.com/VCCT-PROG6212-2025-G3/CMCS-ST10434047/issues)**

---

## 🎥 Project Presentation

<div align="center">
  <a href="https://youtu.be/kviag0wonsE" target="_blank">
    <img src="https://img.youtube.com/vi/kviag0wonsE/maxresdefault.jpg" alt="CMCS Presentation" width="80%" style="border-radius: 10px; box-shadow: 0 4px 8px rgba(0,0,0,0.1);">
  </a>
  
  **[▶️ Watch the Full Walkthrough](https://youtu.be/kviag0wonsE)**
  
  *Discover the two-stage approval workflow, role-based security, and automated calculation features*
</div>

</div>

---

## 🎯 Overview

CMCS transforms the chaotic process of managing monthly contract lecturer claims into a streamlined, automated workflow. Built for academic institutions using **ASP.NET Core MVC** and **Entity Framework Core**, this system ensures accuracy, accountability, and efficiency across the entire claims lifecycle.

### ⚠️ Registration & Compliance Strategy
This system implements a **Hybrid Registration Model** to strictly control user access while maintaining usability:
> **"Registration constitutes a request for access, which is only granted once HR manually updates the Hourly Rate."**

While the registration page allows users to create profiles, **claim submission functionality is hard-locked** by default. A lecturer cannot submit any claims until an HR Administrator explicitly validates the user and assigns a non-zero Hourly Rate in the administrative backend. This ensures no unauthorized claims can ever enter the system.

### The Challenge

Traditional claim management faced critical issues:
- 📝 **Manual calculations** prone to human error
- 🔄 **Unclear approval workflows** causing delays
- 📧 **Email-based tracking** with no visibility
- 💰 **Rate inconsistencies** across departments
- 📁 **Lost documentation** and audit trail gaps

### The Solution

A comprehensive digital platform featuring:
- ⚡ **Automated calculations** based on HR-configured rates
- 🔐 **Two-stage approval** with verification and final approval
- 📊 **Real-time dashboards** showing claim status
- 👥 **Role-based access** for Lecturers, Coordinators, Managers, and HR
- 📈 **Interactive reports** with Chart.js visualizations
- 🎨 **Modern UI** with glassmorphism design language

---

## ✨ Key Features

<table>
<tr>
<td width="50%">

### 🎨 **User Experience**
- Intuitive claim submission wizard
- Real-time calculation previews
- Drag-and-drop document uploads
- Status tracking with notifications
- Mobile-responsive design
- Interactive data visualizations

</td>
<td width="50%">

### 🔧 **System Capabilities**
- **Register + HR Approval Access Control**
- Two-stage approval workflow
- Automated payment calculations
- Secure document management
- Comprehensive audit trails
- Excel/PDF report generation

</td>
</tr>
</table>
---

## 🔑 Demo Credentials

Access the system with these pre-configured test accounts:

### 👨‍💼 HR / System Administrator
```
Name: System Administrator
Email: hr@cmcs.com
Password: Password123!
```
*Full system access: user management, hourly rate configuration, system-wide reports*

### 👔 Academic Manager
```
Name: Academic Manager
Email: manager@cmcs.com
Password: Password123!
```
*Final approval authority: review verified claims, bulk approvals, payment authorization*

### 📊 Programme Coordinator
```
Name: System Coordinator
Email: programcoordinator@cmcs.com
Password: Password123!
```
*Verification responsibilities: validate lecturer claims, request corrections, forward to manager*

### 👨‍🏫 Lecturer
```
Name: John Doe
Email: lecturer@gmail.com
Password: Password123!
```
*Standard user access: submit claims, upload documents, track approval status*

> ⚠️ **Note:** All demo accounts use `Password123!` for testing purposes. Change credentials in production environments.

---

## 🏗️ System Architecture

### Technology Stack

| Layer | Technologies |
|:------|:-------------|
| **Backend** | C# 11, .NET 8, ASP.NET Core MVC |
| **Authentication** | ASP.NET Core Identity with role-based authorization |
| **Database** | Entity Framework Core + SQL Server LocalDB |
| **Frontend** | Bootstrap 5, Chart.js, Font Awesome, jQuery |
| **Cloud Platform** | Microsoft Azure (App Service) |
| **File Storage** | Secure document upload with validation (.pdf, .docx, .xlsx) |

### Approval Flow

```
┌─────────────┐
│  Lecturer   │ Submits Claim + Documents
└──────┬──────┘
       │
       ▼
┌─────────────────┐
│ Auto-Calculate  │ System calculates total based on HR rates
└────────┬────────┘
         │
         ▼
   ┌──────────────────┐
   │   Coordinator    │ Stage 1: Verification
   │  (Verifies)      │
   └────────┬─────────┘
            │
    ┌───────┴────────┐
    │                │
    ▼                ▼
┌─────────┐    ┌──────────┐
│ Reject  │    │ Approve  │
│ (Revise)│    │ & Forward│
└─────────┘    └────┬─────┘
                    │
                    ▼
            ┌──────────────┐
            │   Manager    │ Stage 2: Final Approval
            │  (Approves)  │
            └──────┬───────┘
                   │
           ┌───────┴────────┐
           │                │
           ▼                ▼
      ┌─────────┐    ┌───────────┐
      │ Reject  │    │  Approve  │
      │(Revise) │    │(Payment)  │
      └─────────┘    └───────────┘
```

---

## 🚀 Quick Start

### Prerequisites

Ensure you have the following installed:

```bash
✅ .NET 8.0 SDK or later
✅ Visual Studio 2022 (recommended) or VS Code
✅ SQL Server LocalDB (included with Visual Studio)
✅ Git
```

### Installation

1️⃣ **Clone the repository**
```bash
git clone https://github.com/VCCT-PROG6212-2025-G3/CMCS-ST10434047.git
cd CMCS-ST10434047
```

2️⃣ **Restore dependencies**
```bash
dotnet restore
```

3️⃣ **Update the database**
```bash
dotnet ef database update
```
*This creates the database schema and seeds test data*

4️⃣ **Run the application**
```bash
dotnet run
```

5️⃣ **Open your browser**
```
https://localhost:5001
```

The application is ready to use with pre-configured test accounts! 🎉

---

## 📂 Project Structure

```
CMCS/
│
├── 📁 Controllers/
│   ├── HomeController.cs              # Main navigation
│   ├── ClaimsController.cs            # Claim submission & tracking
│   ├── ApprovalController.cs          # Coordinator & Manager workflows
│   ├── HRController.cs                # User & rate management
│   └── AccountController.cs           # Authentication
│
├── 📁 Models/
│   ├── Claim.cs                       # Claim entity
│   ├── User.cs                        # User entity with roles
│   ├── HourlyRate.cs                  # Lecturer payment rates
│   ├── Document.cs                    # File metadata
│   └── ViewModels/                    # DTOs for views
│
├── 📁 Services/
│   ├── ClaimService.cs                # Business logic
│   └── DocumentService.cs             # File handling
│
├── 📁 Data/
│   ├── ApplicationDbContext.cs        # EF Core context
│   └── Migrations/                    # Database migrations
│
├── 📁 Views/
│   ├── Claims/                        # Claim submission & tracking
│   ├── Approval/                      # Coordinator & Manager views
│   ├── HR/                            # Admin dashboards
│   └── Shared/
│       ├── _Layout.cshtml            # Master layout
│       └── _LoginPartial.cshtml      # Auth navigation
│
├── 📁 wwwroot/
│   ├── css/
│   │   └── site.css                  # Custom styles with glassmorphism
│   ├── js/
│   │   └── site.js                   # Client-side validation
│   ├── lib/                          # Bootstrap, Chart.js, jQuery
│   └── uploads/                      # Uploaded documents
│
├── 📄 Program.cs                      # Application entry point
├── 📄 appsettings.json               # Configuration
└── 📄 CMCS.csproj                    # Project file
```

---

## 🎨 Design Philosophy

CMCS embraces **modern enterprise design**:

- **Glassmorphism** - Contemporary frosted glass aesthetic with backdrop blur
- **Intuitive Navigation** - Role-specific dashboards with clear action paths
- **Data Visualization** - Chart.js graphs showing claim trends and approval rates
- **Responsive Design** - Optimized for desktop, tablet, and mobile devices
- **Accessibility** - WCAG 2.1 AA compliant with semantic HTML

---

## 💼 User Workflows

### For Lecturers 👨‍🏫
1. **Login** with your credentials
2. **Navigate** to "Submit Claim"
3. **Enter** hours worked for the month
4. **Upload** supporting documents (timesheets, contracts)
5. **Review** auto-calculated payment amount
6. **Submit** for verification
7. **Track** claim status in real-time

### For Programme Coordinators 📊
1. **Access** "Pending Verification" dashboard
2. **Review** lecturer submissions and documents
3. **Verify** accuracy of hours and calculations
4. **Approve** to forward to Academic Manager
5. **Reject** with feedback if corrections needed
6. **Monitor** verification metrics and trends

### For Academic Managers 👔
1. **View** "Pending Approval" queue
2. **Review** coordinator-verified claims
3. **Examine** historical claim patterns
4. **Approve** for payment processing
5. **Reject** if additional review needed
6. **Generate** approval reports

### For HR Administrators 👨‍💼
1. **Manage** user accounts and roles
2. **Configure** hourly rates per lecturer
3. **View** system-wide analytics
4. **Generate** payment reports
5. **Export** data for payroll integration
6. **Audit** claim history and approvals

---

## 📊 Key Capabilities

### Automated Payment Calculation
```
Total Payment = Hours Worked × HR-Configured Hourly Rate
```
- Rates set by HR per lecturer
- Real-time calculation preview
- Server-side validation
- Prevents manual entry errors

### Document Management
- Supported formats: PDF, DOCX, XLSX
- Secure file upload and storage
- Automatic file validation
- Persistent storage with claims
- Download capability for reviewers

### Reporting & Analytics
- Claim submission trends over time
- Approval rate metrics by coordinator/manager
- Payment totals by month/department
- Export to Excel/PDF
- Interactive Chart.js visualizations

---

## 🧪 Testing

### Local Development
```bash
# Run the application
dotnet run

# Run with hot reload
dotnet watch run

# Build for production
dotnet publish -c Release
```

### Production Deployment
The application is deployed to **Azure App Service**:
- Automatic scaling based on traffic
- SSL/TLS certificates managed by Azure
- Continuous deployment from GitHub
- 99.9% uptime SLA

---

## 🔐 Security Features

| Feature | Implementation |
|:--------|:--------------|
| **Authentication** | ASP.NET Core Identity with hashed passwords |
| **Authorization** | Role-based access control (RBAC) |
| **File Upload** | Type validation, size limits, virus scanning |
| **Data Protection** | Encrypted connection strings, secure cookies |
| **HTTPS** | Enforced in production with TLS 1.2+ |
| **CSRF Protection** | Anti-forgery tokens on all forms |
| **Audit Trail** | Comprehensive logging of all actions |

---

## 🗺️ Roadmap

### Completed ✅
- [x] Two-stage approval workflow
- [x] Role-based authentication
- [x] Automated payment calculations
- [x] Document upload and management
- [x] Interactive dashboards and reports
- [x] Mobile-responsive design

### Planned 📋
- [ ] Email notifications for claim status changes
- [ ] Bulk claim approval for managers
- [ ] Advanced analytics with predictive insights
- [ ] Integration with payroll systems
- [ ] Dark mode toggle

---

## 🤝 Contributing

We welcome contributions! Here's how you can help:

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/AmazingFeature`)
3. **Commit** your changes (`git commit -m 'Add AmazingFeature'`)
4. **Push** to the branch (`git push origin feature/AmazingFeature`)
5. **Open** a Pull Request

Please ensure your code follows the existing style and includes appropriate tests.

---

## 📄 License

This project is developed for academic purposes as part of the **PROG6212** module at Varsity College.

```
Academic Use - You can:
✅ Study the codebase
✅ Learn from implementation
✅ Use for educational projects
✅ Modify for personal learning
```

---

## 📞 Support & Contact

<div align="center">

### Need Help?

[![GitHub Issues](https://img.shields.io/badge/Issues-Report%20Bug-red?style=for-the-badge&logo=github)](https://github.com/VCCT-PROG6212-2025-G3/CMCS-ST10434047/issues)
[![Video Demo](https://img.shields.io/badge/Video-Watch%20Demo-red?style=for-the-badge&logo=youtube)](https://youtu.be/kviag0wonsE)
[![Live Demo](https://img.shields.io/badge/Demo-Try%20Live-blue?style=for-the-badge&logo=microsoft-azure)](https://st10434047-cmcs-crh5dhadeqc9a4fm.southafricanorth-01.azurewebsites.net)

### Project Information

**Student ID:** ST10434047  
**Module:** PROG6212  
**Institution:** Varsity College

</div>

---

## 🙏 Acknowledgments

- **Microsoft** for .NET 8 and Azure platform
- **Bootstrap Team** for the responsive framework
- **Chart.js** for interactive data visualizations
- **Font Awesome** for the comprehensive icon library
- **Entity Framework Core** for seamless database operations

---

<div align="center">

### ⭐ Star this repo if you find it helpful!

**Built for Academic Excellence**

*Developed with ASP.NET Core 8 • Deployed on Azure • Designed for Efficiency*

[⬆ Back to Top](#-contract-monthly-claim-system-cmcs)

</div>
