FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as base
ARG CONFIG=Release
WORKDIR /app

FROM base as build
ARG CONFIG=Release
COPY ./github-actions-webapi/github-actions-webapi.csproj ./github-actions-webapi/github-actions-webapi.csproj
COPY ./WebApiModels/WebApiModels.csproj ./WebApiModels/WebApiModels.csproj

COPY github-actions-poc.sln .
RUN dotnet restore github-actions-poc.sln

COPY . .
RUN dotnet publish ./github-actions-webapi/github-actions-webapi.csproj -c $CONFIG -o /publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as runtime
WORKDIR /app

COPY --from=build /publish .
CMD ["dotnet", "github-actions-webapi.dll"]:
