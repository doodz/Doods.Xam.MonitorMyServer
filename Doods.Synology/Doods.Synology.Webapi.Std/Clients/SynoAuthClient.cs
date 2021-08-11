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
using System.Collections.Generic;
using System.Linq;
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


        public async Task<SynoAuthType> GetType(string username)
        {
            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName+".Type");
            loginRequest.AddParameter("version", "1");
            loginRequest.AddParameter("method", "get");
            loginRequest.AddParameter("account", username);
            var response = await _client.ExecuteAsync<SynologySimpleResponse<List<SynoAuthType>>>(loginRequest);
            return response.Data.Data.FirstOrDefault();
        }

        private async Task<bool> LoginAsyncV7(string username, string password)
        {
            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName);
            loginRequest.AddParameter("version", "7");
            loginRequest.AddParameter("method", "login");
            loginRequest.AddParameter("account", username);
            loginRequest.AddParameter("passwd", password);
            loginRequest.AddParameter("session", "FileStation");//webui
            loginRequest.AddParameter("enable_syno_token", "yes");
            loginRequest.AddParameter("enable_device_token", "no");
            loginRequest.AddParameter("rememberme", "0");
            try
            {

                var response = await _client.ExecuteAsync<SynologySimpleResponse<SynoLoginResult>>(loginRequest);
                //.ConfigureAwait(false);


                if (response.Data.Success)
                {
                    _client.Sid = response.Data.Data.Sid;
                    _client.Synotoken =response.Data.Data.Synotoken;
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

        public async Task<bool> LoginAsync(string username, string password)
        {


            if (_client.ApiInfo == null)
            {
                var obj = new SynoInfoClient(_client);
                _client.ApiInfo =await obj.GetSynologyApiServicesInfo();
            }

            var info =_client.ApiInfo[ServiceApiName];
            if (info.MaxVersion >= 7)
            {

                var logintype = await GetType(username);

                if (logintype?.Type != "passwd")
                {
                    throw new NotSupportedException($"login type \"{logintype?.Type}\" not supported");
                }

                return await LoginAsyncV7(username, password);
            }

            //format=cookie
            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName);
            loginRequest.AddParameter("version", "3");
            loginRequest.AddParameter("method", "login");
            loginRequest.AddParameter("account", username);
            loginRequest.AddParameter("passwd", password);
            loginRequest.AddParameter("session", "FileStation");//webui
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