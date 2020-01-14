// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="PluginInfo.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2019
//  </copyright>
//  History:
//   2019/10/03 at 10:22:  aka therv.
// ---------------------------------------------------------------------------

using System;
using Doods.Framework.Std;

namespace Doods.Openmediavault.Mobile.Std.Models
{
    public class PluginInfo : NotifyPropertyChangedBase
    {
        private bool _isSelected;
        public string Abstract { get; set; }

        public string Architecture { get; set; }

        public string Breaks { get; set; }

        public string Conflicts { get; set; }

        public string Depends { get; set; }

        public string Description { get; set; }

        public string Descriptionmd5 { get; set; }

        public string Extendeddescription { get; set; }

        public string Filename { get; set; }

        public Uri Homepage { get; set; }

        public bool Installed { get; set; }

        public long Installedsize { get; set; }

        public string Maintainer { get; set; }

        public string Md5Sum { get; set; }

        public string Multiarch { get; set; }

        public string Name { get; set; }

        public string Package { get; set; }

        public string Pluginsection { get; set; }

        public string Predepends { get; set; }

        public string Priority { get; set; }

        public string Repository { get; set; }

        public string Section { get; set; }

        public string Sha1 { get; set; }

        public string Sha256 { get; set; }

        public long Size { get; set; }

        public string Suggests { get; set; }

        public string Summary { get; set; }

        public string Version { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}