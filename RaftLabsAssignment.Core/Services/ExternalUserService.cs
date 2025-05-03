using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RaftLabsAssignment.Core.Models;

namespace RaftLabsAssignment.Core.Services;

public class ExternalUserService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public ExternalUserService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration["ApiBaseUrl"] ?? "https://reqres.in/api/";
        _httpClient.BaseAddress = new Uri(_baseUrl);

         _httpClient.DefaultRequestHeaders.Add("x-api-key", "reqres-free-v1");
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"users/{userId}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiUserResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result?.Data;
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var users = new List<User>();
        int page = 1;

        while (true)
        {
            var response = await _httpClient.GetAsync($"users?page={page}");
            if (!response.IsSuccessStatusCode) break;

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiUserListResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (result?.Data == null || result.Data.Count == 0) break;

            users.AddRange(result.Data);
            if (page >= result.TotalPages) break;
            page++;
        }

        return users;
    }

    private class ApiUserResponse
    {
        public User Data { get; set; }
    }

    private class ApiUserListResponse
    {
        public List<User> Data { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
    }
}
