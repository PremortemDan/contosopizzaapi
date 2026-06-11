FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything and restore/publish
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish -f net8.0

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

ENV ASPNETCORE_URLS="http://+:80"
EXPOSE 80

# Replace "Contoso.dll" with your project's DLL name if different
ENTRYPOINT ["dotnet", "Contoso.dll"]
