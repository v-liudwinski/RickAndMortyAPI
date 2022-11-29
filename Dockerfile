FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./RickAndMorty.API/RickAndMorty.API/RickAndMorty.API.csproj" --disable-parallel
RUN dotnet publish "./RickAndMorty.API/RickAndMorty.API/RickAndMorty.API.csproj" -c release -o /app --no-restore
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT [ "dotnet", "RickAndMorty.API.dll" ]