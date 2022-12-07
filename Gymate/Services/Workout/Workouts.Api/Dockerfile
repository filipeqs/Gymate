#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Workout/Workouts.Api/Workouts.Api.csproj", "Services/Workout/Workouts.Api/"]
COPY ["Services/Workout/Workouts.Application/Workouts.Application.csproj", "Services/Workout/Workouts.Application/"]
COPY ["Services/Workout/Workouts.Infrastructure/Workouts.Infrastructure.csproj", "Services/Workout/Workouts.Infrastructure/"]
COPY ["Services/Workout/Workouts.Domain/Workouts.Domain.csproj", "Services/Workout/Workouts.Domain/"]
RUN dotnet restore "Services/Workout/Workouts.Api/Workouts.Api.csproj"
COPY . .
WORKDIR "/src/Services/Workout/Workouts.Api"
RUN dotnet build "Workouts.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Workouts.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workouts.Api.dll"]