using System.Collections.Generic;
using Info.Controllers;

namespace Info
{
    public class ProcessedInformationService : IProcessedInformationService
    {
        public IEnumerable<ProcessedInformation> GetAll()
        {
            return new List<ProcessedInformation>
            {
                new ProcessedInformation
                {
                    Total = new Total
                    {
                        Value = 2000m
                    }
                }
            };
        }
    }
}