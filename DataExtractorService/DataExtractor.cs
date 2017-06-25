using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace DataExtractorService
{
    public partial class DataExtractor : ServiceBase
    {
        public DataExtractor()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            eventLog.WriteEntry("Starting extraction service", EventLogEntryType.Information);

            while (true)
            {


                eventLog.WriteEntry("Starting extraction sleeping", EventLogEntryType.Information);
                Thread.Sleep(120000);
                eventLog.WriteEntry("Starting extraction waking up", EventLogEntryType.Information);
            }
        }

        protected override void OnStop()
        {

            eventLog.WriteEntry("Stopping extraction service", EventLogEntryType.Information);
        }
    }
}
