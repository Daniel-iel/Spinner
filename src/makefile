restore:
	dotnet restore

build:
	dotnet build -c Release

test:
	dotnet test --no-build --verbosity normal /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

test-nocoverage:
	dotnet test --no-build --verbosity normal /p:CollectCoverage=false

test-mutant:
	cd Spinner.Test && dotnet stryker --reporter "html" -o

format:
	dotnet format '.\' -v diag --severity error --verbosity --no-restore

dependense-diagram:
	dependensee -S "src"  -O  "src/diagram.html"

analyze-package-usage:
	snitch

run-ci:
	act "on: pull_request"  -C ../