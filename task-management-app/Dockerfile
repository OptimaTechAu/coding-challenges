FROM node:18.12.1-alpine

WORKDIR /app

# Install Angular CLI
RUN npm install -g @angular/cli@15.2.11

# Copy package files and install dependencies
COPY package*.json ./
RUN npm install

# Copy the entire application source code
COPY . .

# Expose port
EXPOSE 4200

# Start development server
CMD ["npm", "start", "--", "--host", "0.0.0.0"] 