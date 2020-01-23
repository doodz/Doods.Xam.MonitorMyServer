﻿namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class GetPowerManagementSettingRequest : OmvRequestBase
    {
        private static string _commandText = "omv-rpc PowerMgmt get";

        public GetPowerManagementSettingRequest() : base(_commandText)
        {
        }
    }
}