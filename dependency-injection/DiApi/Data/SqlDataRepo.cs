using DiApi.DataServices;

namespace DiApi.Data
{
    public class SqlDataRepo : IDataRepo
    {
        private readonly IDataService _dataService;

        public SqlDataRepo(IDataService dataService)
        {
            _dataService = dataService;
        }

        public string ReturnData()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("--> Getting data from SQL Database...");

            _dataService.GetProductData("https://something.com/api");

            Console.ResetColor();

            return ("SQL Data from DB");
        }
    }
}
