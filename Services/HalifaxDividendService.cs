using DataStorage;
using DataStorage.Queries;
using DTO;

namespace Services
{
    public class HalifaxDividendService : HistoricEntityService<HalifaxDividend>, IHalifaxDividendService
    {
        public HalifaxDividendService(
            IHalifaxDividendRepository halifaxDividendRepository, 
            IDuplicateHalifaxDividendExistsQuery halifaxDividendExistsQuery) : 
            base(halifaxDividendRepository, halifaxDividendExistsQuery)
        {
        }
    }
}