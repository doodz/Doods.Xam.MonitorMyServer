// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="SynoAuthClient.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2020
//  </copyright>
//  History:
//   2020/04/01 at 13:56: Thibault HERVIOU aka ThibaultHERVIOU.
// ---------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Doods.Synology.Webapi.Std.NewFolder;

namespace Doods.Synology.Webapi.Std
{
    public interface ISynoAuthClient
    {
        Task<bool> LoginAsync(string username, string password);
        void LogOut();
    }

    public class SynoAuthClient : BaseSynoClient, ISynoAuthClient
    {
        public SynoAuthClient(ISynoWebApi client) : base(client)
        {
            Resource = "/auth.cgi";
            ServiceApiName = "SYNO.API.Auth";
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            //format=cookie
            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName);
            loginRequest.AddParameter("version", "3");
            loginRequest.AddParameter("method", "login");
            loginRequest.AddParameter("account", username);
            loginRequest.AddParameter("passwd", password);
            loginRequest.AddParameter("session", "FileStation");
            loginRequest.AddParameter("format", "cookie");

            try
            {
                var response = await _client.ExecuteAsync<SynologyResponse<SynoLoginInfo>>(loginRequest);
                //.ConfigureAwait(false);


                if (response.Data.Success)
                {
                    _client.Sid = response.Data.Data.Sid;
                    _client.LoggedInTime = DateTime.Now;
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return false;
        }

        public void LogOut()
        {
            var request = new SynologyRestRequest(Resource);
            request.AddParameter("api", ServiceApiName);
            request.AddParameter("version", "1");
            request.AddParameter("method", "logout");
            request.AddParameter("session", "FileStation");
            _client.Execute(request);
            _client.Sid = null;
        }
    }
}