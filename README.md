# TrueLayer_Pokemon
Pokemon repo for TrueLayer - This is a simple Rest API, using mediator pattern and CQRS (well there is no command currently in the scope).

# Prerequisite
- Docker - https://docs.microsoft.com/en-us/virtualization/windowscontainers/quick-start/set-up-environment?tabs=Windows-Server

# Architecture & Components
- Mediator Pattern
- CQRS pattern (no commands added yet, since it's out of scope for now)
- Basic Swagger Implementation
- Error Handling Middleware
- Docker Impementation
- Unit Tests
- Integration Tests


# External Sources
- https://funtranslations.com/api/shakespeare
- https://pokeapi.co/
- https://funtranslations.com/api/yoda

# Endpoints
- Swagger: http://localhost:5000/swagger/index.html
- Pokemon: http://localhost:5000/pokemon/
- Translated Pokemon: http://localhost:5000/pokemon/translated/

# Documentation
- https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/visual-studio-tools-for-docker?view=aspnetcore-5.0

# Frameworks:
- Dotnetcore 5.0
- Docker 20.10.6

# How to run the solution:
- Clone the repo into a folder of your choice: git clone https://github.com/ashahzeb/TrueLayer_Pokemon.git
- Open powershell from the root folder eg:TrueLayer 
- Type "docker-compose up"

The endpoint will be up and running. 
curl --location --request GET 'http://localhost:5000/Pokemon/mewtwo' \
--header 'Content-Type: application/json'

# Running Unit Tests 
- Navigate to the unit test folder: cd .UnitTests
- Type dotnet test, to run unit tests included.

# What else can be done to improve code quality
- Authorization
- API Versioning
- Caching
- Logging
- Health Checks
- Acceptance Tests
- Non-sunshine test scenarios
- HTTPs setup in docker


