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

namespace CabuhatExer2
{
    public partial class FrmOpenTextFile : Form
    {
        private string path = "";
        public FrmOpenTextFile()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            DisplayToList();
        }

        private void DisplayToList()
        {
            try
            {
                openFileDialog1.InitialDirectory = @"C:\";
                openFileDialog1.Title = "Browse Text Files";
                openFileDialog1.DefaultExt = "txt";
                openFileDialog1.Filter =
                    "txt files (*.txt)|*.txt|All files (*.*)|*.*";

                if (openFileDialog1.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                path = openFileDialog1.FileName;

                lvShowText.Items.Clear();

                using (StreamReader streamReader = File.OpenText(path))
                {
                    string _getText = "";

                    while ((_getText = streamReader.ReadLine()) != null)
                    {
                        Console.WriteLine(_getText);
                        lvShowText.Items.Add(_getText);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to open file: " + ex.Message,
                    "File Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}