// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="Hostnamectl.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2021
//  </copyright>
//  History:
//   2021/01/12 at 17:41: Thibault HERVIOU aka ThibaultHERVIOU.
// ---------------------------------------------------------------------------

using Doods.Framework.Std;

namespace Doods.Framework.Mobile.Ssh.Std.Models
{
    public class Hostnamectl : NotifyPropertyChangedBase
    {
        private string _architecture;
        private string _bootId;

        private string _chassis;
        private string _iconName;
        private string _kernel;

        private string _machineId;

        private string _operatingSystem;
        private string _staticHostname;
        private string _virtualization;


        public string StaticHostname
        {
            get => _staticHostname;
            internal set => SetProperty(ref _staticHostname, value);
        }

        public string IconName
        {
            get => _iconName;
            internal set => SetProperty(ref _iconName, value);
        }

        public string Chassis
        {
            get => _chassis;
            internal set => SetProperty(ref _chassis, value);
        }


        public string MachineID
        {
            get => _machineId;
            internal set => SetProperty(ref _machineId, value);
        }

        public string BootID
        {
            get => _bootId;
            internal set => SetProperty(ref _bootId, value);
        }

        public string Virtualization
        {
            get => _virtualization;
            internal set => SetProperty(ref _virtualization, value);
        }

        public string OperatingSystem
        {
            get => _operatingSystem;
            internal set => SetProperty(ref _operatingSystem, value);
        }

        public string Kernel
        {
            get => _kernel;
            internal set => SetProperty(ref _kernel, value);
        }

        public string Architecture
        {
            get => _architecture;
            internal set => SetProperty(ref _architecture, value);
        }
    }
}