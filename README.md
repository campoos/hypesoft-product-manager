# 🧠 Hypesoft Product Manager

Sistema fullstack de gerenciamento de produtos com controle de estoque em tempo real, desenvolvido com arquitetura em camadas e containerização via Docker.

Projeto focado em boas práticas de desenvolvimento, organização de código e experiência do usuário.

## 🚀 Tecnologias

- Frontend: React + Vite
- Backend: .NET (C#)
- Banco de dados: MongoDB
- Autenticação: Keycloak (OpenID Connect)
- Containerização: Docker + Docker Compose

## 📦 Pré-requisitos

Antes de começar, você precisa ter instalado:

- Docker
- Docker Compose
- Node.js (caso queira rodar o frontend fora do container)

## ⚙️ Instalação e execução

### 1. Clone o repositório

```bash
git clone <URL_DO_REPOSITORIO>
cd hypesoft-product-manager
```

### 2. Instale as dependências do frontend

```bash
cd frontend
npm install
cd ..
```

### 3. Suba os containers com Docker
```bash
docker-compose up --build
```

Esse comando irá subir automaticamente:

- API
- Banco de dados (MongoDB)
- Keycloak (autenticação)
- Frontend

## 🌐 Acessos

Após subir o projeto, acesse:

- Frontend: http://localhost:3000
- API (Swagger): http://localhost:5000/swagger
- Mongo Express: http://localhost:8081
- Keycloak: http://localhost:8080

## 🔐 Autenticação

O sistema utiliza o Keycloak para autenticação e autorização via OpenID Connect.

### Credenciais padrão (ambiente local):

- Usuário: admin
- Senha: admin

O realm e o client são configurados automaticamente via import no container.

## 📁 Estrutura do projeto
```bash
HYPERSOFT-PRODUCT-MANAGER/
├── backend/
│   └── src/
│       ├── Hypesoft.API/
│       ├── Hypesoft.Application/
│       ├── Hypesoft.Domain/
│       └── Hypesoft.Infrastructure/
│
├── frontend/
│   └── src/
│       ├── assets/
│       ├── components/
│       ├── pages/
│       ├── routes/
│       ├── services/
│       ├── hooks/
│       └── utils/
│
├── keycloak/
│   └── realm-export.json
│
├── docs/
│   └── images/
│
├── docker-compose.yml
└── README.md
```

## 🧠 Arquitetura

O backend foi estruturado seguindo princípios de Clean Architecture:

- **API**: Camada de entrada (controllers e endpoints)
- **Application**: Regras de negócio e casos de uso
- **Domain**: Entidades e contratos
- **Infrastructure**: Acesso a dados e integrações externas

O frontend segue uma arquitetura baseada em:

- Componentização
- Separação por responsabilidade
- Consumo de API via services
- Gerenciamento de estado com hooks

## ⚙️ Decisões técnicas

- **MongDB** pela flexibilidade de schema e facilidade de evolução

- **Docker** para padronização de ambiente e facilidade de execução

- **Keycloak** para autenticação robusta e escalável

- **Clean Architecture** para organização e manutenção do backend

- **Vite** pela velocidade no desenvolvimento frontend

## 🔌 API

Principais funcionalidades da API:

- CRUD completo de produtos
- Controle de estoque
- Atualização de quantidade em tempo real
- Endpoint de dashboard com métricas de estoque

### Endpoints:

- GET /produtos → Lista todos os produtos
- POST /produtos → Cria um novo produto
- PUT /produtos/{id} → Atualiza um produto
- DELETE /produtos/{id} → Remove um produto
- GET /dashboard → Retorna métricas do estoque

## 🧪 Testes

A validação da aplicação foi feita através de:

- Testes manuais via Postman para validação dos endpoints
- Testes de aceitação com usuários (feedback real de usabilidade)
- Validação de fluxos principais (CRUD e controle de estoque)

Obs: Testes automatizados podem ser adicionados como evolução futura.

A aplicação pode ser testada via Swagger:
http://localhost:5000/swagger

## 📸 Interface

![Dashboard](./docs/images/dashboard-preview.png)
![Produtos](./docs/images/produtos-preview.png)
![Criação de produto](./docs/images/modal-criacao-preview.png)