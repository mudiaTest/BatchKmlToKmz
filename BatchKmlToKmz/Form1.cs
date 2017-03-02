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
using System.Xml;


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
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                cblMain.Items.Clear();
                if (dlgOpen.FileNames.Count<string>() > 0)
                    lastPath = ""; Path.GetDirectoryName(dlgOpen.FileNames[0]);
                foreach (String file in dlgOpen.FileNames)
                {
                    cblMain.Items.Add(file);
                }
            }
        }

        private void DoKmz() 
        {
            String filePath;
            String dirPath;
            String stIterInfo = "";
            XmlReader reader;
            XmlWriter writer;
            XmlWriterSettings writterSettings;

            String jpgFileName;

            Double dpNorth = 0;
            Double dpSouth = 0;
            Double dpWest = 0;
            Double dpEast = 0;

            int i = 0;
            for (i = 0; i < cblMain.CheckedItems.Count; i++)
            {
                try
                {
                    filePath = (String)cblMain.CheckedItems[i];
                    dirPath = Path.GetDirectoryName(filePath);
                    stIterInfo = i.ToString() + "/" + cblMain.CheckedItems.Count.ToString() + ": " + filePath;

                    jpgFileName = Path.GetFileNameWithoutExtension(filePath) + ".jpg";

                    if (!File.Exists(dirPath + @"\" + jpgFileName))
                    {
                        rtbInfo.AppendText("Brak '" + dirPath + @"\" + jpgFileName + "'" + "\n");
                        rtbInfo.ScrollToCaret();
                        continue;
                    }

                    reader = XmlReader.Create(filePath);
                    reader.Read();
                    while (!reader.EOF) 
                    {
                        if (reader.Name == "north")
                            dpNorth = reader.ReadElementContentAsDouble();
                        else if (reader.Name == "south")
                            dpSouth = reader.ReadElementContentAsDouble();
                        else if (reader.Name == "east")
                            dpEast = reader.ReadElementContentAsDouble();
                        else if (reader.Name == "west")
                            dpWest = reader.ReadElementContentAsDouble();

                        reader.Read();
                    }
                    reader.Close();

                    writterSettings = new XmlWriterSettings()
                    {
                        //Indent = true,
                        //IndentChars = "\t",
                        //NewLineOnAttributes = true,
                        Encoding = Encoding.GetEncoding("iso-8859-1")
                    };
                    writer = XmlWriter.Create(dirPath + "\\doc.kml", writterSettings);
   

                    //writer.WriteRaw("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>");
                    writer.WriteStartElement("kml", "http://earth.google.com/kml/2.1"); 
                        //writer.WriteAttributeString("xmlns", "http://earth.google.com/kml/2.1");

                        writer.WriteStartElement("Document");
                            writer.WriteStartElement("name");
                            writer.WriteValue(jpgFileName);
                            writer.WriteEndElement();
                            writer.WriteStartElement("Region");
                                writer.WriteStartElement("LatLonAltBox");
                                    writer.WriteStartElement("north");
                                    writer.WriteValue(dpNorth.ToString("0.0000000", System.Globalization.CultureInfo.InvariantCulture));
                                    writer.WriteEndElement();
                                    writer.WriteStartElement("south");
                                    writer.WriteValue(dpSouth.ToString("0.0000000", System.Globalization.CultureInfo.InvariantCulture));
                                    writer.WriteEndElement();
                                    writer.WriteStartElement("east");
                                    writer.WriteValue(dpEast.ToString("0.0000000", System.Globalization.CultureInfo.InvariantCulture));
                                    writer.WriteEndElement();
                                    writer.WriteStartElement("west");
                                    writer.WriteValue(dpWest.ToString("0.0000000", System.Globalization.CultureInfo.InvariantCulture));
                                    writer.WriteEndElement();
                                writer.WriteEndElement();
                            writer.WriteEndElement( );

                            writer.WriteStartElement("GroundOverlay");
                                writer.WriteStartElement("name");
                                writer.WriteValue(Path.GetFileNameWithoutExtension(filePath));
                                writer.WriteEndElement();

                                writer.WriteStartElement("drawOrder");
                                writer.WriteValue("50");
                                writer.WriteEndElement();

                                writer.WriteStartElement("Icon");
                                    writer.WriteStartElement("href");
                                    writer.WriteValue(jpgFileName);
                                    writer.WriteEndElement();
                                writer.WriteEndElement();

                                writer.WriteStartElement("LatLonBox");
                                    writer.WriteStartElement("north");
                                    writer.WriteValue(dpNorth.ToString("0.0000000", System.Globalization.CultureInfo.InvariantCulture));
                                    writer.WriteEndElement();
                                    writer.WriteStartElement("south");
                                    writer.WriteValue(dpSouth.ToString("0.0000000", System.Globalization.CultureInfo.InvariantCulture));
                                    writer.WriteEndElement();
                                    writer.WriteStartElement("east");
                                    writer.WriteValue(dpEast.ToString("0.0000000", System.Globalization.CultureInfo.InvariantCulture));
                                    writer.WriteEndElement();
                                    writer.WriteStartElement("west");
                                    writer.WriteValue(dpWest.ToString("0.0000000", System.Globalization.CultureInfo.InvariantCulture));
                                    writer.WriteEndElement();
                                writer.WriteEndElement();


                                writer.WriteStartElement("Region");
                                    writer.WriteStartElement("LatLonAltBox");
                                        writer.WriteStartElement("north");
                                        writer.WriteValue(dpNorth.ToString("0.0000000", System.Globalization.CultureInfo.InvariantCulture));
                                        writer.WriteEndElement();
                                        writer.WriteStartElement("south");
                                        writer.WriteValue(dpSouth.ToString("0.0000000", System.Globalization.CultureInfo.InvariantCulture));
                                        writer.WriteEndElement();
                                        writer.WriteStartElement("east");
                                        writer.WriteValue(dpEast.ToString("0.0000000", System.Globalization.CultureInfo.InvariantCulture));
                                        writer.WriteEndElement();
                                        writer.WriteStartElement("west");
                                        writer.WriteValue(dpWest.ToString("0.0000000", System.Globalization.CultureInfo.InvariantCulture));
                                        writer.WriteEndElement();
                                    writer.WriteEndElement();
                                writer.WriteEndElement();

                                writer.WriteStartElement("color");
                                writer.WriteValue("A1FFFFFF");
                                writer.WriteEndElement();

                        writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.Flush();
                    writer.Close();


                    //Struktura katalogów
                    //if (Directory.Exists(dirPath + @"\images"))
                    //    Directory.Delete(dirPath + @"\images", true);
                    //Directory.CreateDirectory(dirPath + @"\images");
                    //File.Copy(dirPath + @"\" + Path.GetFileNameWithoutExtension(filePath) + ".jpg", dirPath + @"\images\" + Path.GetFileNameWithoutExtension(filePath) + ".jpg");
                    //komprezja
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.CreateNoWindow = true;
                    startInfo.UseShellExecute = false;
                    startInfo.FileName = "zip.exe";
                    startInfo.WindowStyle = ProcessWindowStyle.Normal;
                    startInfo.Arguments = "-0 -r -j " + dirPath + "\\" + Path.GetFileNameWithoutExtension(filePath) + ".kmz " + Path.Combine(dirPath, jpgFileName) + " " + Path.Combine(dirPath,"doc.kml");
                    //startInfo.Arguments = " -0 -r " + dirPath + "\\" + Path.GetFileNameWithoutExtension(filePath) + ".kmz " + dirPath + "\\images " + dirPath + "\\doc.kml";
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }
                    //sprzątanie i komunikaty
                    //Directory.Delete(dirPath + @"\images", true);
                    File.Delete(dirPath + "\\doc.kml");
                    rtbAkcja.AppendText("Wykonano " + stIterInfo + "\n");
                    rtbAkcja.ScrollToCaret();
                    //System.Threading.Thread.Sleep(1000);
                    this.Refresh();
                }
                catch (Exception ex)
                {
                    rtbInfo.AppendText("Bład " + stIterInfo + "; " + ex.Message + "\n");
                    rtbInfo.ScrollToCaret();
                }
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            DoKmz();
        }
    }
}
