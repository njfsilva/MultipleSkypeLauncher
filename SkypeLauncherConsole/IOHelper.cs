using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SkypeLauncherConsole
{
    public static class IOHelper
    {
        public static List<Account> LoadAccounts(string accountFile)
        {
            if (!File.Exists(accountFile))
            {
                File.Create(accountFile);
            }

            try
            {
                using (var stream = new FileStream(accountFile, FileMode.Open))
                {
                    var xml = new XmlSerializer(typeof (List<Account>));
                    return (List<Account>) (xml.Deserialize(stream));
                }
            }
            catch
            {
                return null;
            }
        }

        public static bool SaveAccounts(List<Account> accounts, string accountsFile)
        {
            try
            {
                using (var stream = new FileStream(accountsFile, FileMode.OpenOrCreate))
                {
                    var xml = new XmlSerializer(typeof (List<Account>));
                    xml.Serialize(stream, accounts);
                }

                return true;
            }
            catch
            {
                return false;
            }
            
        }
    } 
}
