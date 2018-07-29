using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries.GroupedQueries
{
    public class VcgencmdQuery : GenericQuery<VcgencmdBean>
    {

        public int FREQ_ARM = 0;
        public int FREQ_CORE = 1;

        public VcgencmdQuery(IClientSsh client) : base(client)
        {
            CmdString = "vcgencmd";
        }

        protected override VcgencmdBean PaseResult(string result)
        {

            if (!IsValidVcgencmdPath(result))
            {
                if (CmdString == "vcgencmd")
                    CmdString = "/usr/bin/vcgencmd";
                else if(CmdString == "/usr/bin/vcgencmd")
                    CmdString = "/opt/vc/bin/vcgencmd";
                else
                {
                    return CreateVcgencmdBean(result);
                }

                return Run();
            }

            return CreateVcgencmdBean(result);
        }


        private bool IsValidVcgencmdPath(string result)
        {
            return !result.Contains("not found") && !result.Contains("no such file or directory");
        }

        private VcgencmdBean CreateVcgencmdBean(string result)
        {
           var bean = new VcgencmdBean();
            bean.ArmFrequency = new FreqQuery(Client, CmdString, FREQ_ARM).Run();
            bean.CoreFrequency = new FreqQuery(Client, CmdString, FREQ_CORE).Run();
            bean.CoreVolts = new VoltsQuery(Client, CmdString).Run();
            bean.CpuTemperature = new CpuTempQuery(Client, CmdString).Run();
            bean.Version = new FirmwareQuery(Client, CmdString).Run();
            return bean;
        }
    }
}
