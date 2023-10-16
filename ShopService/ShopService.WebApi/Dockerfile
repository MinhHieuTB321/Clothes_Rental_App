#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ShopService.WebApi/ShopService.WebApi.csproj", "ShopService.WebApi/"]
COPY ["ShopService.Infrastructures/ShopService.Infrastructures.csproj", "ShopService.Infrastructures/"]
COPY ["ShopService.Application/ShopService.Application.csproj", "ShopService.Application/"]
COPY ["ShopService.Domain/ShopService.Domain.csproj", "ShopService.Domain/"]
RUN dotnet restore "ShopService.WebApi/ShopService.WebApi.csproj"
COPY . .
WORKDIR "/src/ShopService.WebApi"
RUN dotnet build "ShopService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopService.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopService.WebApi.dll"]