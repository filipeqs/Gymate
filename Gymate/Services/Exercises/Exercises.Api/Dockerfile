#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Exercises/Exercises.Api/Exercises.Api.csproj", "Services/Exercises/Exercises.Api/"]
COPY ["Services/Exercises/Exercises.Domain/Exercises.Domain.csproj", "Services/Exercises/Exercises.Domain/"]
COPY ["Services/Exercises/Exercises.Infrastructure/Exercises.Infrastructure.csproj", "Services/Exercises/Exercises.Infrastructure/"]
COPY ["Services/Exercises/Exercises.Core/Exercises.Core.csproj", "Services/Exercises/Exercises.Core/"]
RUN dotnet restore "Services/Exercises/Exercises.Api/Exercises.Api.csproj"
COPY . .
WORKDIR "/src/Services/Exercises/Exercises.Api"
RUN dotnet build "Exercises.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Exercises.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Exercises.Api.dll"]