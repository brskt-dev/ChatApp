# Use a imagem oficial do .NET 9 SDK para buildar
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copia o arquivo csproj e restaura as dependências
COPY *.csproj .
RUN dotnet restore

# Copia os demais arquivos e faz o build
COPY . .
RUN dotnet publish -c Release -o out

# Segunda etapa: usa uma imagem menor para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expor a porta 80 (ou a porta que sua API utiliza)
EXPOSE 8080

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "chat-api.dll"]

