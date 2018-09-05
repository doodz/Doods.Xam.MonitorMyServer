using CoreAnimation;
using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace Doods.Xam.MonitorMyServer.iOS.Helpers
{
    public class MNFloatingActionButton : UIControl
    {
        /// <summary>
        /// FAB Size options.
        /// </summary>
        public enum FabSize
        {
            Mini,
            Normal
        }

        /// <summary>
        /// Flags for hiding/showing shadow
        /// </summary>
        public enum ShadowState
        {
            ShadowStateShown,
            ShadowStateHidden
        }

        private readonly nfloat _animationDuration;
        private readonly nfloat _animationScale;
        private readonly nfloat _shadowOpacity;
        private readonly nfloat _shadowRadius;

        UIColor _backgroundColor;

        UIImageView _centerImageView;

        bool _hasShadow;

        UIColor _shadowColor;

        private FabSize _size = FabSize.Normal;

        public MNFloatingActionButton(bool animateOnSelection)
            : base()
        {
            this._animationDuration = 0.05f;
            this._animationScale = 0.85f;
            this._shadowOpacity = 0.6f;
            this._shadowRadius = 1.5f;
            this.AnimateOnSelection = animateOnSelection;

            this.CommonInit();
        }

        public MNFloatingActionButton(CGRect frame, bool animateOnSelection)
            : base(frame)
        {
            this._animationDuration = 0.05f;
            this._animationScale = 0.85f;
            this._shadowOpacity = 0.6f;
            this._shadowRadius = 1.5f;
            this.AnimateOnSelection = animateOnSelection;

            this.CommonInit();
        }

        /// <summary>
        /// Size to render the FAB -- Normal or <ini
        /// </summary>
        /// <value>The size.</value>
        public FabSize Size
        {
            get => _size;
            set
            {
                if (_size == value)
                    return;

                _size = value;
                UpdateBackground();
            }
        }

        /// <summary>
        /// The image to display int the center of the button
        /// </summary>
        /// <value>The center image view.</value>
        public UIImageView CenterImageView
        {
            get
            {
                if (_centerImageView == null)
                {
                    _centerImageView = new UIImageView();
                }

                return _centerImageView;
            }
            private set => _centerImageView = value;
        }

        /// <summary>
        /// Background Color of the FAB
        /// </summary>
        /// <value>The color of the background.</value>
        public new UIColor BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;

                UpdateBackground();
            }
        }

        public UIColor ShadowColor
        {
            get => _shadowColor;
            set
            {
                _shadowColor = value;
                UpdateBackground();
            }
        }

        public bool HasShadow
        {
            get => _hasShadow;
            set
            {
                _hasShadow = value;
                UpdateBackground();
            }
        }

        public nfloat ShadowOpacity { get; private set; }

        public nfloat ShadowRadius { get; private set; }

        public nfloat AnimationScale { get; private set; }

        public nfloat AnimationDuration { get; private set; }

        public bool IsAnimating { get; private set; }

        public UIView BackgroundCircle { get; private set; }

        public bool AnimateOnSelection { get; set; }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            this.CenterImageView.Center = this.BackgroundCircle.Center;
            if (!this.IsAnimating)
            {
                this.UpdateBackground();
            }
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            this.AnimateToSelectedState();
            this.SendActionForControlEvents(UIControlEvent.TouchDown);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            this.AnimateToDeselectedState();
            this.SendActionForControlEvents(UIControlEvent.TouchUpInside);
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            this.AnimateToDeselectedState();
            this.SendActionForControlEvents(UIControlEvent.TouchCancel);
        }

        private void CommonInit()
        {
            this.BackgroundCircle = new UIView();

            this.BackgroundColor = UIColor.Red.ColorWithAlpha(0.4f);
            this.BackgroundColor = new UIColor(33.0f / 255.0f, 150.0f / 255.0f, 243.0f / 255.0f, 1.0f);
            this.BackgroundCircle.BackgroundColor = this.BackgroundColor;
            this.ShadowOpacity = _shadowOpacity;
            this.ShadowRadius = _shadowRadius;
            this.AnimationScale = _animationScale;
            this.AnimationDuration = _animationDuration;

            this.BackgroundCircle.AddSubview(this.CenterImageView);
            this.AddSubview(this.BackgroundCircle);
        }

        private void AnimateToSelectedState()
        {
            if (this.AnimateOnSelection)
            {
                this.IsAnimating = true;
                this.ToggleShadowAnimationToState(ShadowState.ShadowStateHidden);
                UIView.Animate(_animationDuration, () =>
                {
                    this.BackgroundCircle.Transform = CGAffineTransform.MakeScale(this.AnimationScale, this.AnimationScale);
                }, () =>
                {
                    this.IsAnimating = false;
                });
            }
        }

        private void AnimateToDeselectedState()
        {
            if (this.AnimateOnSelection)
            {
                this.IsAnimating = true;
                this.ToggleShadowAnimationToState(ShadowState.ShadowStateShown);
                UIView.Animate(_animationDuration, () =>
                {
                    this.BackgroundCircle.Transform = CGAffineTransform.MakeScale(1.0f, 1.0f);
                }, () =>
                {
                    this.IsAnimating = false;
                });
            }
        }

        private void ToggleShadowAnimationToState(ShadowState state)
        {
            nfloat endOpacity = 0.0f;
            if (state == ShadowState.ShadowStateShown)
            {
                endOpacity = this.ShadowOpacity;
            }

            var animation = CABasicAnimation.FromKeyPath("shadowOpacity");
            animation.From = NSNumber.FromFloat((float)this.ShadowOpacity);
            animation.To = NSNumber.FromFloat((float)endOpacity);
            animation.Duration = _animationDuration;
            this.BackgroundCircle.Layer.AddAnimation(animation, "shadowOpacity");
            this.BackgroundCircle.Layer.ShadowOpacity = (float)endOpacity;
        }

        private void UpdateBackground()
        {
            this.BackgroundCircle.Frame = this.Bounds;
            this.BackgroundCircle.Layer.CornerRadius = this.Bounds.Size.Height / 2;
            this.BackgroundCircle.Layer.ShadowColor = this.ShadowColor != null ? this.ShadowColor.CGColor : this.BackgroundColor.CGColor;
            this.BackgroundCircle.Layer.ShadowOpacity = (float)this.ShadowOpacity;
            this.BackgroundCircle.Layer.ShadowRadius = this.ShadowRadius;
            this.BackgroundCircle.Layer.ShadowOffset = new CGSize(1.0, 1.0);
            this.BackgroundCircle.BackgroundColor = this.BackgroundColor;

            var xPos = this.BackgroundCircle.Bounds.Width / 2 - 12;
            var yPos = this.BackgroundCircle.Bounds.Height / 2 - 12;

            this.CenterImageView.Frame = new CGRect(xPos, yPos, 24, 24);
        }
    }
}