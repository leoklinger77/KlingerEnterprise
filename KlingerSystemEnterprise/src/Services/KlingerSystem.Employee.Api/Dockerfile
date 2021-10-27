#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/KlingerSystem.Employee.Api/KlingerSystem.Employee.Api.csproj", "src/Services/KlingerSystem.Employee.Api/"]
RUN dotnet restore "src/Services/KlingerSystem.Employee.Api/KlingerSystem.Employee.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/KlingerSystem.Employee.Api"
RUN dotnet build "KlingerSystem.Employee.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KlingerSystem.Employee.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KlingerSystem.Employee.Api.dll"]