FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["AgendaApi.csproj", "AgendaApi/"]
RUN dotnet restore "AgendaApi.csproj"

COPY . .

RUN dotnet publish "AgendaApi.csproj" -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
ENTRYPOINT ["dotnet", "AgendaApi.dll"]
