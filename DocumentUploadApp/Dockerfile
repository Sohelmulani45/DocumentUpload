FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["DocumentUploadApp/DocumentUploadApp.csproj", "DocumentUploadApp/"]

RUN dotnet restore "DocumentUploadApp/DocumentUploadApp.csproj"

COPY . .

WORKDIR /src/DocumentUploadApp

RUN dotnet publish "DocumentUploadApp.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet","DocumentUploadApp.dll"]
