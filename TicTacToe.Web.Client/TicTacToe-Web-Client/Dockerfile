### STAGE 1:BUILD SOLUTION ###

FROM node:16.10-alpine AS builder

COPY package.json package-lock.json ./

RUN npm install && mkdir /front-ui && mv ./node_modules ./front-ui

WORKDIR /front-ui

RUN npm cache clean --force

COPY . .

RUN npm run ng build -- --deploy-url=/tictactoe-web-client/ --prod

### STAGE 2:RUN HTTP SERVER ###

FROM nginx:alpine AS web-server

COPY ./nginx_config/nginx.conf /etc/nginx/nginx.conf

## Remove default nginx index page
RUN rm -rf /usr/share/nginx/html/*

COPY --from=builder /front-ui/dist /usr/share/nginx/html

EXPOSE 4200 80

ENTRYPOINT ["nginx", "-g", "daemon off;"]