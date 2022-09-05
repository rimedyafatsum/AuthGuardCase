#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Employee/AuthGuardCase.Services.Employee/AuthGuardCase.Services.Employee.csproj", "Services/Employee/AuthGuardCase.Services.Employee/"]
COPY ["Shared/AuthGuardCase.Shared/AuthGuardCase.Shared.csproj", "Shared/AuthGuardCase.Shared/"]
RUN dotnet restore "Services/Employee/AuthGuardCase.Services.Employee/AuthGuardCase.Services.Employee.csproj"
COPY . .
WORKDIR "/src/Services/Employee/AuthGuardCase.Services.Employee"
RUN dotnet build "AuthGuardCase.Services.Employee.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthGuardCase.Services.Employee.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthGuardCase.Services.Employee.dll"]