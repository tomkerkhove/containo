FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
RUN ls -R
COPY Containo.sln ./
COPY Containo.Services.Orders.Api/Containo.Services.Orders.Api.csproj Containo.Services.Orders.Api/
COPY Containo.Services.Orders.Storage/Containo.Services.Orders.Storage.csproj Containo.Services.Orders.Storage/
COPY Containo.Services.Orders.Contracts/Containo.Services.Orders.Contracts.csproj Containo.Services.Orders.Contracts/
COPY Containo.Core.Api/Containo.Core.Api.csproj Containo.Core.Api/
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
