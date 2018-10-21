namespace BLL.Interfaces.Services
{
    public interface IMailService<in T>
    {
        void Send(T data);
    }
}
