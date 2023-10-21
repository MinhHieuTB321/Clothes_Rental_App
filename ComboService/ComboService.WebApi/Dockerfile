#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ComboService.WebApi/ComboService.WebApi.csproj", "ComboService.WebApi/"]
COPY ["ComboService.Application/ComboService.Application.csproj", "ComboService.Application/"]
COPY ["ComboService.Domain/ComboService.Domain.csproj", "ComboService.Domain/"]
COPY ["ComboService.Infrastructures/ComboService.Infrastructures.csproj", "ComboService.Infrastructures/"]
RUN dotnet restore "ComboService.WebApi/ComboService.WebApi.csproj"
COPY . .
WORKDIR "/src/ComboService.WebApi"
RUN dotnet build "ComboService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ComboService.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ComboService.WebApi.dll"]