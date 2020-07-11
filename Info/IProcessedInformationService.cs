using System.Collections.Generic;
using Info.Controllers;

namespace Info
{
    public interface IProcessedInformationService
    {
        IEnumerable<ProcessedInformation> GetAll();
    }
}