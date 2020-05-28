﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Views.CustomCommandList;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultDashBoard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenmediavaultDashboardPage : BaseContentPage
    {
        public OpenmediavaultDashboardPage()
        {
            InitializeComponent();
            Title = "OMV";
            var vm = App.Container.Resolve<OpenmediavaultDashboardViewModel>();
            Start(vm);
        }
    }
}