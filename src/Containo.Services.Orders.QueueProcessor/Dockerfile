FROM microsoft/dotnet:2.1-runtime@sha256:d195f6853eb64ae900745c346a9c14bc4e03d78f2cd97335106f4da80417399c AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk@sha256:5ce14e07bd69096f15f4792a85baf46983e655f6535fc87b7235373f3cec99a3 AS build
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
