namespace Doods.Framework.Ssh.Std.Beans
{
    public class LastloginBean
    {
        public string UserName { get; internal set; }
        public string LogedOn { get; internal set; }

        public string Date { get; internal set; }

        public string StillLogged { get; internal set; }
        public string LogedIn { get; internal set; }
        public string LogedFrom { get; internal set; }
    }
}