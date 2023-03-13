#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/azure-functions/dotnet:4 AS base
WORKDIR /home/site/wwwroot
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Demo.HttpTrigger.Function/Demo.HttpTrigger.Function.csproj", "Demo.HttpTrigger.Function/"]
RUN dotnet restore "Demo.HttpTrigger.Function/Demo.HttpTrigger.Function.csproj"
COPY . .
WORKDIR "/src/Demo.HttpTrigger.Function"
RUN dotnet build "Demo.HttpTrigger.Function.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo.HttpTrigger.Function.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true