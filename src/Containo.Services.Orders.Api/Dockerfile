FROM microsoft/dotnet:2.2.100-sdk AS build
WORKDIR /src
COPY Containo.sln ./
COPY Containo.Services.Orders.Api/Containo.Services.Orders.Api.csproj Containo.Services.Orders.Api
COPY Containo.Services.Orders.Storage/Containo.Services.Orders.Storage.csproj Containo.Services.Orders.Storage/
COPY Containo.Services.Orders.Contracts/Containo.Services.Orders.Contracts.csproj Containo.Services.Orders.Contracts/
COPY Containo.Core.Api/Containo.Core.Api.csproj Containo.Core.Api/
COPY . .
WORKDIR /src/Containo.Services.Orders.Api
RUN dotnet publish -c Release -o /app

FROM microsoft/dotnet:2.2.0-aspnetcore-runtime as runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Containo.Services.Orders.Api.dll"]
