using System.Configuration;
using System.Diagnostics;

namespace SkypeLauncherConsole
{
    internal static class Program
    {
        private static void Main()
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
                        Arguments = $"/Secondary /nosplash /minimized /username:{username} /password:{password}",
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
