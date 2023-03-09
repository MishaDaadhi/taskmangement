#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TaskManager.Api/TaskManager.Api/TaskManager.Api.csproj", "TaskManager.Api/TaskManager.Api/"]
COPY ["Business.Model/TaskManager.Business.Model/TaskManager.Business.Model.csproj", "Business.Model/TaskManager.Business.Model/"]
COPY ["Business.Service/TaskManager.Business.Service/TaskManager.Business.Service.csproj", "Business.Service/TaskManager.Business.Service/"]
COPY ["TaskManager.Core.Common/TaskManager.Core.Common/TaskManager.Core.Common.csproj", "TaskManager.Core.Common/TaskManager.Core.Common/"]
COPY ["TaskManager.DataStore/TaskManager.DataStore/TaskManager.DataStore.csproj", "TaskManager.DataStore/TaskManager.DataStore/"]
COPY ["TaskManager.Core.DataStore/TaskManager.Core.DataStore/TaskManager.Core.DataStore.csproj", "TaskManager.Core.DataStore/TaskManager.Core.DataStore/"]
COPY ["TaskManager.Core.Services/TaskManager.Core.Services/TaskManager.Core.Services.csproj", "TaskManager.Core.Services/TaskManager.Core.Services/"]
COPY ["TaskManager.Api.Contracts/TaskManager.Api.Contracts/TaskManager.Api.Contracts.csproj", "TaskManager.Api.Contracts/TaskManager.Api.Contracts/"]
RUN dotnet restore "TaskManager.Api/TaskManager.Api/TaskManager.Api.csproj"
COPY . .
WORKDIR "/src/TaskManager.Api/TaskManager.Api"
RUN dotnet build "TaskManager.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskManager.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskManager.Api.dll"]