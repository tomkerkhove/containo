FROM microsoft/aspnetcore:2.0@sha256:5f964756fae50873c496915ad952b0f15df8ef985e4ac031d00b7ac0786162d0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0@sha256:1e4043c2484157e029d6b09a3149ebd01c5122f6b62f0a1b6c6f54b90500ea4b AS build
WORKDIR /src
COPY Containo.sln ./
COPY Containo.Services.Orders.Api/Containo.Services.Orders.Api.csproj Containo.Services.Orders.Api/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/Containo.Services.Orders.Api
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Containo.Services.Orders.Api.dll"]
