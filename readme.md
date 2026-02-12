# BookStore API

API desenvolvida em .NET 9.

## Como rodar o projeto

### 1- Subir o banco
Na pasta BookStoreSoftDesign rodar: 
docker compose up -d

### 2- Rodar a API
dotnet run --project BookStore.API
Estará disponivel em: http://localhost:5000/swagger

No Swagger está disponivel todos os endpoints
Nota: No coverImage mandar como uma string vazia nos endpoints pois não criei um acesso na AWS, mas ele esta com toda a estrutura pronta no codigo, só precisaria mudar no appsettings
