#!/bin/sh
cd app

dotnet restore && dotnet build -f net6.0 --force