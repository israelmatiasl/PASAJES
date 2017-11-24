FROM microsoft/aspnetcore:1.1

# Set the Working Directory
WORKDIR /app

# Configure the listening port
ARG APP_PORT=5000
ENV ASPNETCORE_URLS http://*:$APP_PORT
EXPOSE $APP_PORT

# Copy the app
COPY . /app

# Start the app
ENTRYPOINT RUTAS.REST.PASAJE.dll