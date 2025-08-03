using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTCP;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SimpleTcpServer server;
        private void Form1_Load(object sender, EventArgs e)
        {
            string strHostName = string.Empty;
            strHostName = Dns.GetHostName();
            var host = Dns.GetHostEntry(Dns.GetHostName());
            string my_ip = string.Empty;
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    my_ip = ip.ToString();
                    break;
                }
            }
            textBox1.Text = my_ip;

            server = new SimpleTcpServer();
            server.Delimiter = 0x13; //enter
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            textBox3.Invoke((MethodInvoker)delegate ()
            {
                textBox3.Text += e.MessageString + '\n';
                //e.ReplyLine(string.Format("You said: {0}", e.MessageString));
            });

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text += "Server starting...";
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(textBox1.Text);
            server.Start(ip, Convert.ToInt32(textBox2.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (server.IsStarted)
            {
                server.Stop();
            }
        }
    }
}
