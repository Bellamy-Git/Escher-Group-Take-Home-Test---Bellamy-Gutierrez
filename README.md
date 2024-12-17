# Escher Group Take Home Coding Test

## Prerequisites
* Docker
* .NET 6 SDK

## Unit Tests Project Requirements
* NUnit: 3.13.3
* NUnit3TestAdapter: 4.5.0
* Microsoft.NET.Test.Sdk: 4.5.0

# Docker Instructions
## Build and Run
1. Clone the repository:
   ```
   git clone https://github.com/Bellamy-Git/Escher-Group-Take-Home-Test---Bellamy-Gutierrez.git

2. Navigate to the directory:
   ```
   cd Escher-Group-Take-Home-Test---Bellamy-Gutierrez

3. Build the Docker Image:
   ```
   docker build -t escher-group-app .

4. Run the program using bind mounting:
   ```
   docker run -it -v "C:\people:/app/people" escher-group-app

# Non-Docker Instructions
## Build and Run
1. Clone the repository:
   ```
   git clone https://github.com/Bellamy-Git/Escher-Group-Take-Home-Test---Bellamy-Gutierrez.git

2. Navigate to the directory:
   ```
   cd Escher-Group-Take-Home-Test---Bellamy-Gutierrez/EscherGroupTakeHomeTest

3. Build the project:
   ```
   dotnet build

4. Run the project:
   ```
   dotnet run
   
## File Structure
```bash
Escher-Group-Take-Home-Test---Bellamy-Gutierrez/
├── .dockerignore
├── Dockerfile
├── EscherGroupTakeHomeTest/
│   ├── .gitattributes
│   ├── .gitignore
│   ├── Escher Group Take Home Test - Bellamy Gutierrez.csproj
│   ├── Escher Group Take Home Test - Bellamy Gutierrez.sln
│   ├── Program.cs
│   ├── BusinessRules.cs
│   ├── Person.cs
│   ├── PersonService.cs
│   ├── AppSettings.cs
│   └── AppSettings.json
│
└── BellamyEscherUnitTests/
    ├── BusinessRulesTests.cs
    └── Bellamy Escher Unit Tests.csproj
```
