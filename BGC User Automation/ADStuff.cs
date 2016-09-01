using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCMGToolbox;

namespace BGC_User_Automation
{
    public class ADStuff
    {
        public string ADTest(MainWindow m)
        {
            
            bool test = false;
            string domainName = String.Empty;
            string result = String.Empty;

            DCMGToolbox.ActiveDirectory ad = new ActiveDirectory();
            DCMGToolbox.Network net = new Network();

            try
            {
                m.WriteLogs("Checking for domain");
                domainName = ad.GetDomainName();
                m.WriteLogs("Domain: " + domainName);
                if (domainName != String.Empty)
                {
                    m.WriteLogs("Domain Found Attempting to Ping", Logging.LogType.Success);
                    test = net.Pingable(domainName);
                }                    
                else
                {
                    m.WriteLogs("Domain Not Found or doesnt exist", Logging.LogType.Warning);
                    domainName = "NA";
                }
                    
                
                if (test == true  && domainName != "NA")
                {
                    m.WriteLogs("Domain found and pingable",Logging.LogType.Success);
                    result = "pingable || " + domainName;
                }                    
                else
                {
                    m.WriteLogs("Domain found but not pingable",Logging.LogType.Error);
                    result = "notpingable || " + domainName;
                }
                    
            }
            catch(Exception ex)
            {
                m.WriteLogs(ex.Message, Logging.LogType.Critical);
                result = "Exception || " + ex.Message;
            }            

            return result;
        }

        public void CreateAdUser(string username, string firstName, string lastName, string password, string emailAddress = "", bool passNeverExpires = false, bool userCannotChangePW = false, bool changePWOnNextLogon = false)
        {
            ActiveDirectory ad = new ActiveDirectory();
            ad.CreateUser(username, firstName, lastName, password, emailAddress, passNeverExpires, userCannotChangePW, changePWOnNextLogon);
        }

        public string GetDomainDN(string domain)
        {
            string DN = String.Empty;

            ActiveDirectory ad = new ActiveDirectory();
            DN = ad.GetDomainDN(domain);

            return DN;
        }

    }
}
