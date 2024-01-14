using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomColorFilter.Contracts
{
    public record Configuration(string SelectedFilter, bool ApplyDefaultFilterOnStart, IReadOnlyList<Filter> CustomFilters);
}
