using PactNet.Verifier;
using PactNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using PactNet.Output.Xunit;
using PactNet.Infrastructure.Outputters;

namespace ContractTestMinimalApi.Test
{
    public class ProviderTest : IClassFixture<ContractTestFixture>
    {
        private readonly string _directory = "../../../../pacts";
        private readonly ContractTestFixture _fixture;
        private readonly ITestOutputHelper _output;

        public ProviderTest(ContractTestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        [Fact]
        [Trait("Category", "Provider")]
        public async Task GivenValidRequest_WhenGetExpenseById_ThenExpectedResponse()
        {
            await _fixture.InitializeAsync();

            var pactFile = $"{_directory}/ExpenseConsumer-ExpenseProvider.json";

            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new XunitOutput(_output)
                },
                LogLevel = PactLogLevel.Information
            };

            new PactVerifier("ExpenseProvider", config)
                .WithHttpEndpoint(new Uri("https://localhost:5000"))
                .WithFileSource(new FileInfo(pactFile))
                .Verify();
        }

        [Fact]
        [Trait("Category", "Provider")]
        public async Task GivenValidRequest_WhenGetAllExpenses_ThenExpectedResponse()
        {
            await _fixture.InitializeAsync();

            var pactFile = $"{_directory}/AllExpensesConsumer-AllExpensesProvider.json";

            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new XunitOutput(_output)
                },
                LogLevel = PactLogLevel.Information
            };

            new PactVerifier("AllExpensesProvider", config)
                .WithHttpEndpoint(new Uri("https://localhost:5000"))
                .WithFileSource(new FileInfo(pactFile))
                .Verify();
        }
    }
}
