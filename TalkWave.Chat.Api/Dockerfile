# syntax=docker/dockerfile:1

# Define the .NET version to use
ARG DOTNET_VERSION=7.0

# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS builder

# Set the working directory
WORKDIR /source

# Copy the project file and restore dependencies
COPY TalkWave.Chat.Api/TalkWave.Chat.Api.csproj ./
RUN --mount=type=cache,target=/root/.nuget/packages \
    --mount=type=cache,target=/root/.cache/msbuild \
    dotnet restore

# Copy the entire source code
COPY TalkWave.Chat.Api/ ./

# Build the application
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime

# Set the working directory
WORKDIR /app

# Copy the published application from the builder stage
COPY --from=builder /app/publish .

# Expose the port the application runs on
EXPOSE 80

# Define the entry point for the application
ENTRYPOINT ["dotnet", "TalkWave.Chat.Api.dll"]