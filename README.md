# E-Commerce Microservices Project

A modern e-commerce platform built with microservices architecture using .NET 9.

## Project Overview

This project implements a microservices-based e-commerce solution with separate services for:

- **Basket Service**: Manages shopping cart functionality with Redis-based persistence
- **Catalog Service**: Handles product catalog management
- **Common Building Blocks**: Shared components across services

## Key Features

- Domain-Driven Design (DDD) approach with rich domain entities
- Event-driven architecture using MediatR for domain events
- Microservices communication via gRPC
- Redis for distributed caching and basket storage
- Clean Architecture implementation in each service

## Technical Stack

- .NET 9.0
- gRPC for inter-service communication
- Redis with StackExchange.Redis
- Newtonsoft.Json for serialization
- Entity pattern with domain events



## Getting Started

1. Ensure .NET 9 SDK is installed
2. Redis server must be running for basket functionality
3. Build the solution: `dotnet build`
4. Run services individually or using Docker Compose (if configured)

## Development Notes

- The project uses record types for DTOs (Data Transfer Objects)
- Custom JSON resolver for handling private setters
- Entity base class provides domain event handling capabilities
- Each service follows Clean Architecture principles
