﻿using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Framework.Std.Utilities;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views;
using System;
using Doods.Xam.MonitorMyServer.Resx;

namespace Doods.Xam.MonitorMyServer
{
    public partial class MainPage : BaseContentPage
    {
        private SshService ssh;

        public MainPage()
        {
            InitializeComponent();
            Title = Resource.Home;
            var vm = App.Container.Resolve<MainPageViewModel>();
            Start(vm);

        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
           
        }
    }


    internal class toto : ISshRequest,IDeserializer
    {
        public T Deserialize<T>(ISshResponse response)
        {
            return (T)DeserializeObject(response.Content, typeof(T));

            return default(T);
        }


        private object DeserializeObject(string value,Type objectType)
        {
            ValidationUtils.ArgumentNotNull(value, nameof(value));
            //if (!ReflectionUtils.IsNullableType(objectType))
            //{
            //    throw new Exception($"Cannot convert null value to {objectType.ToString()}.");

            //}

            return value;
        }

        public string CommandText { get; } = "ls -la";
        public IDeserializer Handler { get; set; }
    }
}