DOCKER_TAG=ArchitecturePortalBackend

image:
	docker build -t $(DOCKER_TAG) .

run-with-db: database-prep run-clean

run-clean: cleanup restore build run

all : cleanup restore build

cleanup:
	dotnet clean src/

init:
	dotnet tool install -g dotnet-reportgenerator-globaltool

restore:
	dotnet restore src/

build:
	dotnet build src/

test:
	dotnet test --no-build --no-restore -l trx -r `pwd`/results /p:CollectCoverage=true /p:CoverletOutputFormat='json%2copencover' /p:CoverletOutput=`pwd`/results/coverage /p:MergeWith="`pwd`/results/coverage.json" src/

    reportgenerator -reports:`pwd`/results/coverage.opencover.xml -targetdir:`pwd`/results/coverage

docker : image

    docker run --rm --env-file .env $(DOCKER_TAG)
