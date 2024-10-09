# MyMicroservicesSolution

## Overview

MyMicroservicesSolution is a robust microservices architecture project developed using .NET 8.0. It demonstrates a comprehensive approach to building distributed systems with efficient communication between services, utilizing Docker, RabbitMQ, Entity Framework Core, and more.

## Architecture

This project consists of the following microservices:

- **OrderService**: Manages customer orders, integrates with the ProductService to fetch product details.
- **ProductService**: Handles product information, inventory management, and interacts with the OrderService for product-related queries.
- **ApiGateway**: Acts as the entry point for all client requests, routing them to the appropriate services using Ocelot.
- **SharedKernel**: Contains common data transfer objects (DTOs) and messaging contracts shared across microservices.

## Technologies Used

- **Framework**: [.NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Microservices**: 
  - [ASP.NET Core Web API](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0)
  - [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) for data access
  - [RabbitMQ](https://www.rabbitmq.com/) for message broker communication between services
  - [Docker](https://www.docker.com/) for containerization
  - [Ocelot](https://ocelot.readthedocs.io/en/latest/) for API Gateway management
- **Database**: 
  - [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) for persistent storage
- **Container Orchestration**: 
  - [Kubernetes](https://kubernetes.io/) for deploying and managing containerized applications
- **CI/CD**: 
  - [GitHub Actions](https://github.com/features/actions) and [Bitbucket Pipelines](https://bitbucket.org/product/features/pipelines) for continuous integration and deployment

## Features

- **Decoupled Microservices**: Each service operates independently, allowing for better scalability and maintainability.
- **Asynchronous Communication**: Services communicate through RabbitMQ, enabling loose coupling and resilience.
- **API Gateway**: Ocelot routes client requests to the appropriate services, centralizing authentication and routing logic.
- **Database Management**: Each service manages its own database using Entity Framework Core, ensuring data ownership and integrity.
- **Containerized Deployment**: All services can be easily deployed and managed in Docker containers, simplifying the development and deployment process.
- **CI/CD Pipelines**: Automated build, test, and deployment processes using GitHub Actions and Bitbucket Pipelines.

## Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/<username>/MyMicroservicesSolution.git
