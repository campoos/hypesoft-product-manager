# imagem base
FROM node:20

# pasta de trabalho
WORKDIR /app

# copia arquivos
COPY package*.json ./

# instala dependências
RUN npm install

# copia resto
COPY . .

# expõe porta
EXPOSE 5173

# roda o projeto
CMD ["npm", "run", "dev"]