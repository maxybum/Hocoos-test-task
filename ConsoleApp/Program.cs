using ConsoleApp.Helpers;
using ConsoleApp.Tasks;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = ServiceContainer.GetServiceProvider();

//var task1 = serviceProvider.GetRequiredService<ThreadBackendTask>();
//await task1.RunAsync();

//var task2 = serviceProvider.GetRequiredService<ExpressionBackendTask>();
//await task2.RunAsync();

var task3 = serviceProvider.GetRequiredService<ThreadsLimitedThreadBackendTask>();
await task3.RunAsync();

var task4 = serviceProvider.GetRequiredService<FieldsLimitedExpressionBackendTask>();
await task4.RunAsync();


