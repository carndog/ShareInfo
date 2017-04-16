using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareInfo
{
    public class ShareExtractorsDirector
    {
        private readonly IEnumerable<string> _symbols;

        public ShareExtractorsDirector(IEnumerable<string> symbols)
        {
            _symbols = symbols;
        }

        public async Task<IEnumerable<ShareExtract>> GetExtracts()
        {
            var list = new List<ShareExtract>(_symbols.Count());

            var ftse100FeedExtractor = new Ftse100FeedExtractor();
            var googleSearchExtractor = new GoogleSearchExtractor();

            ftse100FeedExtractor.Successor = googleSearchExtractor;

            foreach (string symbol in _symbols)
            {
                ShareExtract extract = await ftse100FeedExtractor.GetExtract(symbol);
                list.Add(extract);
            }

            return list;
        }
    }
}
