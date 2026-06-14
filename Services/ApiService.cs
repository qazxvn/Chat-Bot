using System.Net.Http.Json;
using System.Text;
using ChatBot.Models;
using Microsoft.Identity.Client;

namespace ChatBot.Services;

public class ApiService(IHttpClientFactory httpClientFactory) : IApiService
{
    private static readonly Random _random = new();
    
    public async Task<(string title, string adress, string description, string subways)> RandomPlacePick()
    {
        var places = await GetPlaceForWalk();

        var place = places[_random.Next(places.Count)];

        return (place.title, place.adress, place.description, place.subways);
    }

    private async Task<List<(string title, string adress, string description, string subways)>> GetPlaceForWalk()
    {
        var client = httpClientFactory.CreateClient();

        try
        {
            var fields = "title,address,description,subway";

            var first = await client.GetFromJsonAsync<ApiResponseModel>($"https://kudago.com/public-api/v1.4/places/?location=spb&fields={fields}&page_size=100");
            var totalPages = (int)Math.Ceiling(first.Count / 100.0);
            var page = _random.Next(1, totalPages + 1);
            
            var placesList = await client.GetFromJsonAsync<ApiResponseModel>($"https://kudago.com/public-api/v1.4/places/?location=spb&fields={fields}&page={page}&page_size=100");

            var result = new List<(string, string, string, string)>();

            foreach (var place in placesList.Results)
            {
                var subway = "";
                
                if (place.Subway != null)
                {
                    subway = place.Subway;
                }

                result.Add((place.Title, place.Address, place.Description, subway));
            }

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}