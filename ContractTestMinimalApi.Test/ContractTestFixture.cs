using ContractTestingApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractTestMinimalApi.Test
{
    public class ContractTestFixture
    {
        public async Task InitializeAsync()
        {
            var _app = Program.CreateApp(new string[] { });
            _app.Urls.Clear();
            _app.Urls.Add("https://localhost:5000");
            await _app.StartAsync();
        }
    }
}
