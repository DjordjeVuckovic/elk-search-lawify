ARG BUILD_MODE=container

ARG VITE_SEARCH_BASE_URL
ARG VITE_CONTENT_BASE_URL
ARG VITE_GOOGLE_MAP_KEY

FROM node:18 AS build
WORKDIR /app

ENV VITE_CONTENT_BASE_URL=$VITE_CONTENT_BASE_URL
ENV VITE_CONTENT_BASE_URL=$VITE_CONTENT_BASE_URL
ENV VITE_GOOGLE_MAP_KEY=$VITE_GOOGLE_MAP_KEY

COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build --mode=$BUILD_MODE

FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
COPY ./nginx.conf /etc/nginx/conf.d/default.conf