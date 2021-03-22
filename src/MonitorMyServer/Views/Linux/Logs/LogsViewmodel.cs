using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Doods.Framework.Std.Lists;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Xamarin.Forms.Internals;

namespace Doods.Xam.MonitorMyServer.Views.Linux.Logs
{
    public class LogLineModel
    {
        private const string Regexp = "(\\S+\\s+\\d+\\s+\\S+)\\s+(\\S+)\\s+(.*)";

        //Jan 19 11:14:43
        private const string Pattern = "MMM dd HH:mm:ss";

        private static readonly Regex Rx = new Regex(Regexp,
            RegexOptions.Compiled | RegexOptions.IgnoreCase, TimeSpan.FromSeconds(5));

        private string _logLine;


        public LogLineModel(string logline, long rownum)
        {
            _logLine = logline;
            Rownum = rownum;


            var m = Rx.Match(logline);

            if (m.Success)
            {
                //Date = logline.Date;
                Hostname = m.Groups[2].Value;
                Message = m.Groups[3].Value;

                DateTime dateValue;
                if (DateTime.TryParseExact(m.Groups[1].Value, Pattern, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out dateValue))
                {
                    Date = dateValue;
                    Ts = Date.Ticks;
                }
            }
            else
            {
                Message = logline;
            }

            var tmp = Message.Split(':');
            Title = tmp[0];
            Message = Message.Replace(Title + ':', string.Empty);
        }

        public bool IsNotLast { get; set; } = true;
        public bool IsNotIn { get; set; } = true;
        public long Rownum { get; }

        public long Ts { get; }

        public DateTime Date { get; }

        public string Title { get; }

        public string Hour => Date.ToString("T");

        public string Hostname { get; }

        public string Message { get; }
    }

    //cat /proc/mdstat
    // mdadm --detail /dev/md0
    public class LogsViewmodel : ViewModelWhithState
    {
        private readonly ISshService _sshService;
        private bool _canSudo;

        private IEnumerable<string> _groups;
        private bool _isAdm;
        private IEnumerable<string> _logs;

        private IEnumerable<string> _logsFiles;

        private string _selectedLogFile;

        public LogsViewmodel(ISshService sshService)
        {
            _sshService = sshService;
        }

        public IEnumerable<string> Logs
        {
            get => _logs;
            set => SetProperty(ref _logs, value);
        }

        public IEnumerable<string> LogsFiles
        {
            get => _logsFiles;
            set => SetProperty(ref _logsFiles, value);
        }

        public ObservableRangeCollection<LogLineModel> LogsLines { get; } = new ObservableRangeCollection<LogLineModel>();

        public string SelectedLogFile
        {
            get => _selectedLogFile;
            set => SetProperty(ref _selectedLogFile, value, () => OnChanged(), null);
        }

        public IEnumerable<string> Groups
        {
            get => _groups;
            set => SetProperty(ref _groups, value);
        }

        public bool IsAdm
        {
            get => _isAdm;
            set => SetProperty(ref _isAdm, value);
        }

        public bool CanSudo
        {
            get => _canSudo;
            set => SetProperty(ref _canSudo, value);
        }

        private async Task OnChanged()
        {
            if (SelectedLogFile != null)
                try
                {
                    var Lines = await _sshService.ReadFileRequest(SelectedLogFile, 40, CanSudo);
                    long i = 1;
                    var toto = Lines.Select(l => new LogLineModel(l, i++)).ToList();
                    var gp = toto.GroupBy(t => t.Hour);
                    foreach (var g in gp) g.Skip(1).ForEach(l => l.IsNotIn = false);

                    var lst = toto.Last();
                    lst.IsNotLast = false;
                    lst.IsNotIn = true;
                    LogsLines.ReplaceRange(toto);

                    //TODO split date and other stuff.
                }
                catch (Exception e)
                {
                    SetLabelsStateItem("Error", e.Message);
                }
        }

        protected override async Task OnInternalAppearingAsync()
        {
            try
            {
                await ViewModelStateItem.RunActionAsync(async () => { await RefreshData(); },
                    () => SetLabelsStateItem(openmediavault.SystemLogs, openmediavault.Info),
                    () =>
                    {
                        if (IsAdm || CanSudo)
                            SetLabelsStateItem(openmediavault.SystemLogs, openmediavault.Done___);
                        else
                            SetLabelsStateItem(openmediavault.SystemLogs, "You need to be root, adm or sudo");
                    });
            }
            catch (Exception e)
            {
                SetLabelsStateItem("Error", e.Message);
            }

            await base.OnInternalAppearingAsync();
        }

        protected Task RefreshData()
        {
            return Task.WhenAll(GetGroups(), GetDisksUsage());
        }

        private async Task GetDisksUsage()
        {
            LogsFiles = await _sshService.GetLogsFiles();
        }

        private async Task GetGroups()
        {
            Groups = await _sshService.GetGroups();
            IsAdm = Groups.Any(g => g.ToLower() == "adm");
            CanSudo = Groups.Any(g => g.ToLower() == "root" || g.ToLower() == "sudo");
        }
    }
}