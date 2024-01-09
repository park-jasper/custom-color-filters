using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomColorFilter
{
    public partial class FilterViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private bool isBuiltin;

        [ObservableProperty] private float f00;
        [ObservableProperty] private float f01;
        [ObservableProperty] private float f02;
        [ObservableProperty] private float f03;
        [ObservableProperty] private float f04;

        [ObservableProperty] private float f10;
        [ObservableProperty] private float f11;
        [ObservableProperty] private float f12;
        [ObservableProperty] private float f13;
        [ObservableProperty] private float f14;

        [ObservableProperty] private float f20;
        [ObservableProperty] private float f21;
        [ObservableProperty] private float f22;
        [ObservableProperty] private float f23;
        [ObservableProperty] private float f24;

        [ObservableProperty] private float f30;
        [ObservableProperty] private float f31;
        [ObservableProperty] private float f32;
        [ObservableProperty] private float f33;
        [ObservableProperty] private float f34;

        [ObservableProperty] private float f40;
        [ObservableProperty] private float f41;
        [ObservableProperty] private float f42;
        [ObservableProperty] private float f43;
        [ObservableProperty] private float f44;

        public FilterViewModel(string name, float[,] matrix, bool isBuiltin)
        {
            this.Name = name;

            this.F00 = matrix[0, 0];
            this.F01 = matrix[0, 1];
            this.F02 = matrix[0, 2];
            this.F03 = matrix[0, 3];
            this.F04 = matrix[0, 4];

            this.F10 = matrix[1, 0];
            this.F11 = matrix[1, 1];
            this.F12 = matrix[1, 2];
            this.F13 = matrix[1, 3];
            this.F14 = matrix[1, 4];

            this.F20 = matrix[2, 0];
            this.F21 = matrix[2, 1];
            this.F22 = matrix[2, 2];
            this.F23 = matrix[2, 3];
            this.F24 = matrix[2, 4];

            this.F30 = matrix[3, 0];
            this.F31 = matrix[3, 1];
            this.F32 = matrix[3, 2];
            this.F33 = matrix[3, 3];
            this.F34 = matrix[3, 4];

            this.F40 = matrix[4, 0];
            this.F41 = matrix[4, 1];
            this.F42 = matrix[4, 2];
            this.F43 = matrix[4, 3];
            this.F44 = matrix[4, 4];
        }

        public float[,] BuildMatrix()
            => new float[,]
            {
                { F00, F01, F02, F03, F04 },
                { F10, F11, F12, F13, F14 },
                { F20, F21, F22, F22, F24 },
                { F30, F31, F32, F33, F34 },
                { F40, F41, F42, F43, F44 },
            };
    }
}
