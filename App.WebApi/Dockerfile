FROM microsoft/dotnet:2.2-aspnetcore-runtime
ENTRYPOINT ["dotnet", "App.WebApi.dll"]
ARG source=.
WORKDIR /app
COPY $source .