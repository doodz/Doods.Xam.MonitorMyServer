using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doods.Framework.Std.Lists;
using Doods.Openmedivault.Ssh.Std.Requests;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Renci.SshNet;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultStatistics
{
    public class OpenmediavaultStatisticsViewModel : ViewModelWhithState
    {
        private IOMVSshService _sshService;

        public OpenmediavaultStatisticsViewModel(IOMVSshService sshService)
        {
            _sshService = sshService;
        }
        public ObservableRangeCollection<RrdImageSource> Items { get;  } = new ObservableRangeCollection<RrdImageSource>();

        protected override async Task OnInternalAppearingAsync()
        {
          await RefreshData(); 
            await base.OnInternalAppearingAsync();
        }

        protected Task RefreshData(bool b = true)
        {
           
            
            return Task.WhenAll(ListRdd());
        }

        private async Task ListRdd()
        {
            SetLabelsStateItem(Resource.Dowloading, Resource.PleaseWait);
            ViewModelStateItem.IsRunning = true;
            //var list = new List<RrdImageSource>();
            Items.Clear();
            var result = await _sshService.ListRdd();
            var client = _sshService.GetScpClient();
            if (!client.IsConnected) client.Connect();
            client.RemotePathTransformation = RemotePathTransformation.ShellQuote;
            foreach (var file in result)
            {
                using (var ms = new MemoryStream())
                {
                   
                    client.Download("/var/lib/openmediavault/rrd/" + file.Trim(), ms);
                    var img = new StreamImageSource();

                    var byteArray = ms.ToArray();
                    img.Stream = (token) => Task.FromResult<Stream>(new MemoryStream(byteArray));

                    var item = new RrdImageSource();
                    item.FileName = file;
                    item.ImageSource = img;
                    Items.Add(item);
                }
            }

            //Items.ReplaceRange(list);
            ViewModelStateItem.IsRunning = false;
        }

        private async Task GenerateRdd()
        {
           var result = await  _sshService.GenerateRdd();
          
        }


    }

    public class RrdImageSource
    {
        public ImageSource ImageSource { get; set; }

        public string FileName { get; set; }
    }
}
