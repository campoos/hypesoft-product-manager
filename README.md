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

### 🔧 Acesso ao Keycloak (admin do sistema de autenticação)

- Usuário: admin
- Senha: admin

> Usado para acessar o painel administrativo do Keycloak e gerenciar usuários, roles e clients.

---

### 👥 Acesso à aplicação

#### 👑 Administrador
- Usuário: admin
- Senha: admin123

#### 👤 Usuário comum
- Usuário: user
- Senha: user123

> ⚠️ Esses usuários são criados automaticamente via import do realm no container do Keycloak.

---

- Admin: acesso completo ao sistema (CRUD e dashboard)  
- User: acesso restrito (visualização)

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
- Atualização em tempo real
- Dashboard com métricas

### Endpoints:

- GET /produtos
- POST /produtos
- PUT /produtos/{id}
- DELETE /produtos/{id}

---

- GET /categorias
- POST /categorias
- PUT /categorias/{id}
- DELETE /categorias/{id}

---

- GET /dashboard

## 🧪 Testes

A validação da aplicação foi feita através de:

- Testes manuais via Postman
- Testes de fluxo (CRUD + estoque)
- Testes de usabilidade

> 🔧 Testes automatizados podem ser adicionados como melhoria futura.

## Troubleshooting

### 🔸 Keycloak não carregou o realm

- Verifique se você está no realm correto (não no master)
- Reinicie os containers:

```bash
docker-compose down -v
docker-compose up --build
```

### 🔸 Erros no frontend (imports)

```bash
cd frontend
npm install
```

### 🔸 Porta em uso

Caso alguma porta esteja ocupada, altere no *docker-compose.yml.*

## 📸 Interface

![Dashboard](./docs/images/dashboard-preview.png)
![Produtos](./docs/images/produtos-preview.png)
![Criação de produto](./docs/images/modal-criacao-preview.png)

## 📈 Melhorias futuras

- Testes automatizados (unitários e integração)
- Sistema de permissões por roles
- Paginação e filtros avançados
- Deploy em cloud (Azure / AWS)
- CI/CD pipeline

## 👨‍💻 Autor

Desenvolvido por João Victor
Projeto criado como desafio técnico com foco em boas práticas e arquitetura escalável.

## 📄 Licença

Este projeto está sob a licença MIT.