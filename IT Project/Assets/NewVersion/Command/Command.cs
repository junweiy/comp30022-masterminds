
namespace Command
{
    public interface ICommand
    {
        void ProcessCommand();
        int GetFrame();
        string Show();
        CommandType GetType();
    }
}
