FROM microsoft/dotnet:2.0-runtime@sha256:8b0ea9c3d2317f57f0272f2c8c5e56522d52081c25124982e6f1be9285433c45 AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk@sha256:8fc6dd50b2736c638f3dcdba2604ff9a36a7b6d85b385484c8014ff7ba2344de AS build
WORKDIR /src
COPY Containo.sln ./
COPY Containo.Services.Orders.QueueProcessor/Containo.Services.Orders.QueueProcessor.csproj Containo.Services.Orders.QueueProcessor/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/Containo.Services.Orders.QueueProcessor
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Containo.Services.Orders.QueueProcessor.dll"]
