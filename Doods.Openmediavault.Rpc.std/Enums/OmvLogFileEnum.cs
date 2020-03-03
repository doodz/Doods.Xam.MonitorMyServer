// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="OmvLogFileEnum.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2020
//  </copyright>
//  History:
//   2020/03/02 at 16:33: Thibault HERVIOU aka ThibaultHERVIOU.
// ---------------------------------------------------------------------------

using System.ComponentModel;

namespace Doods.Openmedivault.Ssh.Std.Data
{
    public enum OmvLogFileEnum
    {
        [Description("Authentication")]
        auth,
        [Description("Boot")]
        boot,
        [Description("Daemon")]
        daemon,
        [Description("Ftp")]
        proftpd,
        [Description("FTP - Transfer log")]
        proftpd_xferlog,
        [Description("Messages")]
        messages,
        [Description("Rsync - Jobs")]
        rsync,
        [Description("Rsync - Server")]
        rsyncd,
        [Description("S.M.A.R.T.")]
        smartd,
        [Description("SMB/CIFS - Audit")]
        smbdaudit,
        [Description("Syslog")]
        syslog,
        [Description("Update Management - History")]
        apt_history,
        [Description("Update Management - Terminal output")]
        apt_term
    }
}