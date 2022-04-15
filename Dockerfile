#FROM keyword sets our base image to build our own image upon

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ENV ASPNETCORE_URLS=http://+:5000

#Now we have the ability to compile and run .net 6 applications
#Next, we set workdirectory to execute all subsequent COPY, ADD, CMD, ENTRYPOINT
#and RUN commands on
#If there is not one, it will be created
WORKDIR /app

#Once workdirectory is set, going to copy sourcecode over
#copy everything in P1 demo, over to app folder
COPY . .

#We restore and build our application
RUN dotnet clean ConsoleProj.sln
RUN dotnet publish WebAPI --configuration Release -o ./publish

#Multistage build
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS run

WORKDIR /app

COPY --from=build /app/publish . 
#Expost the port that host needs for swagger

#WORKDIR /app/publish

CMD ["dotnet", "WebAPI.dll"]
#docker build . -t novaflash/stacklite:1.0.3
#docker run -d -p 5000:5000 novaflash/stacklite:tag