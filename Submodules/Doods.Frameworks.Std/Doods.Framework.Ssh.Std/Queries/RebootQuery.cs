using System.Text;
using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    public class RebootQuery : GenericQuery<bool>
    {
        public RebootQuery(IClientSsh client, string sudoPassword) : base(client)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(sudoPassword))
                sb.AppendFormat("echo \"{0}\" | sudo -S /sbin/shutdown -h now", sudoPassword);
            //TODO : using halte command  
        }

        protected override bool PaseResult(string result)
        {
            return true;
        }
    }
}