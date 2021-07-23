using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Mobile.Std.controls;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Std;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Mvvm
{
    public abstract class ViewModelBase : NotifyPropertyChangedBase, IViewModel
    {
        private const int _waitingDurationIsSeconds = 200;
        private int _busyCount;
        protected IColorPalette _colorPalette;
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private bool _isLoaded;
        private bool _isLoading;
        private bool _isVisible;

        private string _title;
        private ViewModelState _viewModelState;

        protected ViewModelBase(ILogger logger, ITelemetryService telemetryService)
        {
            Logger = logger;
            Telemetry = telemetryService;
            NotifyLoading = true;
            RefreshCmd = new Command(RefreshAsync);
        }

        /// <summary>
        ///     Permet de définir un temps d'attente avant le lancement du chargement
        /// </summary>
        protected int? WaitingDurationIsSecond { get; set; }

        protected ILogger Logger { get; }

        protected ITelemetryService Telemetry { get; }

        /// <summary>
        ///     Indique si la progression doit être notifié
        /// </summary>
        protected bool NotifyLoading { get; set; }

        public bool IsLoading
        {
            get => _isLoading;
            protected set => SetProperty(ref _isLoading, value);
        }

        public bool IsLoaded
        {
            get => _isLoaded;
            set => SetProperty(ref _isLoaded, value);
        }

        public bool IsBusy => BusyCount > 0;

        protected int BusyCount
        {
            get => _busyCount;
            set
            {
                SetProperty(ref _busyCount, value);
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        /// <summary>
        /// </summary>
        protected CancellationToken Token => _cts.Token;

        /// <summary>
        ///     Commande de rafraichissement des données, lance StartLoadingAsync
        /// </summary>
        public ICommand RefreshCmd { get; }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public abstract IColorPalette ColorPalette { get; }

        public virtual ICommand CmdState { get; set; }

        public ViewModelState ViewModelState
        {
            get => _viewModelState;
            set => SetProperty(ref _viewModelState, value);
        }

        /// <summary>
        ///     Permet de lancer le chargement du ViewModel
        /// </summary>
        /// <param name="context">Contexte de chargement</param>
        /// <returns></returns>
        public async Task StartLoadingAsync(LoadingContext context)
        {
            if (IsLoading) return;

            IsLoading = true;
            ViewModelState = ViewModelState.Loading;

            TimeTracer tracer = null;
            ITimeWatcher timer;

            if (context.IsValid)
            {
                timer = context.Timer;
            }
            else
            {
                tracer = new TimeTracer(Logger, Telemetry);
                timer = tracer.Timer;
            }

            using (var watch = timer.StartWatcher("StartLoading"))
            {
                watch?.Properties?.Add("type", GetType().Name);

                // Préparation du chargement
                InitializeLoading(context);

                // Permet d'éviter les freezes de l'UI en navigation
                var duration = WaitingDurationIsSecond ?? _waitingDurationIsSeconds;
                if (duration > 0)
                    await Task.Delay(TimeSpan.FromMilliseconds(duration));

                try
                {
                    // On poursuit uniquement si le token n'est pas déjà annulé 
                    // par un changement de page par exemple
                    if (!Token.IsCancellationRequested)
                        await ExecuteAsync(token =>
                        {
                            var ctx = context.IsValid ? context : LoadingContext.Create(context, token, timer);
                            return LoadAsync(ctx);
                        });

                    ViewModelState = ViewModelState.Loaded;
                }
                catch (Exception e)
                {
                    ViewModelState = ViewModelState.Failed;
                    Logger.Error(e);
                    Telemetry.Exception(e);
                }
                finally
                {
                    // Finalisation du chargement
                    FinishLoading(context);
                }
            }

            tracer?.Dispose();

            IsLoading = false;
        }

        private void InitializeLoading(LoadingContext context)
        {
            IsLoaded = false;

            var notify = context.NotifyUser || NotifyLoading;
            if (notify)
                BusyCount++;

            OnInitializeLoading(context);
        }

        private void FinishLoading(LoadingContext context)
        {
            var notify = context.NotifyUser || NotifyLoading;
            if (notify) BusyCount--;

            if (!Token.IsCancellationRequested)
            {
                OnFinishLoading(context);
                IsLoaded = true;
            }
        }

        protected virtual Task LoadAsync(LoadingContext context)
        {
            return Task.FromResult(0);
        }

        protected virtual void OnFinishLoading(LoadingContext context)
        {
            //NP
        }

        protected virtual void OnInitializeLoading(LoadingContext context)
        {
            //NP
        }

        public async Task OnAppearingAsync()
        {
            if (_cts.IsCancellationRequested) _cts = new CancellationTokenSource();

            if (!IsLoaded && !IsLoading && ViewModelState != ViewModelState.Loading)
                await StartLoadingAsync(LoadingContext.OnAppearing);

            IsVisible = true;
            await OnInternalAppearingAsync();
        }

        protected virtual Task OnInternalAppearingAsync()
        {
            return Task.FromResult(0);
        }

        public Task OnDisappearingAsync()
        {
            IsVisible = false;
            CancelExecutions();
            return OnInternalDisappearingAsync();
        }

        protected virtual Task OnInternalDisappearingAsync()
        {
            return Task.FromResult(0);
        }

        protected async Task<T> ExecuteAsync<T>(Func<CancellationToken, Task<T>> action, bool safeExecution = false)
        {
            try
            {
                Token.ThrowIfCancellationRequested();
                return await action(Token);
            }
            catch (AggregateException e)
            {
                var innerException = e.InnerException;
                while (innerException.InnerException != null)
                    innerException = innerException.InnerException;

                Logger.Error(innerException);
                Telemetry.Exception(e);
                //TODO : HokeyApp
                if (!safeExecution) throw;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                Telemetry.Exception(e);
                //TODO : HokeyApp
                if (!safeExecution) throw;
            }

            return default;
        }

        protected async Task ExecuteAsync(Func<CancellationToken, Task> action, bool safeExecution = false)
        {
            try
            {
                Token.ThrowIfCancellationRequested();
                await action(Token);
            }
            catch (AggregateException e)
            {
                var innerException = e.InnerException;
                while (innerException.InnerException != null)
                    innerException = innerException.InnerException;

                //TODO : LOG + HokeyApp
                if (!safeExecution) throw;
            }
            catch (Exception e)
            {
                //TODO : LOG + HokeyApp
                var msg = e.Message;
                if (!safeExecution) throw;
            }
        }

        public void CancelExecutions()
        {
            if (Token.CanBeCanceled && !_cts.IsCancellationRequested)
                _cts.Cancel();

            // TODO : Recréer un nouveau _cts ?
        }

        /// <summary>
        ///     Obtient la description des éléments à placer dans la toolbar
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<CommandItem> GetToolBarItemDescriptions()
        {
            return null;
        }

        /// <summary>
        ///     Indique si la toolbar peut etre actualisé, fonction de la sélection
        /// </summary>
        /// <returns></returns>
        public virtual bool CanUpdateToolBar()
        {
            return true;
        }

        private async void RefreshAsync()
        {
            if (IsBusy) return;
            await StartLoadingAsync(LoadingContext.FromUser);
        }
    }
}