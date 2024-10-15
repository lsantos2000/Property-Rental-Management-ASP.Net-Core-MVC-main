# Property Rental Management

This is a Property Rental Management application built with ASP.NET Core MVC. It allows users to manage properties, rentals, tenants, and more.

It is used as a POC for Santos Real Estate Holdings Inc. of NB, Canada.

## Table of Contents

- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Running the Application](#running-the-application)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

## Getting Started

These instructions will help you set up and run the project on your local machine for development and testing purposes.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Installation

1. Clone the repository:

   ```sh
   git clone https://github.com/yourusername/Property-Rental-Management.git
   cd Property-Rental-Management
   ```

2. Restore the dependencies:

   ```sh
   dotnet restore
   ```

3. Update the database connection string in `appsettings.json`:

   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=PropertyRentalDB;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

4. Apply the database migrations:
   ```sh
   dotnet ef database update
   ```

## Running the Application

1. Build the project:

   ```sh
   dotnet build
   ```

2. Run the project:

   ```sh
   dotnet run
   ```

3. Open your browser and navigate to `https://localhost:5001`.

## Project Structure

- **Controllers/**: Contains the MVC controllers like [`AccessController`](Controllers/AccessController.cs), [`ApartmentsController`](Controllers/ApartmentsController.cs), etc.
- **Models/**: Contains the data models like [`Rental`](Models/Rental.cs), [`User`](Models/User.cs), etc.
- **Views/**: Contains the Razor views for the application.
- **wwwroot/**: Contains static files like CSS, JavaScript, and images.
- **PropertyRentalDatabase/**: Contains the database files.

## Contributing

Contributions are welcome! Please read the [contributing guidelines](CONTRIBUTING.md) first.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
