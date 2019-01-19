FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["SEContactManager.UI.Web/SEContactManager.UI.Web.csproj", "SEContactManager.UI.Web/"]
RUN dotnet restore "SEContactManager.UI.Web/SEContactManager.UI.Web.csproj"
COPY . .
WORKDIR "/src/SEContactManager.UI.Web"
RUN dotnet build "SEContactManager.UI.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SEContactManager.UI.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SEContactManager.UI.Web.dll"]