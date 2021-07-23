using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.Framework.Mobile.Std.controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BooleanView : ContentView
    {
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value),
                typeof(bool), typeof(BooleanView), false, propertyChanged: ValuePropertyChanged);


        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(BooleanView));

        public static readonly BindableProperty DescriptionProperty =
            BindableProperty.Create(nameof(Description), typeof(string), typeof(BooleanView));


        public static readonly BindableProperty SubtitleStyleProperty = BindableProperty.Create(
            nameof(SubtitleStyleProperty),
            typeof(Style), typeof(BooleanView));


        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor),
                typeof(Color), typeof(BooleanView), Color.Blue,
                propertyChanged: TextColorPropertyChanged);

        public BooleanView()
        {
            InitializeComponent();

            MySwitch.BindingContext = this;
            MySwitch.SetBinding(Entry.TextProperty,
                nameof(Value), BindingMode.TwoWay);
        }

        public bool Value
        {
            get => (bool) GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string Description
        {
            get => (string) GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }


        public Style SubtitleStyle
        {
            get => (Style) GetValue(SubtitleStyleProperty);
            set => SetValue(SubtitleStyleProperty, value);
        }


        public Color TextColor
        {
            get => (Color) GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        private static void ValuePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
        }


        private static void TextColorPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            //if (newvalue == oldvalue)
            //    return;
            //var val = (Color)newvalue;
            //((BooleanViewCell)bindable)TextColor = val;
            //var oldVal = (Color)bindable.GetValue(ClearableEntryViewValidator.NormalColorProperty);
            //if (oldVal == val)
            //    return;

            //bindable.SetValue(ClearableEntryViewValidator.NormalColorProperty, val);
        }
    }
}