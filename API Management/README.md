# Azure API Management: FlightLogs and Employee APIs

## Description
This project consists of two APIs hosted in Azure App Services and managed through Azure API Management:
1. FlightLogs API: Manages flight log information
2. Employee API: Manages employee data

Both APIs provide CRUD (Create, Read, Update, Delete) operations and use JSON files for data storage.

## Architecture
- APIs: Hosted in Azure App Services
- API Management: Azure API Management
- Data Storage: JSON files (Note: For production, consider using a more robust storage solution)

## APIs Overview

### FlightLogs API

#### Endpoints
- GET /api/flightlogs: Retrieve all flight logs
- GET /api/flightlogs/{flightNumber}: Retrieve a specific flight log
- POST /api/flightlogs: Create a new flight log
- PUT /api/flightlogs/{flightNumber}: Update an existing flight log
- DELETE /api/flightlogs/{flightNumber}: Delete a flight log

#### Data Model (FlightLog)
- FlightNumber (int)
- DepartureAirportCode (string)
- ArrivalAirportCode (string)
- DepartureDatetime (DateTime)
- ArrivalDatetime (DateTime)
- AirlineName (string)
- FlightDurationMinutes (int)

### Employee API

#### Endpoints
- GET /api/employee: Retrieve all employees
- GET /api/employee/{id}: Retrieve a specific employee
- POST /api/employee: Create a new employee
- PUT /api/employee/{id}: Update an existing employee
- DELETE /api/employee/{id}: Delete an employee

#### Data Model (Employee)
- EmployeeId (int)
- FirstName (string)
- LastName (string)
- Salary (decimal)
- HireDate (DateTime)
- Department (string)
- EmployeeStatus (string)

## Setup and Deployment

1. Deploy both APIs to separate Azure App Services.
2. Set up Azure API Management.
3. Import both APIs into Azure API Management.
4. Configure API Management policies as needed (e.g., rate limiting, caching).

## Configuration

Ensure the following configurations are set for each API:

1. Set the `_filePath` in both controllers to point to the correct JSON file location in the App Service environment.
2. Configure CORS policies if the APIs will be accessed from web applications.
3. Set up proper authentication and authorization in Azure API Management.

## Development

To run these APIs locally:

1. Clone the repository.
2. Ensure you have .NET 6.0 SDK or later installed.
3. Navigate to each API's directory.
4. Run `dotnet restore` to restore dependencies.
5. Run `dotnet run` to start the API.

## Testing

You can test the APIs using tools like Postman or curl. Here are some example requests:

### FlightLogs API

```bash
# Get all flight logs
GET https://your-api-management-url/flightlogs

# Get a specific flight log
GET https://your-api-management-url/flightlogs/1001

# Create a new flight log
POST https://your-api-management-url/flightlogs
Content-Type: application/json

{
  "FlightNumber": 1002,
  "DepartureAirportCode": "LAX",
  "ArrivalAirportCode": "JFK",
  "DepartureDatetime": "2023-06-01T10:00:00",
  "ArrivalDatetime": "2023-06-01T18:00:00",
  "AirlineName": "Example Airlines",
  "FlightDurationMinutes": 360
}
```

### Employee API

```bash
# Get all employees
GET https://your-api-management-url/employee

# Get a specific employee
GET https://your-api-management-url/employee/1

# Create a new employee
POST https://your-api-management-url/employee
Content-Type: application/json

{
  "EmployeeId": 2,
  "FirstName": "Jane",
  "LastName": "Doe",
  "Salary": 75000,
  "HireDate": "2023-01-15",
  "Department": "IT",
  "EmployeeStatus": "Active"
}
```

## Security Considerations

- Implement proper authentication and authorization in Azure API Management.
- Use HTTPS for all API communications.
- Consider encrypting sensitive data in the JSON files or moving to a more secure database solution.
- Implement proper error handling and logging, ensuring no sensitive information is exposed in error messages.

## Future Improvements

- Migrate from JSON file storage to a database system like Azure SQL Database or Cosmos DB.
- Implement caching in Azure API Management to improve performance.
- Add API versioning to manage future updates.
- Implement more robust error handling and logging.

## Additional Resources

- [Azure App Service documentation](https://docs.microsoft.com/en-us/azure/app-service/)
- [Azure API Management documentation](https://docs.microsoft.com/en-us/azure/api-management/)
- [ASP.NET Core Web API documentation](https://docs.microsoft.com/en-us/aspnet/core/web-api)
