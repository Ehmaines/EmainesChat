# EmainesChat

Chat em tempo real com API em .NET, SignalR, Entity Framework, SQL Server e frontend em Angular.

> Em construção.

## Pré-requisitos

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Node.js 18+](https://nodejs.org/)
- [Docker](https://www.docker.com/) (para o SQL Server)

## Configuração inicial

### 1. Suba o banco de dados

```bash
docker compose up -d
```

Isso inicia o SQL Server 2019 em `localhost:1433` com as credenciais padrão.

### 2. Configure os segredos da API

Este projeto usa [.NET User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) para manter credenciais fora do repositório. Você precisa configurá-los antes de rodar a API pela primeira vez.

```bash
cd src/server/EmainesChat.API/EmainesChat.API

dotnet user-secrets set "Jwt:Secret" "<uma-chave-secreta-longa-e-aleatoria>"
dotnet user-secrets set "ConnectionStrings:Default" "Server=localhost,1433;Initial Catalog=EmainesChat;User Id=sa;Password=P@ssw0rd;Encrypt=false;TrustServerCertificate=true;"
```

> O valor de `Jwt:Secret` pode ser qualquer string longa e aleatória — ela é usada para assinar os tokens JWT. A connection string acima corresponde ao container Docker configurado no `docker-compose.yml`.

Os segredos ficam salvos localmente em `%APPDATA%\Microsoft\UserSecrets\` (Windows) ou `~/.microsoft/usersecrets/` (Linux/macOS) e nunca são commitados no repositório.

### 3. Execute as migrations

```bash
cd src/server/EmainesChat.API/EmainesChat.Infrastructure

dotnet ef database update --startup-project ../EmainesChat.API
```

### 4. Suba a API

A API não é iniciada automaticamente. Você pode rodá-la de duas formas:

**Pelo Visual Studio** (recomendado — permite debugar):
Abra `src/server/EmainesChat.API/EmainesChat.API.sln` e pressione F5.

**Pela CLI:**
```bash
cd src/server/EmainesChat.API
dotnet run --project EmainesChat.API --configuration Debug
```

A API ficará disponível em `https://localhost:7080`. O Swagger abre automaticamente em ambiente Development.

### 5. Suba o banco de dados e o frontend

```bash
cd src/client/EmainesChat.App

npm install
npm run dev
```

`npm run dev` sobe o SQL Server via Docker e inicia o frontend em `http://localhost:4200`.
