using System.Threading.Tasks;
using DTO;

namespace ShareInfo.DataExtraction
{
    public abstract class ShareDataExtractor
    {
        public ShareDataExtractor Successor { protected get; set; }

        public abstract Task<ShareExtract> GetExtract(string symbol);
    }
}
