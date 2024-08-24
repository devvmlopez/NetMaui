namespace InntecMobileNetMaui.Views.CustomView;

public partial class CustomBackMenu : ContentView
{
	public CustomBackMenu()
	{
		InitializeComponent();
	}
    public static readonly BindableProperty MenuTextoProperty = BindableProperty.Create("MenuText",
                                                                                      typeof(string),
                                                                                      typeof(ContentView));

    public string MenuText
    {
        get { return GetValue(MenuTextoProperty) as string; }
        set { SetValue(MenuTextoProperty, value); }
    }

    public static readonly BindableProperty IconIzqProperty = BindableProperty.Create("IconIzq",
                                                                                       typeof(ImageSource),
                                                                                       typeof(ContentView));
    public ImageSource IconIzq
    {
        get { return GetValue(IconIzqProperty) as ImageSource; }
        set { SetValue(IconIzqProperty, value); }
    }
    private async void TapIconoIzq_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//CardMenu");
    }
}