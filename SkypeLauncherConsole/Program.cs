using System.Configuration;
using System.Diagnostics;

namespace SkypeLauncherConsole
{
    static class Program
    {
        static void Main()
        {
            var accounts = IOHelper.LoadAccounts(StaticStrings.AccountsFileName);

            if (accounts == null) return;

            var skypeLocation = ConfigurationManager.AppSettings["SkypeLocation"];

            if (!string.IsNullOrEmpty(skypeLocation))
            {
                accounts.ForEach(account =>
                {
                    var username = account.Username.Trim();
                    var password = account.Password.Trim();

                    var start = new ProcessStartInfo
                    {
                        Arguments = string.Format("/Secondary /nosplash /minimized /username:{0} /password:{1}", username, password),
                        FileName = skypeLocation.Trim()
                    };

                    using (var proc = Process.Start(start))
                    {
                        if (proc == null) return;
                        proc.Close();
                    }
                });
            }
        }
         
    }
}
