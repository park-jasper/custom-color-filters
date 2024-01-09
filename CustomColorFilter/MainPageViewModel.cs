using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CustomColorFilter.Contracts;
using CustomColorFilter.Model;

namespace CustomColorFilter;

public partial class MainPageViewModel : ObservableObject
{
    private IColorFilter colorFilter;

    public MainPageViewModel(IColorFilter colorFilter)
    {
        this.colorFilter = colorFilter;

        this.AvailableFilters = [
            new("Identity", BuiltinMatrices.Identity, isBuiltin: true),
            new("Protanopia", BuiltinMatrices.Protanopia, isBuiltin: true),
            new("Protanomaly", BuiltinMatrices.Protanomaly, isBuiltin: true),
            new("Deuteranomaly", BuiltinMatrices.Deuteranomaly, isBuiltin: true),
            new("Deuteranopia", BuiltinMatrices.Deuteranopia, isBuiltin: true),
            new("Tritanopia", BuiltinMatrices.Tritanopia, isBuiltin: true),
            new("Tritanomaly", BuiltinMatrices.Tritanomaly, isBuiltin: true),
            new("Negative", BuiltinMatrices.Negative, isBuiltin: true),
            new("GrayScale", BuiltinMatrices.GrayScale, isBuiltin: true),
            new("Sepia", BuiltinMatrices.Sepia, isBuiltin: true),
            new("Red", BuiltinMatrices.Red, isBuiltin: true),
            new("HueShift180", BuiltinMatrices.HueShift180, isBuiltin: true),
            new("NegativeGrayScale", BuiltinMatrices.NegativeGrayScale, isBuiltin: true),
            new("NegativeSepia", BuiltinMatrices.NegativeSepia, isBuiltin: true),
            new("NegativeRed", BuiltinMatrices.NegativeRed, isBuiltin: true),
            new("NegativeHueShift180", BuiltinMatrices.NegativeHueShift180, isBuiltin: true),
            new("NegativeHueShift180Variation1", BuiltinMatrices.NegativeHueShift180Variation1, isBuiltin: true),
            new("NegativeHueShift180Variation2", BuiltinMatrices.NegativeHueShift180Variation2, isBuiltin: true),
            new("NegativeHueShift180Variation3", BuiltinMatrices.NegativeHueShift180Variation3, isBuiltin: true),
            new("NegativeHueShift180Variation4", BuiltinMatrices.NegativeHueShift180Variation4, isBuiltin: true),
        ];
        var noRed = new float[,] {
                {  0.0f,  0.3f,  0.2f,  0.0f,  0.0f },
                {  0.0f,  1.0f,  0.0f,  0.0f,  0.0f },
                {  0.0f,  0.0f,  1.0f,  0.0f,  0.0f },
                {  0.0f,  0.0f,  0.0f,  1.0f,  0.0f },
                {  0.0f,  0.0f,  0.0f,  0.0f,  1.0f }
            };
        this.AvailableFilters.Add(new("NoRed", noRed, isBuiltin: false));
    }

    [ObservableProperty] private FilterViewModel selectedFilter;
    [ObservableProperty] private bool isFilterSet;

    public ObservableCollection<FilterViewModel> AvailableFilters { get;set; } = new();

    [RelayCommand]
    public void OnApplyFilter()
    {
        this.colorFilter.SetFullScreenColorFilter(this.SelectedFilter.BuildMatrix());
        this.IsFilterSet = true;
        this.DisableFilterCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    public async Task SaveValues()
    {

    }

    [RelayCommand(CanExecute = nameof(IsFilterSet))]
    public void DisableFilter()
    {
        this.colorFilter.SetFullScreenColorFilter(BuiltinMatrices.Identity);
        this.IsFilterSet = false;
        this.DisableFilterCommand.NotifyCanExecuteChanged();
    }
}

