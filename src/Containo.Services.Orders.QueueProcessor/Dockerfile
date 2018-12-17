FROM microsoft/dotnet:2.2.100-sdk AS build
WORKDIR /src
COPY Containo.sln ./
COPY Containo.Services.Orders.QueueProcessor/Containo.Services.Orders.QueueProcessor.csproj Containo.Services.Orders.QueueProcessor/
COPY . .
WORKDIR /src/Containo.Services.Orders.QueueProcessor
RUN dotnet publish -c Release -o /app

FROM microsoft/dotnet:2.2.0-runtime as runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Containo.Services.Orders.QueueProcessor.dll"]
