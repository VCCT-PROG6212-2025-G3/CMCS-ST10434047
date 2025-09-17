# Contract Monthly Claim System (CMCS)

![Language](https://img.shields.io/badge/language-C%23-blueviolet)
![Framework](https://img.shields.io/badge/framework-.NET-blue)
![Database](https://img.shields.io/badge/database-Azure%20SQL-orange)
![Status](https://img.shields.io/badge/status-In%20Development-yellow)

A robust web-based application designed to streamline the monthly contract claims process for lecturers. This system, built for the PROG6212 module, provides an intuitive interface for lecturers to log hours and for academic managers to review and approve claims efficiently.

## Table of Contents
* [Introduction](#introduction)
* [Key Features](#key-features)
* [Technology Stack](#technology-stack)
* [Database Design](#database-design)
* [Installation](#installation)
* [Usage](#usage)
* [Contact](#contact)

---

## Introduction

The **Contract Monthly Claim System (CMCS)** solves the administrative challenge of managing monthly claims for contract lecturers. It replaces manual or disjointed processes with a centralized, user-friendly platform. The application is built using ASP.NET MVC, follows the Model-View-Controller architecture, and is backed by a scalable Azure SQL database. The front-end is designed to be responsive and accessible across various device sizes.

---

## Key Features

* **User Authentication**: Secure login for Lecturers, Programme Coordinators, and Academic Managers.
* **Claim Submission**: Lecturers can easily create, submit, and track their monthly claims, detailing hours worked and hourly rates.
* **Approval Workflow**: A streamlined process for Programme Coordinators and Academic Managers to review, approve, or reject claims.
* **Data Management**: Centralized storage and management of all claims, user data, and related information.
* **Responsive UI**: A clean and intuitive user interface designed with Figma and built to adapt to different screen sizes.

---

## Technology Stack

This project leverages a modern, robust tech stack to ensure scalability and maintainability:

* **Backend**: C# with ASP.NET MVC
* **Database**: Azure SQL
* **UI/UX Design**: Figma
* **Architecture**: Entity Framework, Model-View-Controller (MVC)

---

## Database Design

The database is structured using an Entity-Relationship model, where C# models directly map to Azure SQL tables. This approach leverages the power of Entity Framework for seamless data access and manipulation. The core entities include:

* **Lecturer**: Stores personal details and credentials.
* **Claim**: Contains all information about a specific claim, including hours, rates, and status.
* **ProgrammeCoordinator**: Manages assigned lecturers and reviews claims.
* **AcademicManager**: Holds final approval authority over claims.

Below is the Entity-Relationship Diagram (ERD) for the system.

<img width="1582" alt="Entity-Relationship Diagram for the Contract Monthly Claim System" src="https://github.com/user-attachments/assets/d353f8ae-7d5e-415c-8f22-37b125a4cffe" />

Below is the Project Plan.

<img width="3242" height="1196" alt="Project Outline" src="https://github.com/user-attachments/assets/a8d3b1f8-c407-4ee4-b5ea-34b43daccc36" />
" />

---

## Installation

To get a local copy up and running, follow these simple steps.

1.  **Prerequisites**
    * .NET SDK (Version X.X.X)
    * Visual Studio 2022 or later
    * An Azure SQL instance or local SQL Server
2.  **Clone the repo**
    ```sh
    git clone [https://github.com/VCCT-PROG6212-2025-G3/CMCS-ST10434047.git](https://github.com/your_username/CMCS-ST10434047.git)
    ```
3.  **Configure the database**
    * Open `appsettings.json`.
    * Update the `ConnectionString` with your database credentials.
4.  **Run the application**
    * Open the solution in Visual Studio and press `F5` to build and run the project.

---

## Usage

*To be added: Include screenshots and instructions on how to use the application's key features.*

---

## Contact

*Jamie Abrahams* - *st10434047@vcconnect.edu.za*

Project Link: [https://github.com/VCCT-PROG6212-2025-G3/CMCS-ST10434047](https://github.com/your_username/CMCS-ST10434047)
