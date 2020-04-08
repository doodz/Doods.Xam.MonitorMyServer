using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Threading;
using Doods.Framework.Std.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.Openmediavault.Mobile.Std.Resources
{
    [ContentProperty("Text")]
    public class TranslateExtension : TranslateService, IMarkupExtension
    {
        readonly CultureInfo ci = null;
        const string ResourceId = "Doods.Openmediavault.Mobile.Std.Resources";

        private static IDictionary<string, ResourceManager> ResourcesManagersLst =
            new Dictionary<string, ResourceManager>()
            {
               
            { nameof(openmediavault), new ResourceManager("Doods.Openmediavault.Mobile.Std.Resources.openmediavault",(typeof(openmediavault).Assembly))},
            { nameof(openmediavault_clamav), new ResourceManager("Doods.Openmediavault.Mobile.Std.Resources.openmediavault_clamav",(typeof(openmediavault_clamav).Assembly))},
            { nameof(openmediavault_diskstats), new ResourceManager("Doods.Openmediavault.Mobile.Std.Resources.openmediavault_diskstats",(typeof(openmediavault_diskstats).Assembly))},
            { nameof(openmediavault_forkeddaapd), new ResourceManager("Doods.Openmediavault.Mobile.Std.Resources.openmediavault_forkeddaapd",(typeof(openmediavault_forkeddaapd).Assembly))},
            { nameof(openmediavault_ldap), new ResourceManager("Doods.Openmediavault.Mobile.Std.Resources.openmediavault_ldap",(typeof(openmediavault_ldap).Assembly))},
            { nameof(openmediavault_lvm2), new ResourceManager("Doods.Openmediavault.Mobile.Std.Resources.openmediavault_lvm2",(typeof(openmediavault_lvm2).Assembly))},
            { nameof(openmediavault_nut), new ResourceManager("Doods.Openmediavault.Mobile.Std.Resources.openmediavault_nut",(typeof(openmediavault_nut).Assembly))},
            { nameof(openmediavault_shairport), new ResourceManager("Doods.Openmediavault.Mobile.Std.Resources.openmediavault_shairport",(typeof(openmediavault_shairport).Assembly))},
            { nameof(openmediavault_snmp), new ResourceManager("Doods.Openmediavault.Mobile.Std.Resources.openmediavault_snmp",(typeof(openmediavault_snmp).Assembly))},
            { nameof(openmediavault_tftp), new ResourceManager("Doods.Openmediavault.Mobile.Std.Resources.openmediavault_tftp",(typeof(openmediavault_tftp).Assembly))},
            { nameof(openmediavault_usbbackup), new ResourceManager("Doods.Openmediavault.Mobile.Std.Resources.openmediavault_usbbackup",(typeof(openmediavault_usbbackup).Assembly))},
            };


        //static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(
        //    () => new ResourceManager(ResourceId, IntrospectionExtensions.GetTypeInfo(typeof(TranslateExtension)).Assembly));

        public string Text { get; set; }

        public TranslateExtension():base(Resource.ResourceManager)
        {
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return string.Empty;

            if (Text.Contains(nameof(openmediavault)))
            {
                var array =Text.Split(new string[]{"::"},StringSplitOptions.RemoveEmptyEntries);
                ResourceManager manager;
                if (ResourcesManagersLst.TryGetValue(array[0],out manager) )
                {
                    var toto = openmediavault.ResourceManager.GetString(array[1]);
                    
                    var culture1name = CultureInfo.CurrentCulture.Name;
                    var culture2Name = CultureInfo.DefaultThreadCurrentUICulture?.Name;
                    var culture3Name = Thread.CurrentThread.CurrentUICulture?.Name;
                    if(Text.Equals(nameof(openmediavault)))
                    {
                        this.Translate(array[1], new ResourceManager("Doods.Openmediavault.Mobile.Std.Resources.openmediavault", (typeof(openmediavault).Assembly)));
                    }
                    else
                        return this.Translate(array[1], manager);
                }
            }

            return this.Translate(Text);
          
        }
    }
}
