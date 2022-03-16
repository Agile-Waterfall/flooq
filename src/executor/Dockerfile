FROM node:17-alpine AS builder
WORKDIR /app
COPY package*.json ./

RUN npm install -g typescript
RUN npm ci

COPY . .
RUN npm run build

FROM node:17-alpine AS runner
WORKDIR /app

COPY --from=builder ./app/node_modules ./node_modules
COPY --from=builder ./app/dist ./dist

EXPOSE 3500
CMD [ "node", "./dist/source/index.js" ]