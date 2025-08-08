# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TicTacToe.BlazorUI.csproj", "./"]
RUN dotnet restore "TicTacToe.BlazorUI.csproj"
COPY . .
RUN dotnet publish "TicTacToe.BlazorUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Этап раздачи статичных файлов (nginx)
FROM nginx:alpine AS runtime
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]


