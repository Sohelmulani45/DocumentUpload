FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["DocumentUploadApp/DocumentUploadMVC.csproj", "DocumentUploadApp.MVC/"]

RUN dotnet restore "DocumentUploadApp.MVC/DocumentUploadApp.MVC.csproj"

COPY . .

WORKDIR /src/DocumentUploadApp.MVC

RUN dotnet publish "DocumentUploadApp.MVC.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE=false
ENV DOTNET_USE_POLLING_FILE_WATCHER=1

EXPOSE 8080

ENTRYPOINT ["dotnet", "DocumentUploadMVC.dll"]
