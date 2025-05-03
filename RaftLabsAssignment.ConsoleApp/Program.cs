using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RaftLabsAssignment.Core.Services;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var services = new ServiceCollection();
services.AddSingleton<IConfiguration>(config);
services.AddHttpClient<ExternalUserService>();
var provider = services.BuildServiceProvider();

var userService = provider.GetRequiredService<ExternalUserService>();

Console.WriteLine("Fetching all users...");
var allUsers = await userService.GetAllUsersAsync();
foreach (var user in allUsers)
    Console.WriteLine($"{user.Id}: {user.First_Name} {user.Last_Name} ({user.Email})");

Console.WriteLine("\nFetching user by ID...");
var singleUser = await userService.GetUserByIdAsync(2);
if (singleUser != null)
    Console.WriteLine($"{singleUser.Id}: {singleUser.First_Name} {singleUser.Last_Name} ({singleUser.Email})");
else
    Console.WriteLine("User not found.");
