FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Sample.MassTransit.K8s.Worker.Consumer/Sample.MassTransit.K8s.Worker.Consumer.csproj", "Sample.MassTransit.K8s.Worker.Consumer/"]
RUN dotnet restore "Sample.MassTransit.K8s.Worker.Consumer/Sample.MassTransit.K8s.Worker.Consumer.csproj"
COPY . .
WORKDIR "/src/Sample.MassTransit.K8s.Worker.Consumer"
RUN dotnet build "Sample.MassTransit.K8s.Worker.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample.MassTransit.K8s.Worker.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.MassTransit.K8s.Worker.Consumer.dll"]

# docker build . -f Sample.MassTransit.K8s.Worker.Consumer/Dockerfile -t "dmm91/sample-worker-consumer-mass-transit-k8s:latest"
# docker image push dmm91/sample-api-mass-transit-k8s:latest
# docker run -e ASPNETCORE_ENVIRONMENT='Development' -d dmm91/sample-worker-consumer-mass-transit-k8s:latest
