using System.Threading;
using Doods.Framework.Std;

namespace Doods.Framework.Mobile.Std.Mvvm
{
    public class LoadingContext
    {
        public static LoadingContext FromUser = new LoadingContext(nameof(FromUser), true, true);

        public static LoadingContext OnAppearing = new LoadingContext(nameof(OnAppearing), true, true);

        public static LoadingContext RefreshVisual = new LoadingContext(nameof(RefreshVisual), false, false);

        private readonly string _name;

        private LoadingContext(string name, bool reinitializeLists, bool notifyUser, CancellationToken? token = null,
            ITimeWatcher timer = null, bool isValid = false)
        {
            _name = name;
            ReinitializeLists = reinitializeLists;
            NotifyUser = notifyUser;
            Token = token ?? CancellationToken.None;
            Timer = timer;
            IsValid = isValid;
        }

        public bool ReinitializeLists { get; }

        public bool NotifyUser { get; }

        public CancellationToken Token { get; }

        public ITimeWatcher Timer { get; }

        public bool IsValid { get; }

        public static LoadingContext Create(LoadingContext context, CancellationToken token, ITimeWatcher timer)
        {
            return new LoadingContext(context._name, context.ReinitializeLists, context.NotifyUser, token, timer, true);
        }

        public override bool Equals(object obj)
        {
            if (obj is LoadingContext context) return context._name == _name;

            return base.Equals(obj);
        }
    }
}