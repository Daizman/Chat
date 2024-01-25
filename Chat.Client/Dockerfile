# Указываем базовый образ
FROM node:14 as build
# Устанавливаем рабочую директорию в контейнере
WORKDIR /app
# Копируем файлы package.json и package-lock.json
COPY chat/package*.json ./
# Устанавливаем зависимости
RUN npm install
# Копируем исходный код приложения
COPY chat/ .
# Собираем приложение
RUN npm run build

FROM node:14 as dev
WORKDIR /app
COPY chat/package*.json ./
RUN npm install
COPY chat/ .
EXPOSE 3000
CMD ["npm", "start"]

FROM nginx:alpine as finall
# Копируем собранные файлы из предыдущего шага
COPY --from=build /app/build /usr/share/nginx/html
# Открываем 80 порт для доступа к приложению
EXPOSE 80
# Запускаем Nginx
CMD ["nginx", "-g", "daemon off;"]
