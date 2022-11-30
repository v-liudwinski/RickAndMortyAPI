FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /app

COPY . ./
RUN dotnet restore "./RickAndMorty.API/RickAndMorty.API/RickAndMorty.API.csproj"

COPY . ./
RUN dotnet publish "./RickAndMorty.API/RickAndMorty.API/RickAndMorty.API.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app

EXPOSE 8075/tcp
COPY --from=build /app/out .

ENTRYPOINT [ "dotnet", "RickAndMorty.API.dll" ]