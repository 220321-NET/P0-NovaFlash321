#FROM keyword sets our base image to build our own image upon

FROM mcr.microsoft.com/dotnet/sdk:6.0

#Now we have the ability to compile and run .net 6 applications
#Next, we set workdirectory to execute all subsequent COPY, ADD, CMD, ENTRYPOINT
#and RUN commands on
#If there is not one, it will be created
WORKDIR /app

#Once workdirectory is set, going to copy sourcecode over
#copy everything in P1 demo, over to app folder
COPY . .

#We restore and build our application
RUN dotnet build
RUN dotnet publish WebAPI --configuration Debug -o ./publish

#Expost the port that host needs for swagger
EXPOSE 7143
ENTRYPOINT [ "./publish/WebAPI.dll" ]

CMD ["dotnet", "./publish/WebAPI.dll"]