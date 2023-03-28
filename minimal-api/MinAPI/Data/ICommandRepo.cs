using MinApi.Models;

namespace MinApi.Data
{
    public interface ICommandRepo
    {
        Task SaveChanges();

        Task<Command?> GetSingleCommandById(int id);

        Task<IEnumerable<Command>> GetAllCommands();

        Task CreateCommand(Command cmd);

        void DeleteCommand(Command cmd);
    }
}