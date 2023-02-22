using Core.Contracts.Services;
using Data.Repository.Contracts;

namespace Core.Contracts;

public interface IUnitOfWork
{
    IUserService Users { get; }
}