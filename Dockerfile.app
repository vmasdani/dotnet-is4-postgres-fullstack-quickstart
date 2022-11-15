# Frontend
from node:18.12.1-alpine3.16 AS frontend-env
WORKDIR /App

COPY ./frontend/. ./
RUN corepack enable
RUN --mount=type=cache,target=/root/.yarn YARN_CACHE_FOLDER=/root/.yarn yarn install
RUN --mount=type=cache,target=/root/.yarn YARN_CACHE_FOLDER=/root/.yarn yarn build

# Backend phase 1
FROM mcr.microsoft.com/dotnet/sdk:6.0.403-alpine3.16-amd64 AS build-env

WORKDIR /App

# Copy everything
COPY ./backend/. ./
COPY --from=frontend-env /App/dist wwwroot
# Restore as distinct layers
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages dotnet restore
# Build and publish a release
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0.11-alpine3.16-amd64
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "backend.dll"]
