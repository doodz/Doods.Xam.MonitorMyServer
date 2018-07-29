using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    ///  ls -l /var/log --time-style=long-iso
    ///  total 3240
    ///  -rw-r--r-- 1 root root      0 2017-08-01 20:20 alternatives.log
    ///  -rw-r--r-- 1 root root    188 2017-07-23 18:16 alternatives.log.1
    ///  -rw-r--r-- 1 root root    133 2017-06-05 20:01 alternatives.log.2.gz
    ///  -rw-r--r-- 1 root root    315 2017-05-07 18:02 alternatives.log.3.gz
    ///  -rw-r--r-- 1 root root    541 2017-04-29 18:21 alternatives.log.4.gz
    ///  -rw-r--r-- 1 root root    678 2017-03-31 12:54 alternatives.log.5.gz
    ///  drwxr-xr-x 2 root root   4096 2017-09-01 07:35 apt
    ///  -rw-r----- 1 root adm   78533 2017-09-12 10:45 auth.log
    ///  -rw-r----- 1 root adm  571937 2017-09-11 07:35 auth.log.1
    ///  -rw-r----- 1 root adm   23960 2017-09-03 07:35 auth.log.2.gz
    ///  -rw-r----- 1 root adm   28553 2017-08-28 07:35 auth.log.3.gz
    ///  -rw-r----- 1 root adm   20911 2017-08-20 07:35 auth.log.4.gz
    ///  -rw-r--r-- 1 root root      0 2016-12-29 12:40 bootstrap.log
    ///  -rw-rw---- 1 root utmp   1152 2017-09-06 14:53 btmp
    ///  -rw-rw---- 1 root utmp    768 2017-08-28 21:10 btmp.1
    ///  drwxr-xr-x 2 root root   4096 2017-09-11 07:35 cron-apt
    ///  -rw-r----- 1 root adm  286851 2017-09-12 10:45 daemon.log
    ///  -rw-r----- 1 root adm  466836 2017-09-11 07:35 daemon.log.1
    ///  -rw-r----- 1 root adm   61082 2017-09-03 07:35 daemon.log.2.gz
    ///  -rw-r----- 1 root adm   18933 2017-08-28 07:35 daemon.log.3.gz
    ///  -rw-r----- 1 root adm   14886 2017-08-20 07:35 daemon.log.4.gz
    ///  -rw-r----- 1 root adm   17713 2017-09-12 10:45 debug
    ///  -rw-r----- 1 root adm  122374 2017-09-11 07:30 debug.1
    ///  -rw-r----- 1 root adm    4957 2017-09-03 07:30 debug.2.gz
    ///  -rw-r----- 1 root adm    6449 2017-08-28 07:30 debug.3.gz
    ///  -rw-r----- 1 root adm    4917 2017-08-20 07:30 debug.4.gz
    ///  drwxr-s--- 2 debian-deluged adm    4096 2015-02-15 17:47 deluged
    ///  -rw-r----- 1 root adm       0 2016-12-29 12:40 dmesg
    ///  -rw-r--r-- 1 root root   1625 2017-09-03 20:18 dpkg.log
    ///  -rw-r--r-- 1 root root  24191 2017-08-30 13:34 dpkg.log.1
    ///  -rw-r--r-- 1 root root   5101 2017-07-25 20:54 dpkg.log.2.gz
    ///  -rw-r--r-- 1 root root   4391 2017-06-28 19:05 dpkg.log.3.gz
    ///  -rw-r--r-- 1 root root   7860 2017-05-31 11:17 dpkg.log.4.gz
    ///  -rw-r--r-- 1 root root   6073 2017-04-29 18:21 dpkg.log.5.gz
    ///  -rw-r--r-- 1 root root  24298 2017-03-31 12:54 dpkg.log.6.gz
    ///  -rw-r----- 1 root adm       0 2017-09-11 07:35 fail2ban.log
    ///  -rw-r----- 1 root adm       0 2017-09-03 07:35 fail2ban.log.1
    ///  -rw-r----- 1 root adm      20 2017-08-28 07:35 fail2ban.log.2.gz
    ///  -rw-r----- 1 root adm      20 2017-08-20 07:35 fail2ban.log.3.gz
    ///  -rw-r----- 1 root adm      20 2017-08-14 07:35 fail2ban.log.4.gz
    ///  -rw-r----- 1 root adm      20 2017-08-06 07:35 fail2ban.log.5.gz
    ///  -rw-r--r-- 1 root root  32064 2017-08-30 08:10 faillog
    ///  -rw-r--r-- 1 root root    863 2017-03-31 12:52 fontconfig.log
    ///  drwxr-xr-x 2 root root   4096 2017-03-31 12:28 fsck
    ///  drwxr-xr-x 3 root root   4096 2017-03-31 12:28 installer
    ///  -rw-r----- 1 root adm    7233 2017-09-11 21:41 kern.log
    ///  -rw-r----- 1 root adm    2412 2017-09-09 17:29 kern.log.1
    ///  -rw-r----- 1 root adm     170 2017-09-04 15:58 kern.log.2.gz
    ///  -rw-r----- 1 root adm     538 2017-08-28 14:30 kern.log.3.gz
    ///  -rw-r----- 1 root adm   15651 2017-08-03 04:05 kern.log.4.gz
    ///  -rw-rw-r-- 1 root utmp 292584 2017-09-12 10:42 lastlog
    ///  drwx------ 2 root root  20480 2017-09-12 00:27 letsencrypt
    ///  -rw-r----- 1 root adm    2107 2017-09-12 10:17 mail.info
    ///  -rw-r----- 1 root adm   14563 2017-09-11 07:17 mail.info.1
    ///  -rw-r----- 1 root adm    1191 2017-09-03 07:17 mail.info.2.gz
    ///  -rw-r----- 1 root adm    1440 2017-08-28 07:17 mail.info.3.gz
    ///  -rw-r----- 1 root adm    1142 2017-08-20 07:17 mail.info.4.gz
    ///  -rw-r----- 1 root adm    2107 2017-09-12 10:17 mail.log
    ///  -rw-r----- 1 root adm   14563 2017-09-11 07:17 mail.log.1
    ///  -rw-r----- 1 root adm    1191 2017-09-03 07:17 mail.log.2.gz
    ///  -rw-r----- 1 root adm    1440 2017-08-28 07:17 mail.log.3.gz
    ///  -rw-r----- 1 root adm    1142 2017-08-20 07:17 mail.log.4.gz
    ///  -rw-r----- 1 root adm       0 2017-04-02 07:35 mail.warn
    ///  -rw-r----- 1 root adm     238 2017-03-31 12:31 mail.warn.1
    ///  -rw-r----- 1 root adm   12531 2017-09-12 07:36 messages
    ///  -rw-r----- 1 root adm   41544 2017-09-11 07:35 messages.1
    ///  -rw-r----- 1 root adm    2777 2017-09-03 07:35 messages.2.gz
    ///  -rw-r----- 1 root adm    2857 2017-08-28 07:35 messages.3.gz
    ///  -rw-r----- 1 root adm    2445 2017-08-20 07:35 messages.4.gz
    ///  -rw-r----- 1 root adm       0 2016-12-29 12:40 monit.log
    ///  drwxr-xr-x 2 root adm    4096 2017-09-11 07:35 nginx
    ///  drwxr-xr-x 2 ntp ntp    4096 2016-07-22 19:31 ntpstats
    ///  -rw-r----- 1 root adm  452307 2017-08-30 13:22 nut.log
    ///  drwxr-xr-x 2 root root   4096 2017-03-31 12:28 openmediavault
    ///  -rw------- 1 root root     56 2017-09-11 07:35 php5-fpm.log
    ///  -rw------- 1 root root     56 2017-09-03 07:35 php5-fpm.log.1
    ///  -rw------- 1 root root    248 2017-07-02 16:12 php5-fpm.log.10.gz
    ///  -rw------- 1 root root    214 2017-06-23 20:17 php5-fpm.log.11.gz
    ///  -rw------- 1 root root     76 2017-06-11 07:35 php5-fpm.log.12.gz
    ///  -rw------- 1 root root     76 2017-08-28 07:35 php5-fpm.log.2.gz
    ///  -rw------- 1 root root     76 2017-08-20 07:35 php5-fpm.log.3.gz
    ///  -rw------- 1 root root     76 2017-08-14 07:35 php5-fpm.log.4.gz
    ///  -rw------- 1 root root     76 2017-08-06 07:35 php5-fpm.log.5.gz
    ///  -rw------- 1 root root    250 2017-08-01 20:15 php5-fpm.log.6.gz
    ///  -rw------- 1 root root     76 2017-07-23 07:35 php5-fpm.log.7.gz
    ///  -rw------- 1 root root     76 2017-07-17 08:44 php5-fpm.log.8.gz
    ///  -rw------- 1 root root    244 2017-07-17 08:39 php5-fpm.log.9.gz
    ///  drwxr-xr-x 2 root root   4096 2017-09-11 07:35 proftpd
    ///  drwxr-x--- 3 root adm    4096 2017-09-11 15:53 samba
    ///  -rw-r----- 1 root adm   12103 2017-09-12 10:45 syslog
    ///  -rw-r----- 1 root adm  329740 2017-09-12 07:35 syslog.1
    ///  -rw-r----- 1 root adm   13796 2017-09-11 07:35 syslog.2.gz
    ///  -rw-r----- 1 root adm   13038 2017-09-10 07:35 syslog.3.gz
    ///  -rw-r----- 1 root adm    8744 2017-09-09 07:35 syslog.4.gz
    ///  -rw-r----- 1 root adm    8733 2017-09-08 07:35 syslog.5.gz
    ///  -rw-r----- 1 root adm    9071 2017-09-07 07:35 syslog.6.gz
    ///  -rw-r----- 1 root adm    8723 2017-09-06 07:35 syslog.7.gz
    ///  drwxr-xr-x 2 root root   4096 2014-09-27 11:39 sysstat
    ///  -rw-r--r-- 1 root root  63936 2017-09-11 17:37 tallylog
    ///  -rw-r----- 1 root adm    7892 2017-09-12 07:36 transmissionbt.log
    ///  -rw-r----- 1 root adm   67005 2017-09-10 07:41 transmissionbt.log.1
    ///  -rw-r----- 1 root adm    3786 2017-09-02 08:16 transmissionbt.log.2.gz
    ///  -rw-r----- 1 root adm    2734 2017-08-27 20:41 transmissionbt.log.3.gz
    ///  -rw-r----- 1 root adm    2530 2017-08-19 08:25 transmissionbt.log.4.gz
    ///  drwxr-x--- 2 root adm    4096 2017-05-17 17:53 unattended-upgrades
    ///  -rw-r----- 1 root adm    6108 2017-09-12 07:36 user.log
    ///  -rw-r----- 1 root adm   31892 2017-09-11 04:07 user.log.1
    ///  -rw-r----- 1 root adm    2038 2017-09-03 04:53 user.log.2.gz
    ///  -rw-r----- 1 root adm    2344 2017-08-28 04:17 user.log.3.gz
    ///  -rw-r----- 1 root adm    1961 2017-08-20 04:41 user.log.4.gz
    ///  drwxr-xr-x 2 root root   4096 2017-03-31 12:28 watchdog
    ///  -rw-rw-r-- 1 root utmp   4224 2017-09-12 10:42 wtmp
    ///  -rw-rw-r-- 1 root utmp   6144 2017-08-31 14:14 wtmp.1
    /// </example>
    public class GetLogsListQuery : GetListFileBaseQuery
    {
        public static readonly string Path = "/var/log";
        public GetLogsListQuery(IClientSsh client) : base(client, Path)
        {

        }
    }
}