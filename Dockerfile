FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY . .
RUN dotnet restore

# build app
WORKDIR /app/Kaizen
RUN dotnet build -c Release --no-restore

# publish app
FROM build as publish
RUN dotnet publish -c Release -o /out --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=publish /out .
ENTRYPOINT [ "dotnet", "Kaizen.dll" ]
