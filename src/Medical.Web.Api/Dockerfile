#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Medical.Web.Api/Medical.Web.Api.csproj", "Medical.Web.Api/"]
COPY ["Medical.Application/Medical.Application.csproj", "Medical.Application/"]
COPY ["Medical.Domain/Medical.Domain.csproj", "Medical.Domain/"]
COPY ["Medical.CrossCutting.Common/Medical.CrossCutting.Common.csproj", "Medical.CrossCutting.Common/"]
COPY ["Medical.CrossCutting.IoC/Medical.CrossCutting.IoC.csproj", "Medical.CrossCutting.IoC/"]
COPY ["Medical.Infra.Data/Medical.Infra.Data.csproj", "Medical.Infra.Data/"]
RUN dotnet restore "Medical.Web.Api/Medical.Web.Api.csproj"
COPY . .
WORKDIR "/src/Medical.Web.Api"
RUN dotnet build "Medical.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Medical.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Medical.Web.Api.dll"]