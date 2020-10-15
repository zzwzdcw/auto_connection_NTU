using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace 自动连接NTU
{
    public partial class Form1 : Form
    {
        string header = "http://210.29.79.141:801/eportal/?c=Portal&a=login&callback=dr1003&login_method=1&user_account=%2C0%2C";
        string user = "";
        string 电信 = "%40telecom";
        string 移动 = "%40cmcc";
        string 校园网 = "";//校园网确实为空值
        string 联通 = "%40unicom";
        string pwdset = "&user_password=";
        string pwd = "meng13781652561";
        string user_ip = "&wlan_user_ip=";
        string ip = "";
        string end = "&wlan_user_ipv6=&wlan_user_mac=000000000000&wlan_ac_ip=&wlan_ac_name=&jsVersion=3.3.2&v=";//这边唯一值得注意的就是jsVersion的值其它都是预设的
        string v = "6564";
        string ISP = "";



        public Form1()
        {
            InitializeComponent();
            LinkSSID();
            GetAddressIP();
            LoadProvince();
            tryLink();
            this.Close();

        }
        void GetAddressIP()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            label1.Text = AddressIP;
            ip= AddressIP;
        }
        void tryLink() {
            if (ISP=="电信")
            {
                System.Diagnostics.Process.Start(header + user + 电信 + pwdset + pwd + user_ip + ip + end + v);
            }
            if (ISP == "移动")
            {
                System.Diagnostics.Process.Start(header + user + 移动 + pwdset + pwd + user_ip + ip + end + v);
            }
            if (ISP== "校园网")
            {
                System.Diagnostics.Process.Start(header + user + 校园网 + pwdset + pwd + user_ip + ip + end + v);
            }
            if (ISP == "联通")
            {
                System.Diagnostics.Process.Start(header + user + 联通 + pwdset + pwd + user_ip + ip + end + v);

            }
            System.Diagnostics.Process.Start("www.baidu.com");
        }
        void LinkSSID() {
            using (Process RunCMD = new Process())//使用CMD来连接NTU
            {
                RunCMD.StartInfo = new ProcessStartInfo("cmd.exe", "/k netsh wlan connect name=NTU");
                RunCMD.Start();
                System.Threading.Thread.Sleep(1000);
                RunCMD.Kill();
            }
        }
        void LoadProvince()//加载配置信息
        {
            FileStream fs = new FileStream("data\\data.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);
            string strLine = "";
            strLine = sr.ReadToEnd();
           // strLine = sr.ReadLine();
            string[] sArray = strLine.Split(' ');
            user = sArray[0];
            pwd = sArray[1];    
            ISP = sArray[2];
            sr.Close();

        }
    }
}
