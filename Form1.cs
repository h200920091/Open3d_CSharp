using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;


namespace createPoissonMesh
{
    public partial class Form1 : Form
    {
        // import dll
        [DllImport("D:\\zhihao\\Project\\SSI\\CPP\\test\\build\\Release\\TestVisualizer.dll")]
        static extern void createMeshFromCsv(int layer, string inputFile, int x, int y, int z, int r, int g, int b, int noColor);
        [DllImport("D:\\zhihao\\Project\\SSI\\CPP\\test\\build\\Release\\TestVisualizer.dll")]
        static extern void createMeshFromTxt(int layer, string inputFile);
        //
        string inputFile = "";
        private void Form1_Load(object sender, EventArgs e)
        {
            AllocConsole();
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        public Form1()
        {
            InitializeComponent();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Directory.GetCurrentDirectory();
                ofd.Filter = "CSV(*.csv)|*.csv|" + "TXT(*.txt) | *.txt| " + "所有檔案|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    label1.Text = "檔案位置 : " + ofd.FileName;
                    inputFile = ofd.FileName;
                    if (inputFile.Substring(inputFile.Length - 3) == "txt")
                        return;
                    var reader = new StreamReader(File.OpenRead(ofd.FileName));
                    var line = reader.ReadLine();
                    line = line.Replace(" ", "");
                    var values = line.Split(',');
                    List<string> headers = new List<string>();
                    List<string> headers1 = new List<string>();
                    List<string> headers2 = new List<string>();
                    List<string> headers3 = new List<string>();
                    List<string> headers4 = new List<string>();
                    List<string> headers5 = new List<string>();
                    List<string> headers6 = new List<string>();
                    foreach (var item in values)
                    {
                        if (item != "")
                        {
                            headers.Add(item);
                            headers1.Add(item);
                            headers2.Add(item);
                            headers3.Add(item);
                            headers4.Add(item);
                            headers5.Add(item);
                            headers6.Add(item);
                        }

                    }
                    comboBox1.DataSource = headers1;
                    comboBox2.DataSource = headers2;
                    comboBox3.DataSource = headers3;
                    comboBox4.DataSource = headers4;
                    comboBox5.DataSource = headers5;
                    comboBox6.DataSource = headers6;

                    int max = headers.Count;
                    comboBox1.SelectedIndex = comboBox1.FindStringExact(headers[0]);
                    max--;
                    if (max > 0)
                    {
                        comboBox2.SelectedIndex = comboBox2.FindStringExact(headers[1]);
                        max--;
                    }
                    else
                        comboBox2.SelectedIndex = comboBox2.FindStringExact(headers[headers.Count - 1]);
                    if (max > 0)
                    {
                        comboBox3.SelectedIndex = comboBox3.FindStringExact(headers[2]);
                        max--;
                    }
                    else
                        comboBox3.SelectedIndex = comboBox3.FindStringExact(headers[headers.Count - 1]);
                    if (max > 0)
                    {
                        comboBox4.SelectedIndex = comboBox4.FindStringExact(headers[3]);
                        max--;
                    }
                    else
                        comboBox4.SelectedIndex = comboBox4.FindStringExact(headers[headers.Count - 1]);
                    if (max > 0)
                    {
                        comboBox5.SelectedIndex = comboBox5.FindStringExact(headers[4]);
                        max--;
                    }
                    else
                        comboBox5.SelectedIndex = comboBox5.FindStringExact(headers[headers.Count - 1]);
                    if (max > 0)
                    {
                        comboBox6.SelectedIndex = comboBox6.FindStringExact(headers[5]);
                        max--;
                    }
                    else
                        comboBox6.SelectedIndex = comboBox6.FindStringExact(headers[headers.Count - 1]);
                }
            }
        }




        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9') && (e.KeyChar != 8) && (e.KeyChar != 46))
                {
                    e.Handled = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x = comboBox1.SelectedIndex;
            int y = comboBox2.SelectedIndex;
            int z = comboBox3.SelectedIndex;
            int r = comboBox4.SelectedIndex;
            int g = comboBox5.SelectedIndex;
            int b = comboBox6.SelectedIndex;
            bool noColor = checkBox1.Checked;
            int black = 0;
            int layer = 0;
            if (textBox1.Text == "")
            {
                label4.Text = "Status : Please Enter Layer!!";
                return;
            }
            else
            {
                bool convert;
                convert = Int32.TryParse(textBox1.Text, out layer);
                label4.Text = "Status :";
            }

            if (noColor)
                black++;
            if (inputFile == "")
            {
                label4.Text = "Status : Please Load Data!!";
                return;
            }
            if (inputFile.Substring(inputFile.Length - 3) == "txt")
            {
                label4.Text = "Status :";
                createMeshFromTxt(layer, inputFile);
            }
            else if (inputFile.Substring(inputFile.Length - 3) == "csv")
            {
                label4.Text = "Status :";
                createMeshFromCsv(layer, inputFile, x, y, z, r, g, b, black);
            }
            else
                label4.Text = "Status : Unsupported Data Form!!";
        }
    }
}
