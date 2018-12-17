FROM microsoft/dotnet:2.2.100-sdk AS build
WORKDIR /src
COPY Containo.sln ./
COPY Containo.Services.Orders.Validator/Containo.Services.Orders.Validator.csproj Containo.Services.Orders.Validator/
COPY Containo.Core.Api/Containo.Core.Api.csproj Containo.Core.Api/
COPY . .
WORKDIR /src/Containo.Services.Orders.Validator
RUN dotnet publish -c Release -o /app

FROM microsoft/dotnet:2.2.0-aspnetcore-runtime as runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Containo.Services.Orders.Validator.dll"]
