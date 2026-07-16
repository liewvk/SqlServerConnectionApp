# SqlServerConnectionApp

A C# application for managing and testing SQL Server connections.

## Overview

This application provides a straightforward interface for connecting to SQL Server databases and executing queries. It simplifies the process of establishing secure connections to SQL Server instances and managing database operations programmatically.

## Features

- SQL Server connection management
- Query execution and testing
- Connection string configuration
- Support for Windows Authentication and SQL Server Authentication
- Error handling and connection diagnostics
- Query result display and export capabilities

## Requirements

- **.NET Framework or .NET Core runtime** (version 4.7.2 or higher recommended)
- **Visual Studio 2022** or later (Visual Studio 2026 recommended)
- **SQL Server 2016** or later (SQL Server 2019/2022 recommended)
- **Administrator privileges** for NuGet package installation

## Installation

### Step 1: Clone the Repository

```bash
git clone https://github.com/liewvk/SqlServerConnectionApp.git
cd SqlServerConnectionApp
```

### Step 2: Open in Visual Studio 2026

1. Launch Visual Studio 2026
2. Click **File** > **Open** > **Project/Solution**
3. Navigate to the cloned repository folder
4. Select the `.sln` file and click **Open**

### Step 3: Install Microsoft.Data.SqlClient

The application requires the `Microsoft.Data.SqlClient` NuGet package for SQL Server connectivity.

#### Using NuGet Package Manager (GUI):

1. In Visual Studio, go to **Tools** > **NuGet Package Manager** > **Manage NuGet Packages for Solution**
2. Click the **Browse** tab
3. Search for `Microsoft.Data.SqlClient`
4. Select the latest stable version
5. Check the project checkbox and click **Install**
6. Review the license terms and click **I Accept**

#### Using Package Manager Console:

1. Open **Tools** > **NuGet Package Manager** > **Package Manager Console**
2. Run the following command:

```powershell
Install-Package Microsoft.Data.SqlClient
```

#### Using .NET CLI:

If using .NET Core/.NET 5+, open a terminal in the project directory and run:

```bash
dotnet add package Microsoft.Data.SqlClient
```

### Step 4: Build the Solution

1. Go to **Build** > **Build Solution** (or press `Ctrl+Shift+B`)
2. Ensure all packages are restored and the build completes successfully

## Connecting to SQL Server

### Connection String Formats

#### Windows Authentication:
```
Server=YourServerName;Database=YourDatabaseName;Integrated Security=true;
```

#### SQL Server Authentication:
```
Server=YourServerName;Database=YourDatabaseName;User Id=YourUsername;Password=YourPassword;
```

#### Azure SQL Database:
```
Server=YourServerName.database.windows.net;Database=YourDatabaseName;User Id=YourUsername@YourServerName;Password=YourPassword;
```

### Configuration Steps

1. Open the application configuration file (typically `appsettings.json` or `App.config`)
2. Update the `ConnectionString` with your SQL Server details:
   - **Server**: Your SQL Server instance name or IP address
   - **Database**: Target database name
   - **User Id** / **Password**: Credentials (if not using Windows Authentication)
3. Save the configuration file

### Example Usage in Code

```csharp
using Microsoft.Data.SqlClient;

// Create connection using connection string
string connectionString = "Server=localhost;Database=MyDatabase;Integrated Security=true;";

using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();
    
    // Execute a query
    string query = "SELECT * FROM YourTable";
    SqlCommand command = new SqlCommand(query, connection);
    SqlDataReader reader = command.ExecuteReader();
    
    while (reader.Read())
    {
        // Process results
    }
}
```

## Getting Started

1. Clone the repository (see Installation section)
2. Open the solution in Visual Studio 2026
3. Install Microsoft.Data.SqlClient NuGet package
4. Configure your SQL Server connection string
5. Build the solution (`Ctrl+Shift+B`)
6. Run the application (`F5`)
7. Test your SQL Server connection

## Troubleshooting

### Common Issues

| Issue | Solution |
|-------|----------|
| "Cannot open database" error | Verify the database name and server connection string |
| "Login failed" error | Check username, password, and SQL Server Authentication is enabled |
| NuGet package not found | Ensure Visual Studio has internet connection; try clearing NuGet cache |
| Connection timeout | Increase connection timeout value or check firewall/network settings |
| "Microsoft.Data.SqlClient not found" | Reinstall the NuGet package and rebuild the solution |

### Enabling SQL Server Authentication

1. Open SQL Server Management Studio (SSMS)
2. Right-click your server instance and select **Properties**
3. Go to **Security** tab
4. Select **SQL Server and Windows Authentication mode**
5. Restart the SQL Server service

### Firewall Configuration

- Ensure SQL Server port **1433** (default) is open in Windows Firewall
- For remote connections, configure your network firewall accordingly

## License

This project is provided as-is for development and testing purposes.

## Support

For issues, questions, or contributions, please open an issue on the GitHub repository or contact the maintainers.

---

**Last Updated**: July 2026
