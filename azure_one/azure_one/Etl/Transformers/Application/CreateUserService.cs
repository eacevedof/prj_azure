using azure_one.Etl.Shared.Infrastructure.Repositories;

namespace azure_one.Etl.Application;

public sealed class CreateUserService: ServiceInterface
{
    private readonly UsersRepository UsersRepository;
    
    public CreateUserService(UsersRepository usersRepository)
    {
        this.UsersRepository = usersRepository;
    }
    
    public void InsertRandomUser()
    {
        this.UsersRepository.randomInsert();
    }

    public void PrintAll()
    {
        this.UsersRepository.printAllUsers();
    }
}