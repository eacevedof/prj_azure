namespace azure_one.Etl.Shared.Domain.Repositories;

public interface ImpErrorsRepositoryInterface
{
    public void save(string error, string title = "");
}