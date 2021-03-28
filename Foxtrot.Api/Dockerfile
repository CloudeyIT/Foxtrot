FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Foxtrot.Api/Foxtrot.Api.csproj", "Foxtrot.Api/"]
RUN dotnet restore "Foxtrot.Api/Foxtrot.Api.csproj"
COPY . .
WORKDIR "/src/Foxtrot.Api"
RUN dotnet build "Foxtrot.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Foxtrot.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Foxtrot.Api.dll"]
