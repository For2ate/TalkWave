# Define the .NET version to use
ARG DOTNET_VERSION=9.0

# Base image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS base
WORKDIR /app

# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build

# Corrected the spelling of the build configuration argument
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy only the necessary project files to restore dependencies
COPY ./**/*.csproj ./
RUN dotnet restore "./TalkWave.User.Api.csproj"

# Copy the rest of the application files
COPY . .

# Set the working directory to the project folder
WORKDIR /src/TalkWave.User.Api

# Build the application
RUN dotnet build "TalkWave.User.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish "TalkWave.User.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Final stage: Create the runtime image
FROM base AS final
WORKDIR /app

# Set the environment variable for ASP.NET Core
ENV ASPNETCORE_ENVIRONMENT=Development

# Copy the published application from the publish stage
COPY --from=publish /app/publish .

# Expose the necessary ports

EXPOSE 5287
EXPOSE 7163
ENV ASPNETCORE_URLS="http://*:5287;https://*:7163"

# Set the entry point for the application
ENTRYPOINT ["dotnet", "TalkWave.User.Api.dll"]
