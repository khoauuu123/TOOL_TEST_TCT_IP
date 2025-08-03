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

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SimpleTcpClient client;
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
            //textBox1.Text = my_ip;

            button2.Enabled = false;

            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            textBox3.Invoke((MethodInvoker)delegate ()
            {
                textBox3.Text += e.MessageString + '\n';
            });

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            client.Connect(textBox1.Text.ToString(), Convert.ToInt32(textBox2.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button1.Enabled = true;
            client.Disconnect();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button1.Enabled == false)
            {
                client.Write(textBox4.Text + '\n');
                textBox3.Text += "SEND: " + textBox4.Text + "\r\n";
            }
        }
    }
}
