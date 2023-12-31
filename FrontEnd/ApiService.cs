﻿using System.Net.Http;
using System.Text;
using BackEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd;

public class ApiService {

    private const string ApiBaseUrl = "https://localhost:5293";

    private readonly HttpClient _httpClient;

    public ApiService() {
        _httpClient = new HttpClient();
    }

    public async Task<HttpResponseMessage> GetAsync(string apiUrl) {
        try {
            var response = await _httpClient.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();

            return response;
        } catch (HttpRequestException ex) {
            Console.WriteLine($"An error occurred while making the GET request: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> AddUserAsync(User user) {
        try {
            using (var httpClient = new HttpClient()) {

                var apiUrl = $"{ApiBaseUrl}/api/v1/users";

                var jsonUser = JsonConvert.SerializeObject(user);

                var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode) {
                    return true;
                } else {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return false;
                }
            }
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateUser(User user) {
        try {
            // Convert User object to JSON
            var jsonUser = JsonConvert.SerializeObject(user);

            // Create a StringContent with JSON data
            var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            // Send the HTTP PATCH request
            var response = await _httpClient.PatchAsync($"{ApiBaseUrl}/{user.Id}", content);

            // Check if the request was successful (status code 200)
            if (response.IsSuccessStatusCode) {
                return true;
            }

            // Handle other status codes or errors if needed
            // You can check the response.StatusCode and response.Content properties for more details

            return false;
        } catch (Exception ex) {
            // Handle exceptions
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteUser(int userId) {
        try {
            string apiUrl = ApiBaseUrl + $"/api/v1/users/{userId}";

            HttpResponseMessage response = await _httpClient.DeleteAsync(apiUrl);

            response.EnsureSuccessStatusCode();

            return true;
        } catch (HttpRequestException) {
            return false;
        }
    }
}