﻿namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class ListRddRequest : OmvRequestBase
    {

        public const string RequestString = " ls  /var/lib/openmediavault/rrd";
        public ListRddRequest() : base(RequestString)
        {

        }

    }
}