
using Microsoft.Maui.Controls.Shapes;
namespace InntecMobileNetMaui.Views.CustomView;
public partial class CustomMenu : ContentView
{
	public CustomMenu()
	{
		InitializeComponent();
	}
	public static readonly BindableProperty IconIzqProperty = BindableProperty.Create("IconIzq" , 
																					   typeof(ImageSource), 
																					   typeof(ContentView));

	public ImageSource IconIzq 
	{
		get { return GetValue(IconIzqProperty) as ImageSource;}
		set { SetValue(IconIzqProperty , value); }	
    }
    public static readonly BindableProperty MenuTextoProperty = BindableProperty.Create("IconIzqEnable",
                                                                                      typeof(string),
                                                                                      typeof(ContentView));

    public string IconIzqEnable
    {
        get { return GetValue(MenuTextoProperty) as string; }
        set { SetValue(MenuTextoProperty, value); }
    }
    private async void TapIconoIzq_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//CardDetailPage");
    }

    
    public static readonly BindableProperty IconCentroIzqProperty = BindableProperty.Create("IconCentroIzq",
                                                                                     typeof(ImageSource),
                                                                                     typeof(ContentView));

    public ImageSource IconCentroIzq
    {
        get { return GetValue(IconCentroIzqProperty) as ImageSource; }
        set { SetValue(IconCentroIzqProperty, value); }
    }
    private async void TapIconoCentroIzq_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//CardPageList");
    }

    public static readonly BindableProperty IconCentroIzqEnableProperty = BindableProperty.Create("IconCentroIzqEnable",
                                                                                     typeof(string),
                                                                                     typeof(ContentView));

    public string IconCentroIzqEnable
    {
        get { return GetValue(IconCentroIzqEnableProperty) as string; }
        set { SetValue(IconCentroIzqEnableProperty, value); }
    }

    public static readonly BindableProperty IconCentroDerProperty = BindableProperty.Create("IconCentroDer",
                                                                                       typeof(ImageSource),
                                                                                       typeof(ContentView));

    public ImageSource IconCentroDer
    {
        get { return GetValue(IconCentroDerProperty) as ImageSource; }
        set { SetValue(IconCentroDerProperty, value); }
    }
    private async void TapIconoCentroDer_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//CardPageList");
    }

    public static readonly BindableProperty IconCentroDerEnableProperty = BindableProperty.Create("IconCentroDerEnable",
                                                                                  typeof(string),
                                                                                  typeof(ContentView));

    public string IconCentroDerEnable
    {
        get { return GetValue(IconCentroDerEnableProperty) as string; }
        set { SetValue(IconCentroDerEnableProperty, value); }
    }


    public static readonly BindableProperty IconDerProperty = BindableProperty.Create("IconDer",
                                                                                       typeof(ImageSource),
                                                                                       typeof(ContentView));

    public ImageSource IconDer
    {
        get { return GetValue(IconDerProperty) as ImageSource; }
        set { SetValue(IconDerProperty, value); }
    }

    private async void TapIconoDer_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//MyDatePage");
    }

    public static readonly BindableProperty IconDerEnableProperty = BindableProperty.Create("IconDerEnable",
                                                                                     typeof(string),
                                                                                     typeof(ContentView));

    public string IconDerEnable
    {
        get { return GetValue(IconDerEnableProperty) as string; }
        set { SetValue(IconDerEnableProperty, value); }
    }

    public static readonly BindableProperty IconCentroProperty = BindableProperty.Create("IconCentro",
                                                                                      typeof(ImageSource),
                                                                                      typeof(ContentView));
    public ImageSource IconCentro
    {
        get { return GetValue(IconCentroProperty) as ImageSource; }
        set { SetValue(IconCentroProperty, value); }
    }

    private async void TapIconoCentro_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//CardMenu");
    }
    public static readonly BindableProperty IconCentroEnableProperty = BindableProperty.Create("IconCentroEnable",
                                                                                     typeof(string),
                                                                                     typeof(ContentView));

    public string IconCentroEnable
    {
        get { return GetValue(IconCentroEnableProperty) as string; }
        set { SetValue(IconCentroEnableProperty, value); }
    }
}