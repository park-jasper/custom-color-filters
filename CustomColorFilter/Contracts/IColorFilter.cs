using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomColorFilter.Contracts
{
    public interface IColorFilter
    {
        void Initialize();
        void Uninitialize();
        void SetFullScreenColorFilter(float[,] matrix);
    }
}
