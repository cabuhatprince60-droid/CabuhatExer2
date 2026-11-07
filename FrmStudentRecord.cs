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
    public partial class FrmStudentRecord : Form
    {
        private string path = "";
        private TextBox txtStudentId;
        private TextBox txtStudentName;

        public FrmStudentRecord()
        {
            InitializeComponent();
            CreateRegistrationFields();
            this.FormClosing += FrmStudentRecord_FormClosing;
        }

        private void CreateRegistrationFields()
        {
            ClientSize = new Size(392, 340);

            Label idLabel = new Label
            {
                AutoSize = true,
                Location = new Point(36, 270),
                Text = "Student ID:"
            };

            txtStudentId = new TextBox
            {
                Location = new Point(110, 267),
                Size = new Size(105, 20)
            };

            Label nameLabel = new Label
            {
                AutoSize = true,
                Location = new Point(36, 300),
                Text = "Name:"
            };

            txtStudentName = new TextBox
            {
                Location = new Point(110, 297),
                Size = new Size(245, 20)
            };

            Controls.Add(idLabel);
            Controls.Add(txtStudentId);
            Controls.Add(nameLabel);
            Controls.Add(txtStudentName);
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string studentId = txtStudentId.Text.Trim();
            string studentName = txtStudentName.Text.Trim();

            if (studentId.Length == 0 || studentName.Length == 0)
            {
                MessageBox.Show("Enter both the student ID and student name.",
                    "Student Record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStudentId.Focus();
                return;
            }

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Title = "Choose Student Records File";
                    saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialog.DefaultExt = "txt";
                    saveFileDialog.AddExtension = true;

                    if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    path = saveFileDialog.FileName;
                }
            }

            try
            {
                string record = studentId + " - " + studentName;
                File.AppendAllText(path, record + Environment.NewLine);
                lvShowText.Items.Add(record);
                txtStudentId.Clear();
                txtStudentName.Clear();

                MessageBox.Show("Student registered and saved successfully.",
                    "Register", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save the student: " + ex.Message,
                    "Register Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmStudentRecord_FormClosing(
            object sender, FormClosingEventArgs e)
        {
            if (this.Owner != null && !this.Owner.IsDisposed)
            {
                this.Owner.Show();
                this.Owner.Activate();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
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
                        lvShowText.Items.Add(_getText);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to find or read file: " + ex.Message,
                    "File Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (lvShowText.Items.Count == 0)
            {
                MessageBox.Show("There is no student record to upload.",
                    "Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save Student Record";
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    File.WriteAllLines(saveFileDialog.FileName,
                        lvShowText.Items.Cast<ListViewItem>().Select(item => item.Text));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to upload the record: " + ex.Message,
                        "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            MessageBox.Show(
                "Successfully uploaded the student record!",
                "Upload",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}
