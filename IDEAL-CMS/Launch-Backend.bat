@echo off
SETLOCAL EnableDelayedExpansion

echo ==========================================
echo    IDEAL ERP - BACKEND STARTER
echo ==========================================
echo.

:: 1. Check for .NET
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo [ERROR] .NET SDK not found. Please install .NET 8.0 SDK.
    echo Download: https://dotnet.microsoft.com/download/dotnet/8.0
    pause
    exit /b
)
echo [OK] .NET SDK verified.

:: 2. Check for Docker
docker version >nul 2>&1
if %errorlevel% neq 0 (
    echo [WARNING] Docker not found or not running. 
    echo Database ^(PostgreSQL^) may not start automatically.
    echo Please start Docker Desktop.
    pause
) else (
    echo [OK] Docker verified.
    echo Starting Database...
    docker-compose up -d
    if %errorlevel% neq 0 (
        docker compose up -d
    )
)

:: 3. Restore and Run
echo.
echo Restoring dependencies...
cd src\IDEAL.ERP.Api
dotnet restore

echo.
echo Building and Launching API...
dotnet run

pause
