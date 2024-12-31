using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Application.Contracts;
using Domain.BusinessLogic;
using Domain.Model;
using Presentation.ViewModels;

namespace CustomColorFilter;

public partial class MainPageViewModel : ObservableObject
{
    private readonly IColorFilter colorFilter;
    private readonly IFileService fileService;
    private readonly IStartupService startupService;

    public MainPageViewModel(
        IColorFilter colorFilter,
        IFileService fileService,
        IStartupService startupService)
    {
        this.colorFilter = colorFilter;
        this.fileService = fileService;
        this.startupService = startupService;
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
        var config = this.fileService.GetConfig();
        foreach (var customFilter in config.CustomFilters)
        {
            this.AvailableFilters.Add(new FilterViewModel(customFilter.Name, MatrixHelpers.ToMatrix(customFilter.Matrix, 5), isBuiltin: false));
        }
        this.ApplyDefaultFilterOnStart = config.ApplyDefaultFilterOnStart;

        this.PropertyChanged += MainPageViewModel_PropertyChanged;
        
        foreach (var filter in this.AvailableFilters)
        {
            if (filter.Name == config.SelectedFilter)
            {
                this.SelectedFilter = filter;
                break;
            }
        }

        this.InitializeStartupService();
    }

    private async void InitializeStartupService()
    {
        await this.startupService.InitializeAsync();
        this.LaunchOnStartup = this.startupService.IsStartupEnabled();
    }

    public void OnAppearing()
    {
        if (this.ApplyDefaultFilterOnStart && this.SelectedFilter is not null)
        {
            this.colorFilter.SetFullScreenColorFilter(this.SelectedFilter.BuildMatrix());
            this.IsFilterSet = true;
            this.DisableFilterCommand.NotifyCanExecuteChanged();
        }
    }

    private async void MainPageViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch(e.PropertyName)
        {
            case nameof(this.SelectedFilter):
                this.IsCustomFilter = !this.SelectedFilter.IsBuiltin;
                this.SelectedFilterName = this.SelectedFilter.Name;
                this.DeleteCustomFilterCommand.NotifyCanExecuteChanged();
                break;
            case nameof(this.SelectedFilterName):
                if (this.SelectedFilter is not null)
                {
                    this.SelectedFilter.Name = this.SelectedFilterName;
                }
                break;
            case nameof(this.LaunchOnStartup):
                var isEnabled = await this.startupService.SetIsStartupEnabledAsync(this.LaunchOnStartup);
                this.LaunchOnStartup = isEnabled;
                break;
        }
    }

    [ObservableProperty] private int selectedIndex;
    [ObservableProperty] private FilterViewModel selectedFilter;
    [ObservableProperty] private string selectedFilterName;
    [ObservableProperty] private bool isFilterSet;
    [ObservableProperty] private bool isCustomFilter;
    [ObservableProperty] private bool applyDefaultFilterOnStart;
    [ObservableProperty] private bool launchOnStartup;

    public ObservableCollection<FilterViewModel> AvailableFilters { get;set; } = new();

    [RelayCommand]
    public void OnAddCustomFilter()
    {
        this.AvailableFilters.Add(new("NewFilter", BuiltinMatrices.Identity, isBuiltin: false));
    }

    [RelayCommand(CanExecute = nameof(IsCustomFilter))]
    public void OnDeleteCustomFilter()
    {
        this.AvailableFilters.RemoveAt(this.SelectedIndex);
    }

    [RelayCommand]
    public void OnApplyFilter()
    {
        this.colorFilter.SetFullScreenColorFilter(this.SelectedFilter.BuildMatrix());
        this.IsFilterSet = true;
        this.DisableFilterCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(IsFilterSet))]
    public void DisableFilter()
    {
        this.colorFilter.SetFullScreenColorFilter(BuiltinMatrices.Identity);
        this.IsFilterSet = false;
        this.DisableFilterCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(IsCustomFilter))]
    public async Task SaveValues()
    {
        var customFilters = this.AvailableFilters
            .Where(f => !f.IsBuiltin)
            .Select(vm => new Filter(vm.Name, MatrixHelpers.ToFlatArray(vm.BuildMatrix())))
            .ToList();
        var config = new Configuration(this.SelectedFilterName, this.ApplyDefaultFilterOnStart, customFilters);
        this.fileService.SaveConfig(config);
    }
}

