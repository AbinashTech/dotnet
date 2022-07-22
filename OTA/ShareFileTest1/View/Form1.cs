using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace ShareFileTest1
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void SharedFileStorage1()
        {
            try
            {
                NetworkShare.DisconnectFromShare(Global.ServerPath, true); //Disconnect in case we are currently connected with our credentials;
                NetworkShare.ConnectToShare(Global.ServerPath, Global.UserName, Global.Password); //Connect with the new credentials
               
                Global.SourceFile = @"\" + Global.ServerPath + "\\" + Global.SourceFileName + ".exe";

                Global.DestinationPath = Application.StartupPath + "\\" + Global.SourceFileName + ".exe";

                if (File.Exists(Global.DestinationPath))
                {
                    //TO Move the existing .exe file as backup file (.bak)
                   
                    if (File.Exists(Global.BackUpFileName)) 
                    {
                        File.Delete(Global.BackUpFileName);
                        Directory.CreateDirectory(Global.BackUpFilePath);
                        File.Move(Global.DestinationPath, Global.BackUpFileName);
                        File.Copy(Global.SourceFile, Global.DestinationPath);
                    }
                    else
                    {
                        Directory.CreateDirectory(Global.BackUpFilePath);
                        File.Move(Global.DestinationPath, Global.BackUpFileName);
                        File.Copy(Global.SourceFile, Global.DestinationPath);
                    }

                }
                else
                {
                    File.Copy(Global.SourceFile, Global.DestinationPath);
                }
                NetworkShare.DisconnectFromShare(Global.ServerPath, false); //Disconnect from the server.                
                MessageBox.Show("The application will be restarted");
                Application.Restart();
            }
            catch (Exception ex)
            {
                
            }
        }

        private void lblCamera_Click(object sender, EventArgs e)
        {
            SharedFileStorage1();
            
        }

    }
}
