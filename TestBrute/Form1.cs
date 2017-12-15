using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestBrute
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> mybase = new List<string>();
        List<string> results = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Текстовый файл(*.txt)|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    loadBase(dialog.FileName);
                    button2.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: Файл недоступен для открытия: " + ex.Message);
                }
            }
        }

        void loadBase(string location)
        {
            var lines = File.ReadLines(location);
            mybase.Clear();
            results.Clear();
            foreach (var item in lines)
            {
                mybase.Add(item);
            }
            update_ui();
        }

        void update_ui()
        {
            label2.Text = mybase.Count+"";
            label4.Text = results.Count + "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            results.Clear();
            update_ui();
            for (int i = 0; i < mybase.Count; i++)
            {
                check(mybase[i]);
            }
        }

        void check(string s)
        {
            string name = s.Split(':')[0];
            string pass = s.Split(':')[1];
            string response=request("http://t70734o6.bget.ru/testcheck.php?login=" + name + "&pass=" + pass);
            if (response.Contains("Вы вошли")) {
                results.Add(s);
                update_ui();
            }
        }

        private string request(string Url)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(Url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            return Out;
        }
    }
}
