#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ContactsInsertProducer.Api/FIAP.TechChallenge.ContactsConsult.Api.csproj", "ContactsInsertProducer.Api/"]
COPY ["ContactsInsertProducer.Application/FIAP.TechChallenge.ContactsConsult.Application.csproj", "ContactsInsertProducer.Application/"]
COPY ["ContactsInsertProducer.Domain/FIAP.TechChallenge.ContactsConsult.Domain.csproj", "ContactsInsertProducer.Domain/"]
COPY ["ContactsInsertProducer.Infrastructure/FIAP.TechChallenge.ContactsConsult.Infrastructure.csproj", "ContactsInsertProducer.Infrastructure/"]
COPY ["ContactsInsertProducer.Integrations/FIAP.TechChallenge.ContactsConsult.Infrastructure.csproj", "ContactsInsertProducer.Integrations/"]
RUN dotnet restore "./ContactsInsertProducer.Api/FIAP.TechChallenge.ContactsConsult.Api.csproj"
COPY . .
WORKDIR "/src/ContactsInsertProducer.Api"
RUN dotnet build "./FIAP.TechChallenge.ContactsInsertProducer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FIAP.TechChallenge.ContactsInsertProducer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FIAP.TechChallenge.ContactsConsult.Api.dll"]