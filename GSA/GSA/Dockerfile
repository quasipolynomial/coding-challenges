FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 8000

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY GSA/GSA.csproj GSA/
RUN dotnet restore GSA/GSA.csproj
COPY . .
WORKDIR /src/GSA
RUN dotnet build GSA.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish GSA.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GSA.dll"]
