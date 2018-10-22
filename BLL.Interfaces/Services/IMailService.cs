using BLL.Interfaces.Args;
using System;

namespace BLL.Interfaces.Services
{
    public interface IMailService<in T>
    {
        bool Send(T data);
    }
}
