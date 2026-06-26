# TODO API — Back-End Challenge

A simple TODO REST API built with ASP.NET Core (.NET 10). Users can register, authenticate, and manage their own tasks.

## Tech Stack

- .NET 10 / ASP.NET Core (Minimal APIs)
- C# with nullable reference types enabled

## Data Model

The schema follows the ER diagram in [`Artifacts/DER .png`](Artifacts/DER%20.png).

### `User` ([`Entities/User.cs`](Entities/User.cs))

| Field          | Type    | Notes        |
| -------------- | ------- | ------------ |
| `Id`           | `Guid`  | Primary key  |
| `Name`         | `string`|              |
| `PasswordHash` | `string`|              |
| `Mail`         | `string`|              |
| `Age`          | `int`   |              |
| `IsActive`     | `bool`  |              |

A `User` has many `Task`s (one-to-many).

### `Task` ([`Entities/Task.cs`](Entities/Task.cs))

| Field       | Type         | Notes                  |
| ----------- | ------------ | ---------------------- |
| `Id`        | `int`        | Primary key            |
| `UserId`    | `Guid`       | Foreign key → `User`   |
| `TaskName`  | `string`     |                        |
| `Status`    | `TaskStatus` | Enum                   |
| `CreatedAt` | `DateTime`   |                        |
| `UpdatedAt` | `DateTime`   |                        |

### `TaskStatus` ([`Enum/TaskStatus.cs`](Enum/TaskStatus.cs))

`Pending` · `InProgress` · `Completed` · `Cancelled`

## Routes

| Method  | Route                 | Description                            |
| ------- | --------------------- | -------------------------------------- |
| `POST`  | `/register`           | Register a new user                    |
| `POST`  | `/auth`               | Authenticate a user                    |
| `GET`   | `/todos`              | Return all tasks linked to the user    |
| `POST`  | `/todos`              | Create a task                          |
| `PUT`   | `/todos/:id`          | Update a task by id                    |
| `PATCH` | `/todos/status/:id`   | Update a task's status by id           |

## Getting Started

```bash
dotnet run
```
