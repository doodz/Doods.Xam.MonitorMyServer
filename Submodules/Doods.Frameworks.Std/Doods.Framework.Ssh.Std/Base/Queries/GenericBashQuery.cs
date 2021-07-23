using System;
using Doods.Framework.Ssh.Std.Interfaces;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace Doods.Framework.Ssh.Std.Base.Queries
{
    public class GenericBashQuery<T>
    {
        // FICHIER_TEMP=`mktemp -t shh_doods.XXXXX` && sudo apt-get update >> $FICHIER_TEMP && echo ${FICHIER_TEMP}

        // FICHIER_TEMP=`mktemp -t shh_doods.XXXXX` && sudo bash -c 'exec sudo apt-get update >> $FICHIER_TEMP' && echo ${FICHIER_TEMP}
        // sudo bash -c 'exec  apt-get update >> toto42' & echo $!

        // FICHIER_TEMP=`mktemp -t shh_doods.XXXXX` && sudo bash -c 'exec  apt-get update >> $FICHIER_TEMP' && echo ${FICHIER_TEMP}

        //sudo bash -c ' FICHIER_TEMP=`mktemp -t shh_doods.XXXXX` && apt-get update >> $FICHIER_TEMP' && echo ${FICHIER_TEMP}


        // pi @raspberrypi:~ $ FICHIER_TEMP=$(mktemp -t shh_doods.XXXXX) && echo $FICHIER_TEMP
        // /tmp/shh_doods.3CahH

        // pi@raspberrypi:~ $ FICHIER_TEMP=$(mktemp -t shh_doods.XXXXX) && echo $FICHIER_TEMP &&ls >> $FICHIER_TEMP
        // /tmp/shh_doods.fulAw

        // pi@raspberrypi:~ $ bash -c 'FICHIER_TEMP=$(mktemp -t shh_doods.XXXXX) && echo $FICHIER_TEMP && exec 'ls' >>  $FICHIER_TEMP'
        // /tmp/shh_doods.En4ja


        // pi@raspberrypi:~ $ bash -c 'exec -a sadhadxk sleep 1000000'&
        // [1] 4017

        // pi@raspberrypi:~ $ bash -c 'FICHIER_TEMP=$(mktemp -t shh_doods.XXXXX) && echo $FICHIER_TEMP && exec sudo ls -la>>  $FICHIER_TEMP'
        // /tmp/shh_doods.WmH0g

        // nohup myprogram > foo.out 2> foo.err < /dev/null &

        // FICHIER_TEMP=`mktemp -t shh_doods.XXXXX` > /dev/null && echo ${FICHIER_TEMP}& nohup sudo apt-get update > $FICHIER_TEMP 2>&1 < /dev/null &
        private readonly string _bashCmd = "sudo bash -c 'exec {0}' && echo \"" + ReturnQuery.ResultOk +
                                           "\" || echo \"" + ReturnQuery.ResultKo + "\"";

        protected readonly IClientSsh Client;
        protected string CmdString;
        public ShellStream shell;

        public GenericBashQuery(IClientSsh client, string cmdString) : this(client)
        {
            //CmdString = cmdString;
            CmdString = string.Format(_bashCmd, cmdString);
        }

        public GenericBashQuery(IClientSsh client)
        {
            Client = client;
        }

        public virtual void RunInShell(EventHandler<ShellDataEventArgs> ondataReseved)
        {
            if (!Client.IsConnected())
            {
                Client.Logger.Info("Client not connected, Connect. for run in shell");
                Client.Connect();
            }

            //shell = Client.CreateShell();
            //shell.DataReceived += ondataReseved;
            //shell.WriteLine(CmdString);
        }
    }
}