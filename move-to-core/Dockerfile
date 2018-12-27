FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["move-to-core/MoveToCore.csproj", "move-to-core/"]
RUN dotnet restore "move-to-core/MoveToCore.csproj"
COPY . .
WORKDIR "/src/move-to-core"
RUN dotnet build "MoveToCore.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MoveToCore.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MoveToCore.dll"]