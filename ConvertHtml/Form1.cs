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

namespace ConvertHtml
{
    public partial class Form1 : Form
    {
        private StreamWriter sw;
        //private string sourcePath = @"c:\scratch\esp8266\";
        public Form1()
        {
            InitializeComponent();

           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "seleziona directory sorgente";
            if (folderDialog.ShowDialog() != DialogResult.OK)
            {

                    // folderDialog.SelectedPath -- your result
                    return;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            sw = new StreamWriter(/*"c:\\scratch\\html.h"*/ofd.FileName, false);


            DirectoryInfo di = new DirectoryInfo(/*sourcePath*/folderDialog.SelectedPath);
            foreach (var fi in di.GetFiles("*.html"))
            {
                Console.WriteLine(fi.Name);
                convertFile(fi.Name,folderDialog.SelectedPath);
            }

            foreach (var fi in di.GetFiles("*.css"))
            {
                Console.WriteLine(fi.Name);
                convertFile(fi.Name,folderDialog.SelectedPath);
            }


            foreach (var fi in di.GetFiles("*.js"))
            {
                Console.WriteLine(fi.Name);
                convertFile(fi.Name, folderDialog.SelectedPath);
            }

            sw.Close();

            // Suspend the screen.
            Console.ReadLine();
        }

        private void convertFile(string filename, String path)
        {
            StreamReader file = new StreamReader(path + "\\" + filename);
            string name = filename.Replace(".", "_");


            sw.WriteLine("/* " + name + "*/");
            sw.WriteLine("const char " + name + "[] PROGMEM = {");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                line = line.Replace("\"", "\\\"");
                line = "\"" + line + "\""/* + Environment.NewLine*/;
                sw.WriteLine(line);
            }
            file.Close();

            sw.WriteLine("};" + Environment.NewLine);
        }
    }
}
