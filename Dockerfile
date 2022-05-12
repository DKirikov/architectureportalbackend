ARG PROJECT_NAME=ArchitecturePortalBackend
ARG CONFIGURATION=Release

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder
ARG CONFIGURATION
ARG PROJECT_NAME
WORKDIR /src
COPY ./src/ArchitecturePortalBackend.sln ./ArchitecturePortalBackend.sln
COPY ./src/ArchitecturePortalBackend.API/ArchitecturePortalBackend.API.csproj ./ArchitecturePortalBackend.API/ArchitecturePortalBackend.API.csproj
COPY ./src/ArchitecturePortalBackend.BusinessLogic/ArchitecturePortalBackend.BusinessLogic.csproj ./ArchitecturePortalBackend.BusinessLogic/ArchitecturePortalBackend.BusinessLogic.csproj
COPY ./src/ArchitecturePortalBackend.DataAccess/ArchitecturePortalBackend.DataAccess.csproj ./ArchitecturePortalBackend.DataAccess/ArchitecturePortalBackend.DataAccess.csproj
COPY ./src/ArchitecturePortalBackend.Migrator/ArchitecturePortalBackend.Migrator.csproj ./ArchitecturePortalBackend.Migrator/ArchitecturePortalBackend.Migrator.csproj
COPY ./src/ArchitecturePortalBackend.VaultProvider/ArchitecturePortalBackend.VaultProvider.csproj ./ArchitecturePortalBackend.VaultProvider/ArchitecturePortalBackend.VaultProvider.csproj

#RUN dotnet restore --configfile $NUGET_CONFIG_FILE -v:minimal -warnaserror
RUN dotnet restore -v:minimal -warnaserror

COPY src/ .

RUN dotnet build --no-restore -c ${CONFIGURATION} -v:minimal && \
    dotnet test --no-build --no-restore -c ${CONFIGURATION} -l trx -r /results /p:CollectCoverage=true /p:CoverletOutputFormat='json%2copencover' /p:CoverletOutput=/results/coverage /p:MergeWith="/results/coverage.json"


RUN if [ -f ${PROJECT_NAME}.API/${PROJECT_NAME}.API.csproj ]; then \
      dotnet publish --no-build -c ${CONFIGURATION} -o /app/host ${PROJECT_NAME}.API/${PROJECT_NAME}.API.csproj; \
    fi && \
    if [ -f ${PROJECT_NAME}.Migrator/${PROJECT_NAME}.Migrator.csproj ]; then \
      dotnet publish --no-build -c ${CONFIGURATION} -o /app/migrator ${PROJECT_NAME}.Migrator/${PROJECT_NAME}.Migrator.csproj; \
    fi

FROM builder AS test
ARG build
LABEL build=$build
LABEL image=test
RUN ( if [ -d /results/ ]; then /tools/trx2junit /results/*.trx --output /results/junit/; fi ) && \
    ( if [ -f /results/coverage.opencover.xml ]; then /tools/reportgenerator -reports:/results/coverage.opencover.xml -targetdir:/results/coverage; fi )


FROM base AS final
WORKDIR /app/host

COPY --from=builder /app /app
ENTRYPOINT ["dotnet", "ArchitecturePortalBackend.API.dll"]