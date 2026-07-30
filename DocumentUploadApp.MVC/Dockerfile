# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore (adjust name if different)
COPY ["DocumentUploadApp.MVC/DocumentUploadApp.MVC.csproj", "DocumentUploadApp.MVC/"]
RUN dotnet restore "DocumentUploadApp.MVC/DocumentUploadApp.MVC.csproj"

# Copy everything and publish
COPY . .
WORKDIR /src/DocumentUploadApp.MVC
RUN dotnet publish "DocumentUploadApp.MVC.csproj" -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV DOTNET_RUNNING_IN_CONTAINER=true

# Use PORT provided by Render; fallback to 8080 locally
CMD ["bash", "-lc", "export ASPNETCORE_URLS=http://0.0.0.0:${PORT:-8080} && dotnet DocumentUploadApp.MVC.dll"]
