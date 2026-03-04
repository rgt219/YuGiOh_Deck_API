# Stage 1: Build the application
FROM ://mcr.microsoft.com AS build
WORKDIR /src
COPY ["YuGiOhDeckApi.csproj", "./"]
RUN dotnet restore "YuGiOhDeckApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM ://mcr.microsoft.com AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "YuGiOhDeckApi.dll"]