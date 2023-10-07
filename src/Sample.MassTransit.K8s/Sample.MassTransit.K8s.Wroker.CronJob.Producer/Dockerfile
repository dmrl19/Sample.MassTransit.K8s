FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Sample.MassTransit.K8s.Wroker.CronJob.Producer/Sample.MassTransit.K8s.Wroker.CronJob.Producer.csproj", "Sample.MassTransit.K8s.Wroker.CronJob.Producer/"]
RUN dotnet restore "Sample.MassTransit.K8s.Wroker.CronJob.Producer/Sample.MassTransit.K8s.Wroker.CronJob.Producer.csproj"
COPY . .
WORKDIR "/src/Sample.MassTransit.K8s.Wroker.CronJob.Producer"
RUN dotnet build "Sample.MassTransit.K8s.Wroker.CronJob.Producer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample.MassTransit.K8s.Wroker.CronJob.Producer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.MassTransit.K8s.Wroker.CronJob.Producer.dll"]

# docker build . -f Sample.MassTransit.K8s.Wroker.CronJob.Producer/Dockerfile -t "dmm91/sample-worker-cronjob-producer-mass-transit-k8s:latest"
# docker image push dmm91/sample-worker-cronjob-producer-transit-k8s:latest
# docker run -e ASPNETCORE_ENVIRONMENT='Development' -d dmm91/sample-worker-cronjob-producer-mass-transit-k8s:latest
