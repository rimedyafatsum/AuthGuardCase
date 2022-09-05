#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Clients/AuthGuardCase.Clients.Client_3/AuthGuardCase.Clients.Client_3.csproj", "Clients/AuthGuardCase.Clients.Client_3/"]
RUN dotnet restore "Clients/AuthGuardCase.Clients.Client_3/AuthGuardCase.Clients.Client_3.csproj"
COPY . .
WORKDIR "/src/Clients/AuthGuardCase.Clients.Client_3"
RUN dotnet build "AuthGuardCase.Clients.Client_3.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthGuardCase.Clients.Client_3.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthGuardCase.Clients.Client_3.dll"]