// File: RaftLabsAssignment.Tests/ExternalUserServiceTests.cs
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RaftLabsAssignment.Core.Services;
using Xunit;
using Xunit.Abstractions;

public class ExternalUserServiceTests
{
    private readonly ExternalUserService _userService;
    private readonly ITestOutputHelper _output;

    public ExternalUserServiceTests(ITestOutputHelper output)
    {
        _output = output;

        var inMemorySettings = new Dictionary<string, string?>
        {
            ["ApiBaseUrl"] = "https://reqres.in/api/"
        };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(config["ApiBaseUrl"]!)
        };

        _userService = new ExternalUserService(httpClient, config);
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnUser_WhenValidIdIsProvided()
    {
        var user = await _userService.GetUserByIdAsync(2);

        _output.WriteLine($"ID: {user?.Id}, Name: {user?.First_Name} {user?.Last_Name}");

        Assert.NotNull(user);
        Assert.Equal(2, user.Id);
        Assert.False(string.IsNullOrWhiteSpace(user.First_Name));
    }

    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnMultipleUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        foreach (var user in users)
        {
            _output.WriteLine($"ID: {user.Id}, Name: {user.First_Name} {user.Last_Name}, Email: {user.Email}");
        }
        Assert.NotNull(users);
        Assert.NotEmpty(users);
    }
}

