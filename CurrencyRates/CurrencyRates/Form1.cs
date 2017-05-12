using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CurrencyRates
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        List<Rates> PairName = new List<Rates>();
        public Form1()
        {
            InitializeComponent();
            Console.WriteLine("Hi");
            LoadRates();
            StartAsyncTimedWork();
        }
        string inputPR;
        double inputTR;
        private void StartAsyncTimedWork()
        {
            Console.WriteLine("Hello");
            timer.Interval = 10000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(timer_Tick), sender, e);
            }
            else
            {
                lock (timer)
                {
                    if (this.timer.Enabled)
                    {
                        timer.Stop();
                        LoadRates();
                        if (checkBox1.Checked)
                        {
                            recRate();
                            recTarget();
                        }
                        timer.Start();
                    }
                }
            }
        }


        private void LoadRates()
        {

            String URLString = "http://rates.fxcm.com/RatesXML";
            XmlDocument doc = new XmlDocument();
            doc.Load(URLString);
            PairName.Clear();
            listBox1.Items.Clear();
            foreach (XmlNode node in doc.DocumentElement)
            {
                string pairrate = node.Attributes[0].Value;
                double bid = double.Parse(node["Bid"].InnerText);
                double ask = double.Parse(node["Ask"].InnerText);
                double high = double.Parse(node["High"].InnerText);
                double low = double.Parse(node["Low"].InnerText);
                int direction = int.Parse(node["Direction"].InnerText);
                string last = node["Last"].InnerText;

                //listBox1.Items.Add(new Rates(pairrate, bid, ask, high, low, direction, last));
                PairName.Add(new Rates(pairrate, bid, ask, high, low, direction, last));
                
            }
            foreach (var pn in PairName)
            {
                listBox1.Items.Add(pn.PairRate);
            }
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                //propertyGrid1.SelectedObject = listBox1.SelectedItem;
                propertyGrid1.SelectedObject = PairName[listBox1.SelectedIndex];
            }
        }
        private void recRate()
        {
            inputPR = rates.Text;
            if (inputPR != null)
            {
                textBox1.Text = "Please enter a valid pair displayed on right. ";
                // listbox1.items
                foreach (var item in PairName)
                {
                    //textBox1.Text += item.ToString();
                    //textBox1.Text += item.Bid;
                    if (inputPR == item.PairRate.ToString())
                    {
                        textBox1.Text = "Found";


                        //propertyGrid1.;
                    }


                }
                if (textBox1.Text != "Found" || textBox1.Text == null)
                {
                    inputPR = null;
                }

            }
        }
        private void getRate(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                recRate();
            }
        }
        private void recTarget()
        {
            double.TryParse(target.Text, out inputTR);
            
            if (inputPR == null || inputTR == 0.0)
            {
                textBox1.Text = "Please enter a valid pair name and then a numerical target. ";
            }
            else
            {
                foreach (var item in PairName)
                {
                    if (inputPR == item.PairRate.ToString())
                    {

                        if (inputTR <= item.Bid)
                        {
                            textBox1.Text = "Current rate is equal to or greater than target rate of: " + inputTR;


                            //propertyGrid1.;
                        }
                        else if (inputTR >= item.Bid)
                        {
                            textBox1.Text = "Target rate not reached. ";
                        }

                    }
                }
            }
        }
        private void getTarget(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                recTarget();

                //string stringTR;
                //stringTR = textBox1.Text;

            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
