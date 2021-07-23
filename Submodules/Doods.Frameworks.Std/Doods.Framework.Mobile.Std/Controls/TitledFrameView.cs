using System.Linq;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Controls
{
    public class TitledFrameView : Frame
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title),
            typeof(string),
            typeof(TitledFrameView), propertyChanged: TitlePropertyChanged);

        public static readonly BindableProperty TitleStyleProperty = BindableProperty.Create(nameof(TitleStyle),
            typeof(Style),
            typeof(TitledFrameView),
            DefaultTitleStyle);

        public static readonly BindableProperty SubTitleProperty = BindableProperty.Create(nameof(SubTitle),
            typeof(string),
            typeof(TitledFrameView), propertyChanged: SubTitlePropertyChanged);

        public static readonly BindableProperty SubTitleStyleProperty = BindableProperty.Create(nameof(SubTitleStyle),
            typeof(Style),
            typeof(TitledFrameView),
            DefaultSubTitleStyle);

        private static readonly Style DefaultTitleStyle =
            new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter
                    {
                        Property = Label.FontAttributesProperty, Value = FontAttributes.Bold
                    },
                    new Setter
                    {
                        Property = Label.FontSizeProperty, Value = 14
                    },
                    new Setter
                    {
                        Property = Label.TextColorProperty, Value = Color.White
                    },
                    new Setter
                    {
                        Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center
                    }
                }
            };

        private static readonly Style DefaultSubTitleStyle =
            new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter
                    {
                        Property = Label.FontAttributesProperty, Value = FontAttributes.Italic
                    },
                    new Setter
                    {
                        Property = Label.FontSizeProperty, Value = 14
                    },
                    new Setter
                    {
                        Property = Label.TextColorProperty, Value = Color.Black
                    },
                    new Setter
                    {
                        Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center
                    }
                }
            };

        private readonly ControlTemplate _titleTemplate = new ControlTemplate(typeof(TitleTemplate));

        public TitledFrameView()
        {
            ControlTemplate = _titleTemplate;
            var tapGestureRecognizer = new TapGestureRecognizer();
        }

        public string Title
        {
            get => (string) GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public Style TitleStyle
        {
            get => (Style) GetValue(TitleStyleProperty);
            set => SetValue(TitleStyleProperty, value);
        }


        public string SubTitle
        {
            get => (string) GetValue(SubTitleProperty);
            set => SetValue(SubTitleProperty, value);
        }

        public Style SubTitleStyle
        {
            get => (Style) GetValue(SubTitleStyleProperty);
            set => SetValue(SubTitleStyleProperty, value);
        }

        private static void TitlePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (TitledFrameView) bindable;
            if (control.Children.First() is TitleTemplate temp) temp.TitleLabel.Text = newvalue.ToString();
        }

        private static void SubTitlePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (TitledFrameView) bindable;
            if (control.Children.First() is TitleTemplate temp) temp.SubTitleLabel.Text = newvalue.ToString();
        }
    }
}