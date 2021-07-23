using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Switch = Xamarin.Forms.Switch;

namespace Doods.Framework.Mobile.Std.Behaviors
{
    public class ViewTappedButtonBehavior : Behavior<View>
    {
        public static readonly BindableProperty AnimationTypeProperty =
            BindableProperty.Create(nameof(AnimationType), typeof(AnimationType), typeof(ViewTappedButtonBehavior),
                AnimationType.Fade);

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ViewTappedButtonBehavior));

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ViewTappedButtonBehavior));

        private bool _isAnimating;

        public AnimationType AnimationType
        {
            get => (AnimationType) GetValue(AnimationTypeProperty);
            set => SetValue(AnimationTypeProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public View AssociatedObject { get; private set; }

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;

            if (bindable is Button myButton)
            {
                myButton.Clicked += View_Tapped;
            }
            else if (bindable is Switch mySwitch)
            {
                mySwitch.Toggled += View_Tapped;
            }
            else
            {
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += View_Tapped;
                bindable.GestureRecognizers.Add(tapGestureRecognizer);
            }
        }

        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            AssociatedObject = null;

            if (bindable.GestureRecognizers.FirstOrDefault() is TapGestureRecognizer exists)
                exists.Tapped -= View_Tapped;
        }

        private void View_Tapped(object sender, EventArgs e)
        {
            if (_isAnimating)
                return;

            _isAnimating = true;

            var view = (View) sender;

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    if (AnimationType == AnimationType.Fade)
                    {
                        await view.FadeTo(0.3, 300);
                        await view.FadeTo(1, 300);
                    }
                    else if (AnimationType == AnimationType.Scale)
                    {
                        await view.ScaleTo(1.2, 170, Easing.Linear);
                        await view.ScaleTo(1, 170, Easing.Linear);
                    }
                    else if (AnimationType == AnimationType.Rotate)
                    {
                        await view.RotateTo(360, 200, Easing.Linear);
                        view.Rotation = 0;
                    }
                    else if (AnimationType == AnimationType.FlipHorizontal)
                    {
                        // Perform half of the flip
                        await view.RotateYTo(90, 200);
                        await view.RotateYTo(0, 200);
                    }
                    else if (AnimationType == AnimationType.FlipVertical)
                    {
                        // Perform half of the flip
                        await view.RotateXTo(90, 200);
                        await view.RotateXTo(0, 200);
                    }
                    else if (AnimationType == AnimationType.Shake)
                    {
                        await view.TranslateTo(-15, 0, 50);
                        await view.TranslateTo(15, 0, 50);
                        await view.TranslateTo(-10, 0, 50);
                        await view.TranslateTo(10, 0, 50);
                        await view.TranslateTo(-5, 0, 50);
                        await view.TranslateTo(5, 0, 50);
                        view.TranslationX = 0;
                    }
                }
                finally
                {
                    if (Command != null)
                        if (Command.CanExecute(CommandParameter))
                            Command.Execute(CommandParameter);

                    Debug.WriteLine(CommandParameter);

                    _isAnimating = false;
                }
            });
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }
}