# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY global.json ./
COPY Directory.Build.props ./
COPY RazorCicdDemo.slnx ./
COPY src/RazorCicdDemo.Web/RazorCicdDemo.Web.csproj src/RazorCicdDemo.Web/

RUN dotnet restore src/RazorCicdDemo.Web/RazorCicdDemo.Web.csproj

COPY src/ src/

RUN dotnet publish src/RazorCicdDemo.Web/RazorCicdDemo.Web.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_RUNNING_IN_CONTAINER=true

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "RazorCicdDemo.Web.dll"]
