using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;

namespace ShareInfo.DataExtraction
{
    public class ShareExtractorsDirector
    {
        private readonly IEnumerable<string> _symbols;

        public ShareExtractorsDirector(IEnumerable<string> symbols)
        {
            _symbols = symbols;
        }

        public async Task<IEnumerable<AssetPrice>> GetExtracts()
        {
            List<AssetPrice> list = new List<AssetPrice>(_symbols.Count());

            Ftse100FeedExtractor ftse100FeedExtractor = new Ftse100FeedExtractor();
            LseSearchExtractor lseSearchExtractor = new LseSearchExtractor();

            ftse100FeedExtractor.Successor = lseSearchExtractor;

            foreach (string symbol in _symbols)
            {
                AssetPrice extract = await ftse100FeedExtractor.GetExtract(symbol);
                list.Add(extract);
            }

            return list;
        }
    }
}
