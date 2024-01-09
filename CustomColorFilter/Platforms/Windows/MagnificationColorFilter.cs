using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CustomColorFilter.Contracts;

namespace CustomColorFilter.Platforms.Windows;

internal class MagnificationColorFilter : IColorFilter
{
    public void Initialize()
    {
        NativeMethods.MagInitialize();
    }

    public void SetFullScreenColorFilter(float[,] matrix)
    {
        if (matrix.GetLength(0) != 5 || matrix.GetLength(1) != 5)
        {
            throw new ArgumentException("Only matrix with dimension 5x5 allowed");
        }
        var asArray = matrix.Cast<float>().ToArray();
        var effect = new NativeMethods.MAGCOLOREFFECT()
        {
            transform = asArray,
        };
        NativeMethods.MagSetFullscreenColorEffect(ref effect);
    }

    public void Uninitialize()
    {
        NativeMethods.MagUninitialize();
    }
}

// Copied from https://github.com/yanna92yar/greys
static class NativeMethods
{
    const string Magnification = "Magnification.dll";

    [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
    public static extern bool MagInitialize();

    [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
    public static extern bool MagUninitialize();

    [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
    public static extern bool MagSetFullscreenColorEffect(ref MAGCOLOREFFECT pEffect);

    public struct MAGCOLOREFFECT
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
        public float[] transform;
    }
}
