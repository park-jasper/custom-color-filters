namespace CustomColorFilter;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel vm;
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = this.vm = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.vm.OnAppearing();
    }
}
