#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/KlingerSystem.Business.Api/KlingerSystem.Business.Api.csproj", "src/Services/KlingerSystem.Business.Api/"]
RUN dotnet restore "src/Services/KlingerSystem.Business.Api/KlingerSystem.Business.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/KlingerSystem.Business.Api"
RUN dotnet build "KlingerSystem.Business.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KlingerSystem.Business.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KlingerSystem.Business.Api.dll"]