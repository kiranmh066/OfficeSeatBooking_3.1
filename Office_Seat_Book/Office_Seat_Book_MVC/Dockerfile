#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Office_Seat_Book_MVC/Office_Seat_Book_MVC.csproj", "Office_Seat_Book_MVC/"]
COPY ["OfficeAPI/OfficeAPI.csproj", "OfficeAPI/"]
COPY ["Office_Seat_Book_BLL/Office_Seat_Book_BLL.csproj", "Office_Seat_Book_BLL/"]
COPY ["Office_Seat_Book_DLL/Office_Seat_Book_DLL.csproj", "Office_Seat_Book_DLL/"]
COPY ["Office_Seat_Book_Entity/Office_Seat_Book_Entity.csproj", "Office_Seat_Book_Entity/"]
RUN dotnet restore "Office_Seat_Book_MVC/Office_Seat_Book_MVC.csproj"
COPY . .
WORKDIR "/src/Office_Seat_Book_MVC"
RUN dotnet build "Office_Seat_Book_MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Office_Seat_Book_MVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Office_Seat_Book_MVC.dll"]