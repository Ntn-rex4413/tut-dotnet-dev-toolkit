using DiApi.DataServices;

namespace DiApi.Data
{
    public class SqlDataRepo : IDataRepo
    {
        // if there's a need for some reason to use a scoped service within a singleton service
        // like in this example
        // it can be done by creating a scope with scope factory which disposes of the scoped
        // service instance

        private readonly IServiceScopeFactory _scopeFactory;

        public SqlDataRepo(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public string ReturnData()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("--> Getting data from SQL Database...");

            using (var scope = _scopeFactory.CreateScope())
            {
                var dataService = scope.ServiceProvider.GetRequiredService<IDataService>();

                dataService.GetProductData("https://something.com/api");

                Console.ResetColor();
            }

            return ("SQL Data from DB");
        }
    }
}
