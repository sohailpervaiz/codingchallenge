using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;

            // TODO: data preparation and initialisation of additional data structures to improve performance goes here.

        }


        public SearchResults Search(SearchOptions options)
        {
            // TODO: search logic goes here.


            var shirtResults = _shirts.FindAll(x => options.Colors.Contains(x.Color) && options.Sizes.Contains(x.Size));

            return new SearchResults
            {
                Shirts = shirtResults,
                ColorCounts = GetColorCounts(options),
                SizeCounts = GetSizeCounts(options)
            };
        }

        private List<ColorCount> GetColorCounts(SearchOptions options)
        {
            return Color.All.Select(o => new ColorCount
            {
                Color = o,
                Count = _shirts.Count(c => c.Color.Id == o.Id
                                 && (!options.Sizes.Any() || options.Sizes.Select(s => s.Id).Contains(c.Size.Id)))

            }).ToList();
        }

        private List<SizeCount> GetSizeCounts(SearchOptions options)
        {
            return Size.All.Select(o => new SizeCount
            {
                Size = o,
                Count = _shirts.Count(s => s.Size.Id == o.Id
                                && (!options.Colors.Any() || options.Colors.Select(c => c.Id).Contains(s.Color.Id)))

            }).ToList();
        }
    }
}