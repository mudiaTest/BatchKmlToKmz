using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        String lastPath;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cblMain.Items.Count; i++)
            {
                cblMain.SetItemChecked(i, true);
            }
 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cblMain.Items.Count; i++)
            {
                cblMain.SetItemChecked(i, false);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dlgOpen.InitialDirectory = lastPath;
            dlgOpen.DefaultExt = ".map";
            dlgOpen.ShowDialog();
            if (dlgOpen.FileNames.Count<string>() > 0)
                lastPath = ""; Path.GetDirectoryName(dlgOpen.FileNames[0]);
            foreach (String file in dlgOpen.FileNames)
            {
                cblMain.Items.Add(file);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String filePath;
            String dirPath;
            for (int i = 0; i < cblMain.CheckedItems.Count; i++)
            {
                filePath = (String)cblMain.CheckedItems[i];
                dirPath = Path.GetDirectoryName(filePath);
                if (Directory.Exists(dirPath + @"\images"))
                    Directory.Delete(dirPath + @"\images", true);
                Directory.CreateDirectory(dirPath+@"\images");
                File.Copy(dirPath + @"\" + Path.GetFileNameWithoutExtension(filePath) + ".jpg", dirPath + @"\images\" + Path.GetFileNameWithoutExtension(filePath) + ".jpg");

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.FileName = "zip.exe";
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.Arguments = "-0 -r \"" + dirPath + "\\" + Path.GetFileNameWithoutExtension(filePath) + ".kmz\" " + "images " + Path.GetFileNameWithoutExtension(filePath) + ".kml";
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }


                Directory.Delete(dirPath + @"\images", true);
            }
        }
    }
}
