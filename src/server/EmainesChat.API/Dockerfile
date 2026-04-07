# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EmainesChat.API/EmainesChat.API.csproj",                        "EmainesChat.API/"]
COPY ["EmainesChat.Application/EmainesChat.Application.csproj",        "EmainesChat.Application/"]
COPY ["EmainesChat.Domain/EmainesChat.Domain.csproj",                  "EmainesChat.Domain/"]
COPY ["EmainesChat.Infrastructure/EmainesChat.Infrastructure.csproj",  "EmainesChat.Infrastructure/"]
RUN dotnet restore "EmainesChat.API/EmainesChat.API.csproj"
COPY . .
RUN dotnet publish "EmainesChat.API/EmainesChat.API.csproj" -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "EmainesChat.API.dll"]
