# MediFlow-Hospital-Appointment-Management-System

![.NET 8](https://img.shields.io/badge/.NET-8-blue?logo=dotnet)
![SQL Server](https://img.shields.io/badge/Database-SQLServer-lightgrey?logo=microsoftsqlserver)
![Redis](https://img.shields.io/badge/Cache-Redis-red?logo=redis)
![JWT](https://img.shields.io/badge/Auth-JWT-green?logo=jsonwebtokens)
![FluentValidation](https://img.shields.io/badge/Validation-FluentValidation-orange)
![AutoMapper](https://img.shields.io/badge/Mapping-AutoMapper-yellow)
![Hangfire](https://img.shields.io/badge/Jobs-Hangfire-brightgreen)
![SignalR](https://img.shields.io/badge/Realtime-SignalR-orange)
![Serilog](https://img.shields.io/badge/Logging-Serilog-blue)
![Swagger](https://img.shields.io/badge/Docs-Swagger-yellow?logo=swagger)
![QuestPDF](https://img.shields.io/badge/PDF-QuestPDF-lightblue)
![MailKit](https://img.shields.io/badge/Email-MailKit-lightgrey)


Enterprise-level Hospital Appointment &amp; Management System built with ASP.NET Core 8 Web API, Clean Architecture, CQRS, LINQ, EF Core, Redis, JWT Authentication, AutoMapper, FluentValidation, Hangfire, SignalR, Serilog, Swagger.

MediFlow — Hospital Appointment & Management System
Enterprise-level ASP.NET Core 8 Web API Project  
Built with Clean Architecture (3-layer pattern), following industry best practices like CQRS, LINQ, Repository & Unit of Work.

About:
MediFlow is a hospital appointment & patient management REST API designed to solve real-world healthcare workflow problems. It streamlines doctor scheduling, patient registration, appointment booking, billing, notifications, and reporting — built to handle production-grade challenges like conflict detection, background reminders, caching, and secure authentication.

⚙️ Tech Stack
ASP.NET Core 8 — Web API framework

Entity Framework Core 8 — ORM & migrations

SQL Server — Database

Redis — Caching

ASP.NET Core Identity + JWT — Authentication

FluentValidation — Validation

AutoMapper — DTO ↔ Entity mapping

Hangfire — Background jobs

SignalR — Real-time notifications

Serilog + Seq — Logging

Swagger — API docs

QuestPDF — PDF export

MailKit — Email service

🏗️ Architecture
API Layer → Controllers, Middlewares, Swagger

Service Layer → Business logic, DTOs, Validators, AutoMapper, CQRS

Repository Layer → EF Core DbContext, Repositories, Unit of Work

Domain Layer → Entities, Enums, Constants
