// See https://aka.ms/new-console-template for more information
using System.Net.Http;
using JAConsole;
using JAConsoleBL;
using JAConsoleDL;

// string connectionString = File.ReadAllText("./connectionString.txt");

// IRepo repo = new DBRespository(connectionString);


// IJABL bl = new ConsoleProjBL(repo);
ConsoleProjectUI.HttpService httpService = new ConsoleProjectUI.HttpService();
await new MainConsole(httpService).PrivilageCheck();


