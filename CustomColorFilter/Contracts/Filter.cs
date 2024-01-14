using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomColorFilter.Contracts
{
    public record Filter(string Name, float[] Matrix);
}
