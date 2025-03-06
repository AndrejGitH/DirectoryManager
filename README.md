# Directory Manager

![DirectoryManagerGif](https://github.com/user-attachments/assets/dbf58ecd-ea4a-4d5e-8f19-a9d5e8f294b4)

## Overview
`DirectoryManager` is a console application built for managing directory structures. It supports the following functionalities:
- Printing directory structures with proper indentation.
- Listing all unique file extensions in a directory.
- Serializing directory data to JSON.
- Deserializing JSON files to reconstruct and print directory structures.

The project follows best practices, including Dependency Injection (DI), Dependency Inversion Principle (DIP), and Interface Segregation Principle (ISP), ensuring extensibility and testability. It uses an iterative Depth-First Search (DFS) approach to avoid stack overflow issues in deep directory structures.

## Prerequisites
- **Visual Studio**: Version 2022 (17.13 or later) with the .NET Desktop Development workload.
- **.NET**: .NET 8.0 SDK (Target Framework: `net8.0`).
- **NUnit Test Adapter**: Required for running unit tests (included via NuGet).

## Setup Instructions
1. **Clone the Repository**:
git clone https://github.com/AndrejGitH/DirectoryManager.git
2. **Open the Solution**:
- Navigate to the cloned folder and open `DirectoryManager.sln` in Visual Studio 2022.
3. **Restore NuGet Packages**:
- Visual Studio should automatically restore packages (e.g., `NUnit`, `NUnit3TestAdapter`, `Microsoft.NET.Test.Sdk`). If not, go to **Tools > NuGet Package Manager > Restore**.
4. **Build the Solution**:
- Build the project using **Build > Build Solution** (or Ctrl+Shift+B).

## Usage
1. **Run the Application**:
- Press F5 or go to **Debug > Start Debugging** to launch the console app.
- When prompted, provide a directory path (e.g., `C:\Users\YourName\Desktop\TestPrint`) or a JSON file path (e.g., `myFolder.json`).
2. **Choose an Option**:
- `1`: Print directory structure.
- `2`: Print all unique file extensions.
- `3`: Serialize directory structure to JSON.
- `4`: Deserialize and print a JSON file.
- Type `exit` to change the path or close the application.
