using ContractTestMinimalApi.Api;
using PactNet;
using PactNet.Matchers;

namespace ContractTestMinimalApi.Test
{
    public class ConsumerTest
    {
        private readonly string _directory = $"../../../../pacts";

        [Fact]
        [Trait("Category", "Consumer")]
        public async Task GivenExpenseExists_WhenGetExpenseById_ThenExpectedExpenseReturned()
        {
            // Arrange
            var pact = Pact.V4("ExpenseConsumer", "ExpenseProvider", new PactConfig
            {
                PactDir = _directory
            }).WithHttpInteractions();

            var expectedExpense = new Expense(1, "Test Expense", 100.0);

            var interaction = pact
                .UponReceiving("A request to get an expense by ID")
                .WithRequest(HttpMethod.Get, "/api/getExpenseById/1")
                .WillRespond()
                .WithStatus(200)
                .WithJsonBody(expectedExpense);

            // Act
            await pact.VerifyAsync(async mockProvider =>
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = mockProvider.MockServerUri
                };
                var api = new ApiClient(httpClient);
                var actualExpense = await api.GetExpenseById(1);

                // Assert
                Assert.Equal(expectedExpense.Id, actualExpense.Id);
                Assert.Equal(expectedExpense.Name, actualExpense.Name);
                Assert.Equal(expectedExpense.Amount, actualExpense.Amount);
            });
        }

        [Fact]
        [Trait("Category", "Consumer")]
        public async Task GivenExpensesExist_WhenGetAllExpenses_ThenAllExpensesReturned()
        {
            // Arrange
            var pact = Pact.V4("AllExpensesConsumer", "AllExpensesProvider", new PactConfig
            {
                PactDir = _directory
            }).WithHttpInteractions();

            var expectedExpenses = new List<Expense>()
            {
                new (2, "Test Expense 2", 200.0),
                new (1, "Test Expense", 100.0)
            };

            var interaction = pact
                .UponReceiving("A request to get all expenses")
                .WithRequest(HttpMethod.Get, "/api/getAllExpenses")
                .WillRespond()
                .WithStatus(200)
                .WithJsonBody(Match.Type(expectedExpenses));

            // Act
            await pact.VerifyAsync(async mockProvider =>
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = mockProvider.MockServerUri
                };
                var api = new ApiClient(httpClient);
                var actualExpense = await api.GetAllExpenses();

                // Assert
                Assert.Equal(expectedExpenses.Count, actualExpense.Count());
            });
        }
    }
}