﻿namespace Doods.Openmediavault.Rpc.Std.Requests.System
{
    public class SystemInformationRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc System getInformation";

        public SystemInformationRequest() : base(RequestString)
        {
        }
    }
}