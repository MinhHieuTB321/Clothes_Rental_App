#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["OrderService.WebApi/OrderService.WebApi.csproj", "OrderService.WebApi/"]
COPY ["OrderService.Infrastructures/OrderService.Infrastructures.csproj", "OrderService.Infrastructures/"]
COPY ["OrderService.Application/OrderService.Application.csproj", "OrderService.Application/"]
COPY ["OrderService.Domain/OrderService.Domain.csproj", "OrderService.Domain/"]
RUN dotnet restore "OrderService.WebApi/OrderService.WebApi.csproj"
COPY . .
WORKDIR "/src/OrderService.WebApi"
RUN dotnet build "OrderService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderService.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderService.WebApi.dll"]