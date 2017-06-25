using System.ServiceProcess;

namespace DataExtractorService
{
    static class Program
    {
        static void Main()
        {
            ServiceBase[] servicesToRun = {
                new DataExtractor()
            };

            ServiceBase.Run(servicesToRun);
        }
    }
}
