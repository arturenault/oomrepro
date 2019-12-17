FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
RUN apt-get update && apt-get install -y procps
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["OOMRepro/OOMRepro.csproj", "OOMRepro/"]

RUN dotnet restore "OOMRepro/OOMRepro.csproj"
COPY . .
WORKDIR "/src/OOMRepro"
RUN dotnet build "OOMRepro.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OOMRepro.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "OOMRepro.dll"]