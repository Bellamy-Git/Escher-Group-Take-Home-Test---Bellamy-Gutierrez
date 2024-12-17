# Stage 1: Build the Application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy the entire source folder
COPY . ./

# Restore dependencies using the solution file
RUN dotnet restore "EscherGroupTakeHomeTest/Escher Group Take Home Test - Bellamy Gutierrez.sln"

# Publish the application
RUN dotnet publish "EscherGroupTakeHomeTest/Escher Group Take Home Test - Bellamy Gutierrez.csproj" -c Release -o out

# Stage 2: Run the Application
FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app

# Copy the build output from Stage 1
COPY --from=build /app/out ./

# Run the application
ENTRYPOINT ["dotnet", "Escher Group Take Home Test - Bellamy Gutierrez.dll"]
