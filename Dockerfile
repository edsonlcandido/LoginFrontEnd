# Stage 1: Build the Blazor WebAssembly application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder
WORKDIR /src
COPY . .
RUN dotnet restore "./LoginFrontEnd.csproj" # Substitua YourProjectName.csproj
RUN dotnet publish "./LoginFrontEnd.csproj" -o /app/publish --no-restore

# Stage 2: Serve the application using Nginx
FROM nginx:stable-alpine
COPY --from=builder /app/publish/wwwroot /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf # Use sua configura��o personalizada, se necess�rio
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]