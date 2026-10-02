From mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY PlatformService.csproj ./

RUN dotnet restore PlatformService.csproj

COPY . ./

RUN dotnet publish -c release -o output

From mcr.microsoft.com/dotnet/aspnet:10.0 AS  run

WORKDIR /app

COPY --from=build /app/output ./

ENTRYPOINT [ "dotnet","PlatformService.dll" ]