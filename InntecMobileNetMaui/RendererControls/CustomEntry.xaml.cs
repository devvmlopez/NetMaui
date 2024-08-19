using Microsoft.Maui.Controls.Shapes;
using System.ComponentModel;

namespace InntecMobileNetMaui.RendererControls;

public partial class CustomEntry : ICustomEntry
{
    #region TEXTPROPERTY
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
                                                                                typeof(string),
                                                                                typeof(CustomEntry),
                                                                                string.Empty,
                                                                                BindingMode.TwoWay,
                                                                                propertyChanged: TextChanged);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    private static void TextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((ICustomEntry)bindable).Text((string)newValue);
    }
    void ICustomEntry.Text(string value)
    {
        Text = value;
    }
    #endregion

    #region  ISPASSWORD PROPERTY
    public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword),
                                                                                         typeof(bool),
                                                                                         typeof(CustomEntry),
                                                                                         false,
                                                                                         BindingMode.OneWay,
                                                                                         propertyChanged: IsPasswordChanged);

    private static void IsPasswordChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((ICustomEntry)bindable).IsPassword((bool)newValue);
    }

    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }

    void ICustomEntry.IsPassword(bool value)
    {
        MeEntry.IsPassword = value;
    }

    #endregion

    #region PLACE HOLDER PROPERTY
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder),
                                                                                        typeof(string),
                                                                                        typeof(CustomEntry),
                                                                                        string.Empty,
                                                                                        BindingMode.OneWay,
                                                                                        propertyChanged: PlaceholderChanged);

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    private static void PlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((ICustomEntry)bindable).Placeholder((string)newValue);

    }
    void ICustomEntry.Placeholder(string value)
    {
        MeLabel.Text = value;
    }

    #endregion

    #region ICONO

    public static readonly BindableProperty IcoDataProperty = BindableProperty.Create(nameof(IcoData),
                                                                                      typeof(Microsoft.Maui.Controls.Shapes.Geometry),
                                                                                      typeof(CustomEntry),
                                                                                      null,
                                                                                      BindingMode.OneWay,
                                                                                      propertyChanged: IcoDataChanged);
    [TypeConverter(typeof(PathGeometryConverter))]
    public Microsoft.Maui.Controls.Shapes.Geometry IcoData
    {
        get => (Microsoft.Maui.Controls.Shapes.Geometry)GetValue(IcoDataProperty);
        set => SetValue(IcoDataProperty, value);
    }
    private static void IcoDataChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((ICustomEntry)bindable).SetIcoData((Microsoft.Maui.Controls.Shapes.Geometry)newValue);
    }
    void ICustomEntry.SetIcoData(Microsoft.Maui.Controls.Shapes.Geometry value)
    {
        MeIcon.Data = value;
    }

    #endregion

    #region MASK PROPERTY

    public static readonly BindableProperty MaskProperty = BindableProperty.Create(nameof(Mask),
                                                                                      typeof(string),
                                                                                      typeof(CustomEntry),
                                                                                      null,
                                                                                      BindingMode.OneWay,
                                                                                      propertyChanged: MaskChanged);

    public string Mask
    {
        get => (string)GetValue(MaskProperty);
        set => SetValue(MaskProperty, value);
    }
    private static void MaskChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((ICustomEntry)bindable).SetMask((string)newValue);
    }
    void ICustomEntry.SetMask(string value)
    {
        if (MeEntry.Behaviors.Count > 0)
        {
            MeEntry.Behaviors.RemoveAt(0);
        }

        if (!string.IsNullOrEmpty(value))
        {
            MaskedBehavior Mask = new MaskedBehavior();
            Mask.Mask = value;
            MeEntry.Behaviors.Add(Mask);
            MeEntry.MaxLength = value.Length;
        }
    }



    #endregion

    #region KEYBOARD PROPERTY

    public static readonly BindableProperty KeyBoardProperty = BindableProperty.Create(nameof(KeyBoard),
                                                                                      typeof(Keyboard),
                                                                                      typeof(CustomEntry),
                                                                                      Keyboard.Default,
                                                                                      BindingMode.OneWay,
                                                                                      propertyChanged: KeyBoardChanged);

    public Keyboard KeyBoard
    {
        get => (Keyboard)GetValue(KeyBoardProperty);
        set => SetValue(KeyBoardProperty, value);
    }
    private static void KeyBoardChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((ICustomEntry)bindable).SetKeyboard((Keyboard)newValue);
    }
    void ICustomEntry.SetKeyboard(Keyboard value)
    {
        MeEntry.Keyboard = value;
    }


    #endregion

    public CustomEntry()
    {
        InitializeComponent();
        this.MeEntry.TextChanged += new EventHandler<TextChangedEventArgs>(MeEntry_TextChanged);
        this.MeEntry.Focused += new EventHandler<FocusEventArgs>(MeEntry_Focused);
        this.MeEntry.Unfocused += new EventHandler<FocusEventArgs>(MeEntry_Unfocused);

    }

    private void MeEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Text = e.NewTextValue;
    }

    void MeEntry_Focused(System.Object sender, Microsoft.Maui.Controls.FocusEventArgs e)
    {
        ScaleLabelDown();
    }

    void MeEntry_Unfocused(System.Object sender, Microsoft.Maui.Controls.FocusEventArgs e)
    {
        if (string.IsNullOrEmpty(MeEntry.Text))
        {
            ScaleLabelUp();
        }
    }

    private void ScaleLabelDown()
    {
        MeLabel.ScaleTo(0.8, 100, Easing.Linear);
        MeLabel.TranslateTo(0, -20, 100, Easing.Linear);
    }

    private void ScaleLabelUp()
    {
        MeLabel.ScaleTo(1, 100, Easing.Linear);
        MeLabel.TranslateTo(0, 0, 100, Easing.Linear);
    }


}
internal interface ICustomEntry
{
    void Text(string value);
    void Placeholder(string value);
    void SetIcoData(Microsoft.Maui.Controls.Shapes.Geometry value);
    void IsPassword(bool value);
    void SetMask(string value);
    void SetKeyboard(Keyboard value);
}