#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:3.1-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["ECommerca.Api.Products/ECommerca.Api.Products.csproj", "ECommerca.Api.Products/"]
RUN dotnet restore "ECommerca.Api.Products/ECommerca.Api.Products.csproj"
COPY . .
WORKDIR "/src/ECommerca.Api.Products"
RUN dotnet build "ECommerca.Api.Products.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerca.Api.Products.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerca.Api.Products.dll"]