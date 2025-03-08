# Agenda API

Esta é uma API desenvolvida com .NET 9 para gerenciar contatos em uma agenda. A aplicação permite realizar operações de CRUD (Criar, Ler, Atualizar, Deletar) para adicionar, visualizar, editar e remover contatos. Ela utiliza o Entity Framework Core para acesso a dados e o RabbitMQ para mensageria.

## Funcionalidades

- **CRUD de Contatos**: Criação, leitura, atualização e exclusão de contatos.
- **Validações**: Validação de dados de entrada com FluentValidation.
- **Mensageria**: Envia mensagens via RabbitMQ quando um novo contato é criado.
- **Swagger**: Documentação da API com Swagger.
- **Autenticação**: Segurança implementada com JWT Bearer tokens (opcional).

## Tecnologias

- **Backend**: .NET 9
- **Banco de Dados**: SQL Server (pode ser configurado para In-Memory para testes).
- **Mensageria**: RabbitMQ
- **Validações**: FluentValidation
- **Mapeamento de objetos**: AutoMapper
- **Autenticação**: JWT Bearer
- **Testes**: xUnit, Moq
- **Documentação**: Swagger

## Requisitos

- **.NET 9**
- **RabbitMQ** (se for utilizar mensageria)
- **SQL Server** ou **In-Memory Database** (para testes)
- **Swagger UI** (opcional para visualização da API)

## Como Rodar o Projeto

### Pré-requisito

- **Ter o .NET SDK configurado**
- **Ter algum serviço docker instalado e rodando**

### Passo 1: Rodar o docker compose

```
docker compose up --build
```

### Passo 2: Rodar a migration

```
dotnet ef database update
```

Com isso a aplicação, o banco de dados e o RabbitMQ já serão iniciados em containers locais.
A aplicação roda em: http://localhost:5240/

## Testes

O projeto inclui testes unitários para a camada de serviço e controlador.

Basta executar o seguinte comando:

```
dotnet test
```
