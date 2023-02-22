using System.Collections.Generic;
using azure_one.Etl.Shared.Infrastructure.Repositories;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class TruncateTableService: AbsRawService
{
    private readonly TruncateRepository _truncateRepository;
    private readonly List<string> _tables = new (){ "languages", "countries"};
    
    public TruncateTableService(TruncateRepository truncateRepository)
    {
        _truncateRepository = truncateRepository;
    }
    
    public override void Invoke()
    {
        foreach (string table in _tables)
        {
            _truncateRepository.TruncateTable(table);    
        }
    }
}