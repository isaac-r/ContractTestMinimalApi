namespace ContractTestingApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var app = CreateApp(args);
            app.Run();
        }

        public static WebApplication CreateApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigureServices(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/api/getExpenseById/{id:int}", (int id) =>
            {
                return Results.Ok(new
                {
                    Id = id,
                    Name = "Test Expense",
                    Amount = 100.00,
                });
            });

            app.MapGet("/api/getAllExpenses", () =>
            {
                return Results.Ok(new[]
                {
                    new { Id = 1, Name = "Test Expense 1", Amount = 100.00 },
                    new { Id = 2, Name = "Test Expense 2", Amount = 200.00 },
                });
            });

            return app;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAuthorization();
        }
    }
}
