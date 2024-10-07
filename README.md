# Power Trade Aggregation Console Application

## Project Description

This is a console application written in C# using .NET 8. The application processes and aggregates power positions for the next day based on energy trade data (power trades). The power positions are aggregated by hour and saved into a CSV file.

### Features:
- Processes power trades for the day-ahead.
- Aggregates energy volumes by hour.
- Saves the result in a CSV file.
- Configurable retry policy for handling failures during data extraction.
- Flexible configuration via `appsettings.json`.

## Requirements

- [.NET 8 SDK or higher](https://dotnet.microsoft.com/download/dotnet/8.0)
- C#-compatible IDE (Visual Studio, Visual Studio Code, Rider)

