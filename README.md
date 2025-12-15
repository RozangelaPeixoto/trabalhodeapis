# 🏢 Trabalho de APIs - Empresas e Funcionários

Um projeto de **microserviços em ASP.NET Core** que oferece APIs RESTful para gerenciamento de empresas e funcionários, com banco de dados MySQL containerizado.

---

## 📋 Sobre o Projeto

Este projeto implementa dois serviços independentes:

- **Empresas.Api**: API para gerenciar dados de empresas
- **Funcionarios.Api**: API para gerenciar dados de funcionários

Ambos os serviços utilizam:
- ✅ ASP.NET Core 7+
- ✅ Entity Framework Core
- ✅ MySQL 8.0 (via Docker)
- ✅ Swagger/OpenAPI para documentação
- ✅ Tratamento global de exceções

---

## 🏗️ Arquitetura

```
TrabalhodeAPIs/
├── Empresas.Api/          # API de Empresas
│   ├── Controllers/       # Controladores HTTP
│   ├── Models/           # Entidades de domínio
│   ├── DTOs/             # Data Transfer Objects
│   ├── Data/             # DbContext e configurações
│   └── Migrations/       # Migrações do EF Core
├── Funcionarios.Api/      # API de Funcionários
│   ├── Controllers/      # Controladores HTTP
│   ├── Models/          # Entidades de domínio
│   ├── DTOs/            # Data Transfer Objects
│   ├── Data/            # DbContext e configurações
│   └── Migrations/      # Migrações do EF Core
├── docker-compose.yml    # Configuração do MySQL
└── .env                  # Variáveis de ambiente
```

---

## 🚀 Quickstart

### Pré-requisitos

- [.NET 7.0+](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- Git

### Instalação e Execução

#### 1. Clone o repositório
```bash
git clone <seu-repositorio>
cd TrabalhodeAPIs
```

#### 2. Configure as variáveis de ambiente
Crie um arquivo `.env` na raiz do projeto:
```env
DB_HOST=localhost
DB_PORT=3306
DB_USER=user_api
DB_PASSWORD=sua_senha_segura
DB_ROOT_PASSWORD=root_senha_segura
DB_DATA_PATH=./Data
```

#### 3. Inicie o MySQL com Docker
```bash
docker-compose up -d
```

#### 4. Restaure as dependências e execute as migrações
```bash
# Para Empresas.Api
cd Empresas.Api
dotnet restore
dotnet ef database update
dotnet run

# Em outro terminal, para Funcionarios.Api
cd Funcionarios.Api
dotnet restore
dotnet ef database update
dotnet run
```

---

## 📡 Endpoints

### Empresas.Api (por padrão: `http://localhost:5000`)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/api/empresas` | Lista todas as empresas |
| `GET` | `/api/empresas/{id}` | Obtém uma empresa por ID |
| `POST` | `/api/empresas` | Cria uma nova empresa |
| `PUT` | `/api/empresas/{id}` | Atualiza uma empresa |
| `DELETE` | `/api/empresas/{id}` | Deleta uma empresa |

**Exemplo de criação de empresa:**
```json
POST /api/empresas
{
  "nome": "Tech Solutions Ltda",
  "endereco": "Rua das Flores, 123",
  "telefone": "11 3456-7890"
}
```

### Funcionarios.Api (por padrão: `http://localhost:5001`)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/api/funcionarios` | Lista todos os funcionários |
| `GET` | `/api/funcionarios/{id}` | Obtém um funcionário por ID |
| `POST` | `/api/funcionarios` | Cria um novo funcionário |
| `PUT` | `/api/funcionarios/{id}` | Atualiza um funcionário |
| `DELETE` | `/api/funcionarios/{id}` | Deleta um funcionário |

**Exemplo de criação de funcionário:**
```json
POST /api/funcionarios
{
  "nome": "João Silva",
  "cargo": "Desenvolvedor Sênior",
  "salario": 8500.00
}
```

---

## 📚 Documentação Swagger

Cada API possui documentação interativa via Swagger:

- **Empresas.Api**: `http://localhost:5000/swagger`
- **Funcionarios.Api**: `http://localhost:5001/swagger`

---

## 📦 Modelos de Dados

### Empresa
```csharp
public class Empresa
{
    public int Id { get; set; }
    public required string Nome { get; set; }              
    public required string Endereco { get; set; }
    public required string Telefone { get; set; }
}
```

### Funcionário
```csharp
public class Funcionario
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Cargo { get; set; }
    public decimal Salario { get; set; }
}
```

---

## 🛠️ Tecnologias

- **Runtime**: .NET 7.0+
- **Web Framework**: ASP.NET Core
- **ORM**: Entity Framework Core
- **Banco de Dados**: MySQL 8.0
- **Containerização**: Docker
- **Documentação API**: Swagger/OpenAPI
- **Validation**: Data Annotations

---

## 🔧 Desenvolvimento

### Criar uma migração
```bash
cd Empresas.Api
dotnet ef migrations add NomeDaMigracao
dotnet ef database update
```

### Listar migrações pendentes
```bash
dotnet ef migrations list
```

### Remover última migração
```bash
dotnet ef migrations remove
```

---

## 📝 Tratamento de Erros

A API fornece respostas padronizadas para erros:

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Nome": ["O nome é obrigatório."]
  }
}
```

### Códigos de Status HTTP

| Código | Significado |
|--------|------------|
| `200` | OK - Requisição bem-sucedida |
| `201` | Created - Recurso criado |
| `400` | Bad Request - Erro na validação |
| `404` | Not Found - Recurso não encontrado |
| `500` | Internal Server Error - Erro do servidor |
| `503` | Service Unavailable - Banco de dados indisponível |

---

## 🧪 Testes

Para testar as APIs, você pode usar:

- **Postman**: Importe os arquivos `.http` presentes em cada projeto
- **Swagger UI**: Acesse os endpoints `/swagger`

---

## 🐳 Docker

### Iniciar o MySQL
```bash
docker-compose up -d
```

### Parar o MySQL
```bash
docker-compose down
```

### Ver logs do MySQL
```bash
docker-compose logs -f mysql
```

### Limpar volumes (⚠️ apaga dados)
```bash
docker-compose down -v
```
