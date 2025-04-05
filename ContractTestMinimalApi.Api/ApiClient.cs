using System.Text.Json.Serialization;

namespace ContractTestMinimalApi.Api
{
    public class ApiClient(HttpClient httpClient)
    {
        public async Task<Expense> GetExpenseById(int id)
        {
            var response = await httpClient.GetFromJsonAsync<Expense>($"/api/getExpenseById/{id}");
            return response!;
        }
    }

    public record Expense
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        public Expense(int id, string name, double amount)
        {
            Id = id;
            Name = name;
            Amount = amount;
        }
    }
}
