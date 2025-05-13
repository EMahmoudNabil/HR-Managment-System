# HR Management System

A comprehensive Human Resources Management System built with ASP.NET Core MVC. The system provides an easy-to-use interface for managing employees, departments, and attendance.

## 📋 Table of Contents
- [Features](#features)
- [Requirements](#requirements)
- [Installation](#installation)
- [Project Structure](#project-structure)
- [Documentation](#documentation)
- [Contributing](#contributing)

## ✨ Features
- Employee Management (CRUD operations)
- Department Management
- Attendance Tracking
- Role-based Access Control
- User-friendly Interface
- Reports and Statistics
- Data Validation
- Error Handling

## 🛠 Requirements
- .NET Core 7.0 or later
- SQL Server
- Visual Studio 2022 or later
- Git

## 🚀 Installation

1. Clone the repository:
```bash
git clone https://github.com/emahmoudnabil/HR-Management-System-.git
```

2. Open the project in Visual Studio

3. Update the `appsettings.json` file with your database connection string:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HRManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

4. Run the following commands in Package Manager Console:
```bash
Update-Database
```

5. Run the project

## 📁 Project Structure
```
WebApplication1/
├── Areas/           # Project Areas
├── Controllers/     # MVC Controllers
├── Data/           # Database Context
├── Helpers/        # Helper Classes
├── Mapping/        # Object Mappings
├── Models/         # Data Models
├── Services/       # Business Services
├── ViewModels/     # View Models
├── Views/          # Razor Views
└── wwwroot/        # Static Files
```

## 📚 Documentation
- [Project Demo Video](https://youtu.be/8OGdXcPMJus)

## 🤝 Contributing
Contributions are welcome! Please follow these steps:
1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 Support
If you encounter any issues or have questions, please open a new issue in the project repository.
