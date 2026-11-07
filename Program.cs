using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CabuhatExer2
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FrmOpenTextFile openTextFileForm = new FrmOpenTextFile();
            FrmStudentRecord studentRecordForm = new FrmStudentRecord();

            studentRecordForm.Show(openTextFileForm);
            Application.Run(openTextFileForm);
        }
    }
}
