# TalkWave

TakkWave - is a web application for multi-user text communication. The application provides the ability to register, authorise, send, delete and edit messages, as well as view message history.

## Features 🌟

- **Registration and authorisation**
- **Chats**

## Technologies 🛠️

### Backend

- **Language**: C#
- **Framework**: .NET, ASP.NET WebAPI, RabbitMQ, FluentValidation, AutoMapper, Refit
- **Database**: PostgreSQL (or SQL Server)
- **ORM**: Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)

### Frontend

- **Language**: TypeScript(JavaScript)
- **Library**: React
- **Styles**: CSS Modules, MaterialUI
- **Design**: FSD(https://feature-sliced.design/)
- **State**: Redux Toolkit
- **Routing**: React Router

### Infrastructure

- **Containerization**: Docker
- **CI/CD**: GitHub Actions (or GitLab CI)
- **Hosting**: Azure, AWS or any other cloud service

## Running the Project with Docker 🐳

### Prerequisites

- Ensure Docker and Docker Compose are installed on your system.
- Verify the required versions specified in the Dockerfiles:
  - .NET SDK: 7.0
  - Node.js: 22.13.1-slim

### Environment Variables

- The `database` service requires the following environment variables:
  - `POSTGRES_USER`: Database username (default: `user`)
  - `POSTGRES_PASSWORD`: Database password (default: `password`)
  - `POSTGRES_DB`: Database name (default: `talkwave`)

### Build and Run Instructions

1. Clone the repository and navigate to the project root directory.
2. Execute the following command to build and start the services:

   ```bash
   docker-compose up --build
   ```

3. Access the services via the following ports:
   - `talkwave_chat_api`: [http://localhost:8080](http://localhost:8080)
   - `talkwave_user_api`: [http://localhost:8081](http://localhost:8081)
   - `talkwave_user_ui`: [http://localhost:3000](http://localhost:3000)

### Notes

- The `database` service uses a volume `db_data` to persist data.
- Ensure the ports `8080`, `8081`, and `3000` are available on your host machine.

By following these instructions, you can successfully set up and run the TalkWave project using Docker.