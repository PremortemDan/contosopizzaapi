FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything and restore/publish
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish -f net8.0

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

EXPOSE 80

# Use the PORT env variable provided by Render (or default to 80).
# The shell form allows expanding ${PORT} at container start time.
ENTRYPOINT ["bash","-lc","ASPNETCORE_URLS=http://*:${PORT:-80} dotnet Contoso.dll"]
