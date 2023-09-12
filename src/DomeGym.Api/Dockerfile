FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/DomeGym.Api/DomeGym.Api.csproj", "src/DomeGym.Api/"]
RUN dotnet restore "src/DomeGym.Api/DomeGym.Api.csproj"
COPY . .
WORKDIR "/src/src/DomeGym.Api"
RUN dotnet build "DomeGym.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DomeGym.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DomeGym.Api.dll"]
