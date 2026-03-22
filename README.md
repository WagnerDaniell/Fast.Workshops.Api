# Fast.Workshops.Api

API REST para rastreamento de participação em workshops trimestrais da FAST Soluções.

---

## Tecnologias

- .NET 8
- MySQL 8.0
- Entity Framework Core + Pomelo
- JWT Bearer Authentication
- FluentValidation
- Swagger / OpenAPI
- Docker + Docker Compose

---

## Pré-requisitos

- [Docker](https://www.docker.com/) instalado
- [Docker Compose](https://docs.docker.com/compose/) instalado

---

## Como rodar o projeto

### 1. Clone o repositório

```bash
git clone https://github.com/WagnerDaniell/Fast.Workshops.Api.git
cd Fast.Workshops.Api
```

### 2. Configure as variáveis de ambiente

Crie um arquivo `.env` na raiz do projeto com o seguinte conteúdo:

# Exemplo que pode ser usado:
```env
ASPNETCORE_ENVIRONMENT=Development

DB_HOST=db
DB_PORT=3306
MYSQL_DATABASE=fast_workshops

#Conectando com o root só pq está em um ambiente dev locals
MYSQL_USER=root
MYSQL_PASSWORD=root
MYSQL_ROOT_PASSWORD=root

JWT__SECRETKEY=8f3c2b7a9e1d4c6f8a2b5d7e9c1f3a6b8d2e4f7a9c5b1d3e6f8a2c4b7d9e1f6a
```

> **Atenção:** a chave JWT deve ter no mínimo 32 caracteres.

### 3. Suba os containers

```bash
docker compose up --build
```

Isso irá subir:
- **API** em `http://localhost:5000`
- **MySQL** na porta `3306`
- **Adminer** (interface visual do banco) em `http://localhost:8080`

> As migrations são aplicadas automaticamente ao subir a aplicação em ambiente Development.

### 4. Acesse a documentação

```
http://localhost:5000/swagger ou https://localhost:5001/swagger
```

---

## Autenticação

A API utiliza **JWT Bearer Token**. Para acessar os endpoints protegidos:

1. Crie uma conta em `POST /api/v1/auth/register`
2. Faça login em `POST /api/v1/auth/login`
3. Copie o `accessToken` retornado
4. No Swagger, clique em **Authorize** e cole o token no formato:
```
Bearer {seu_token}
```

---

## Endpoints

### Auth — `/api/v1/auth`

> Endpoints públicos, não requerem autenticação.

| Método | Rota | Descrição |
|--------|------|-----------|
| POST | `/api/v1/auth/register` | Cria uma nova conta de administrador |
| POST | `/api/v1/auth/login` | Realiza login e retorna o token JWT |

#### POST `/api/v1/auth/register`

**Request:**
```json
{
  "name": "Wagner",
  "email": "wagner@fast.com",
  "password": "senha123"
}
```

**Response `201`:**
```json
{
  "userName": "Wagner",
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

---

#### POST `/api/v1/auth/login`

**Request:**
```json
{
  "email": "wagner@fast.com",
  "password": "senha123"
}
```

**Response `200`:**
```json
{
  "userName": "Wagner",
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

---

### Colaboradores — `/api/v1/colaboradores`

> Todos os endpoints requerem autenticação.

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/v1/colaboradores` | Lista todos os colaboradores |
| GET | `/api/v1/colaboradores/{id}` | Busca um colaborador pelo ID |
| POST | `/api/v1/colaboradores` | Cria um novo colaborador |
| PUT | `/api/v1/colaboradores/{id}` | Atualiza um colaborador |
| DELETE | `/api/v1/colaboradores/{id}` | Remove um colaborador |

#### POST `/api/v1/colaboradores`

**Request:**
```json
{
  "name": "João Silva"
}
```

**Response `201`:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "João Silva"
}
```

---

#### PUT `/api/v1/colaboradores/{id}`

**Request:**
```json
{
  "name": "João Silva Atualizado"
}
```

**Response `200`:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "João Silva Atualizado"
}
```

---

### Workshops — `/api/v1/workshops`

> Todos os endpoints requerem autenticação.

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/v1/workshops` | Lista todos os workshops |
| GET | `/api/v1/workshops/{id}` | Busca um workshop com lista de colaboradores presentes |
| POST | `/api/v1/workshops` | Cria um novo workshop |
| PUT | `/api/v1/workshops/{id}` | Atualiza um workshop |
| DELETE | `/api/v1/workshops/{id}` | Remove um workshop |
| POST | `/api/v1/workshops/{id}/colaboradores/{colaboradorId}` | Registra presença de colaborador no workshop |
| DELETE | `/api/v1/workshops/{id}/colaboradores/{colaboradorId}` | Remove presença de colaborador do workshop |

#### POST `/api/v1/workshops`

**Request:**
```json
{
  "name": "Workshop de Clean Code",
  "date": "2026-04-10T16:00:00",
  "description": "Boas práticas de desenvolvimento de software."
}
```

**Response `201`:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Workshop de Clean Code",
  "date": "2026-04-10T16:00:00",
  "description": "Boas práticas de desenvolvimento de software.",
  "createdAt": "2026-03-21T14:00:00"
}
```

> **Regra de negócio:** não é possível criar dois workshops na mesma data e horário.

---

#### GET `/api/v1/workshops/{id}`

**Response `200`:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Workshop de Clean Code",
  "date": "2026-04-10T16:00:00",
  "description": "Boas práticas de desenvolvimento de software.",
  "createdAt": "2026-03-21T14:00:00",
  "colaboradores": [
    {
      "id": "1fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "João Silva"
    },
    {
      "id": "2fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "Maria Santos"
    }
  ]
}
```

---

#### POST `/api/v1/workshops/{id}/colaboradores/{colaboradorId}`

Registra a presença de um colaborador em um workshop (ata de presença).

**Response `204 No Content`**

> **Regras de negócio:**
> - O workshop deve existir
> - O colaborador deve existir
> - O colaborador não pode ser registrado duas vezes no mesmo workshop

---

#### DELETE `/api/v1/workshops/{id}/colaboradores/{colaboradorId}`

Remove a presença de um colaborador de um workshop.

**Response `204 No Content`**

---

## Códigos de resposta

| Código | Descrição |
|--------|-----------|
| 200 | Sucesso |
| 201 | Criado com sucesso |
| 204 | Removido com sucesso |
| 400 | Dados inválidos |
| 401 | Não autenticado |
| 404 | Recurso não encontrado |
| 409 | Conflito (recurso já existe) |
| 500 | Erro interno do servidor |

---

## Adminer — Interface visual do banco

Acesse `http://localhost:8080` com as seguintes credenciais:

| Campo | Valor |
|-------|-------|
| Sistema | MySQL |
| Servidor | db |
| Usuário | root |
| Senha | root |
| Base de dados | fast_workshops |

---

## Estrutura do projeto

```
Fast.Workshops.Api/
├── Fast.Workshops.Api/           # Controllers, Middlewares, Program.cs
├── Fast.Workshops.Application/   # UseCases, DTOs, Validators, Services
├── Fast.Workshops.Domain/        # Entidades, Interfaces, Exceptions
└── Fast.Workshops.Infrastructure/ # Context, Repositories, Migrations
```
