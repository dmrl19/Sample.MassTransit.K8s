FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Sample.MassTransit.K8s.Api/Sample.MassTransit.K8s.Api.csproj", "Sample.MassTransit.K8s.Api/"]
COPY ["Sample.MassTransit.K8s.Models/Sample.MassTransit.K8s.Models.csproj", "Sample.MassTransit.K8s.Models/"]

RUN dotnet restore "Sample.MassTransit.K8s.Api/Sample.MassTransit.K8s.Api.csproj"
COPY . .
WORKDIR "/src/Sample.MassTransit.K8s.Api"
RUN dotnet build "Sample.MassTransit.K8s.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample.MassTransit.K8s.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.MassTransit.K8s.Api.dll"]


# docker build . -f Sample.MassTransit.K8s.Api/Dockerfile -t "dmm91/sample-api-mass-transit-k8s:latest"
# docker image push dmm91/sample-api-mass-transit-k8s:latest
# docker run -e ASPNETCORE_ENVIRONMENT='Development' -p 9999:80 -d dmm91/sample-api-mass-transit-k8s:latest
