using FinanceTracker.Console.ApiClients;
using FinanceTracker.Console.Menus;

const string ApiBaseUrl = "http://localhost:5062/";

var httpClient = new HttpClient { BaseAddress = new Uri(ApiBaseUrl) };

var userClient        = new UserApiClient(httpClient);
var transactionClient = new TransactionApiClient(httpClient);
var budgetClient      = new BudgetApiClient(httpClient);
var reportClient      = new ReportApiClient(httpClient);

var userMenu        = new UserMenu(userClient);
var transactionMenu = new TransactionMenu(transactionClient);
var budgetMenu      = new BudgetMenu(budgetClient);
var reportMenu      = new ReportMenu(reportClient);

var mainMenu = new MainMenu(userMenu, transactionMenu, budgetMenu, reportMenu);

await mainMenu.RunAsync();
