using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Smart_Home
{
    public partial class Form1 : Form
    {
        static SerialPort serialPort;

        public Form1()
        {
            InitializeComponent();
            getAvailablePorts();
        }

        void getAvailablePorts()
        {
            String[]  ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if(comboBox1.Text == "")
                {
                    MessageBox.Show("Invalid COM port!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    serialPort = new SerialPort();
                    serialPort.ReadTimeout = 3000;
                    serialPort.WriteTimeout = 1500;
                    serialPort.PortName = comboBox1.Text;
                    serialPort.BaudRate = 9600;
                    serialPort.Open();
                    label6.Text = "CONNECTED";
                    label6.BackColor = Color.Green;
                    button1.Enabled = true;
                    button3.Enabled = false;
                    button4.Enabled = true;
                    checkBox1.Enabled = true;
                    checkBox2.Enabled = true;
                    comboBox1.Enabled = false;
                }
            }
            catch(UnauthorizedAccessException)
            {
                MessageBox.Show("Port is busy!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            serialPort.Close();
            label6.Text = "DISCONNECTED";
            label6.BackColor = Color.Red;
            button1.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = false;
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            comboBox1.Enabled = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            label2.Text = "";
            serialPort.Write("weather");
            try
            {
                label2.Text = serialPort.ReadLine();
            }
            catch(TimeoutException)
            {
                MessageBox.Show("Timed out while trying to obtain data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                serialPort.Write("light on");
                label4.Text = "Light is ON";
            }
            else
            {
                serialPort.Write("light off");
                label4.Text = "Light is OFF";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
