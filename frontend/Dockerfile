FROM node:6.14.4

WORKDIR /app

COPY package.json /app
COPY package-lock.json ./

RUN npm install

COPY . ./

CMD ["npm", "start"]
