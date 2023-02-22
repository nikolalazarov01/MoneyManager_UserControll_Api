using Core.Contracts;
using Core.Contracts.Services;
using Data.Repository;
using Data.Repository.Contracts;

namespace Core.Services;

public class UnitOfWork : IUnitOfWork
{
    public IUserService Users { get; private set; }

    public UnitOfWork(IUserRepository userRepository)
    {
        Users = new UserService(userRepository);
    }
}