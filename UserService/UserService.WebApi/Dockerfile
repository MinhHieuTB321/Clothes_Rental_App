#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UserService.Domain/UserService.Domain.csproj", "UserService.Domain/"]
COPY ["UserService.Application/UserService.Application.csproj", "UserService.Application/"]
COPY ["UserService.Infrastructures/UserService.Infrastructures.csproj", "UserService.Infrastructures/"]
COPY ["UserService.WebApi/UserService.WebApi.csproj", "UserService.WebApi/"]

RUN dotnet restore "UserService.WebApi/UserService.WebApi.csproj"
COPY . .
WORKDIR "/src/UserService.WebApi"
RUN dotnet build "UserService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserService.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.WebApi.dll"]