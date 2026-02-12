# BookStore API

API desenvolvida em .NET 9.

## Como rodar o projeto

### 1- Subir o banco
Na pasta BookStoreSoftDesign rodar: 
docker compose up -d

### 2- Rodar a API
dotnet run --project BookStore.API
Estará disponivel em: http://localhost:5000/swagger

No Swagger estão disponíveis todos os endpoints.

Observação: No campo coverImage, envie uma string vazia nos endpoints, pois não foi configurado o acesso à AWS.
A estrutura para integração com o S3 já está implementada no código — basta ajustar as configurações no appsettings.
