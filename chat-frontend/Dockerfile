# Usa a imagem oficial do Node.js para buildar o frontend
FROM node:20 AS build
WORKDIR /app

# Copia os arquivos do projeto e instala as dependências
COPY package.json package-lock.json ./
RUN npm install

# Copia o restante dos arquivos e faz o build
COPY . .
RUN npm run build --configuration=production

# Segunda etapa: usa Nginx para servir os arquivos Angular
FROM nginx:latest
WORKDIR /usr/share/nginx/html

# Copia corretamente a pasta `browser/` dentro da build do Angular
COPY --from=build /app/dist/chat-frontend/browser .

# Remove a configuração padrão do Nginx e adiciona um customizado
RUN rm /etc/nginx/conf.d/default.conf
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
