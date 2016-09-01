using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DCMGToolbox;
using System.Threading;



namespace BGC_User_Automation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            Task.Factory.StartNew(() =>Init(this));            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ADStuff ads = new ADStuff();
            //lblTestOutput.Content = ads.ADTest();            
        }

       
        
        private void Init(MainWindow m)
        {
            lblStatusBar.UpdateStatusLabel("Looking up and pinging Domain");
            WriteLogs("Application Init Starting");
            string adTest = String.Empty;
            ADStuff ads = new ADStuff();            
            adTest = ads.ADTest(m);
            DCMGToolbox.ActiveDirectory ad = new ActiveDirectory();

            string[] split = adTest.Split(new string[] { "||" },StringSplitOptions.None);
            string pingres = split[0];
            string domain = split[1];            

            txtDomain.UpdateTextBoxTS(pingres);

            if (pingres.ToUpper().Trim() == "PINGABLE")
            {
                txtDomain.UpdateTextBoxTS(domain);
                txtPingable.UpdateTextBoxTS("True");
                //build combobox        
                //"LDAP://DC=YourCompany,DC=com"
                List<string> l = new List<string>();
                string dn = ad.ConvertStringDomainToDN(domain);
                l = ad.GetOUList(dn);
                cbxOUs.UpdateCBXDatasource(l);                
            }

            if (domain.ToUpper().Trim() == "NA")
            {
                txtDomain.UpdateTextBoxTS("No Domain Found");
                txtPingable.UpdateTextBoxTS("False");
            }
            lblStatusBar.UpdateStatusLabel("Domain lookup task completed.");

            
        }

        public void WriteLogs(string logEntry,Logging.LogType lt = Logging.LogType.Message)
        {
            string entry = Environment.NewLine + DateTime.Now.ToString() + " " + lt.ToString() + " " + logEntry;

            if (lt == Logging.LogType.Message)
            {                
                rtbLog.AppendRTBText(entry, System.Windows.Media.Brushes.Black);
            }

            if (lt == Logging.LogType.Error)
            {
                rtbLog.AppendRTBText(entry, System.Windows.Media.Brushes.Red);
            }

            if (lt== Logging.LogType.Success)
            {
                rtbLog.AppendRTBText(entry, System.Windows.Media.Brushes.Green);
            }

            if(lt == Logging.LogType.Warning)
            {
                rtbLog.AppendRTBText(entry, System.Windows.Media.Brushes.Orange);
            }

            if (lt == Logging.LogType.Critical)
            {
                rtbLog.AppendRTBText(entry, System.Windows.Media.Brushes.DarkRed,true);
            }
        }

        private void mnuFileExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            ADStuff a = new ADStuff();
            

            string fName = txtFirstName.Text;
            string lName = txtLastName.Text;
            string uName = txtUserName.Text;
            string pWord = String.Empty;
            string email = txtEmail.Text;
            bool passOnLogon = chkChangePWOnLogin.IsChecked.Value;
            bool passNeverExpires = chkPwdNeverExpires.IsChecked.Value;
            bool cannotChangePW = chkCannotChangePW.IsChecked.Value;
            string domain = txtDomain.Text;            
            

            
            if (pbPassword.Password == pbConfirm.Password)
            {
                pWord = pbConfirm.Password;
                //MessageBox.Show(pWord + " " + passOnLogon.ToString());
                if(fName.Count() > 1 && lName.Count() > 1 && uName.Count() > 1 && pWord.Count() > 8)
                {
                    MessageBoxResult mb = MessageBox.Show("Please verify the information below:", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (mb == MessageBoxResult.OK)
                    {
                        //a.CreateAdUser(uName, fName, lName, pWord, email, passNeverExpires, cannotChangePW, passOnLogon);
                                                
                    }
                }
                else
                {
                    MessageBox.Show("UserName, FirstName, LastName, and Password cannot be blank" + Environment.NewLine + Environment.NewLine + "Password must also be longer than 8 characters","Information",MessageBoxButton.OK,MessageBoxImage.Information);
                }
                
            }
            else
            {
                MessageBoxResult mb = MessageBox.Show("Passwords do NOT match!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            
        }
    }
}
