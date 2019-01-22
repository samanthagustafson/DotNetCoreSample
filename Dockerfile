FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base

WORKDIR /app
EXPOSE 80

ENV CONNECTION_STRING="13.94.175.88:27017,13.94.168.121:27018,13.81.214.106:27019"
ENV KAFKA_HOST="13.94.175.88:9092"
ENV LOGSTASH_HOST="172.20.224.1"
ENV LOGSTASH_PORT="5000"

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["libs", "."]
COPY ["MoveToCore/MoveToCore.csproj", "MoveToCore/"]
RUN dotnet restore "MoveToCore/MoveToCore.csproj"
COPY . .
WORKDIR "/src/MoveToCore"
RUN dotnet build "MoveToCore.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MoveToCore.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY client/dist /client
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MoveToCore.dll"]