using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ShareFileTest1.View
{
    public partial class frmWhatsApp : Form
    {
        bool sideBar_Expand = true;
        bool FlagprogressMove = true;
        int ProgressMove = 2;
        int date =0;
        public frmWhatsApp()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (sideBar_Expand)
            {
                pnlMenu.Width -= 10;
                if (pnlMenu.Width == pnlMenu.MinimumSize.Width)
                {
                    sideBar_Expand = false;
                    tmrMenu.Stop();
                }
            }
            else
            {
                pnlMenu.Width += 10;
                if (pnlMenu.Width == pnlMenu.MaximumSize.Width)
                {
                    sideBar_Expand = true;
                    tmrMenu.Stop();
                }
            }
        }

        private void pbMenu_Click(object sender, EventArgs e)
        {
            tmrMenu.Start();
            lblDescriptions.Text = "LCS WORKSPACE";

        }

        private void frmWhatsApp_Load(object sender, EventArgs e)
        {
            pnlNotificationSubMenu.Visible = false;
            pnlProcessing.Visible = false;
            lblDescriptions.Text = "LCS WORKSPACE";

        }
        private void HideSubMenuPanel()
        {
            if(pnlNotificationSubMenu.Visible==true)
            {
                pnlNotificationSubMenu.Visible = false;
            }
        }

        private void ShowSubMenuPanel(Panel MenuPanle)
        {
            if (MenuPanle.Visible == false)
            {
                HideSubMenuPanel();
                MenuPanle.Visible = true;
            }
            else
            {
                MenuPanle.Visible = false;
            }

        }

        private void pbNotification_Click(object sender, EventArgs e)
        {
            ChangePanelStatus();
            lblDescriptions.Text = "NOTIFICATIONS";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                btnMaximize.Text = "[]";
            }
            else if(this.WindowState==FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                btnMaximize.Text = "[ ]";
            }
            
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pbChats_Click(object sender, EventArgs e)
        {
            ChangePanelStatus();
            lblDescriptions.Text = "CHATS";
            pnlChatBg.Visible = true;
        }

        private void lblMenu_Click(object sender, EventArgs e)
        {
            ChangePanelStatus();
            lblDescriptions.Text = "LCS WORKSPACE";

        }

        private void pbStatus_Click(object sender, EventArgs e)
        {
            ChangePanelStatus();
            lblDescriptions.Text = "STATUS";
        }

        private void pbSelectProfile_Click(object sender, EventArgs e)
        {
            ChangePanelStatus();
            lblDescriptions.Text = "PROFILE";
        }

        private void pbSettings_Click(object sender, EventArgs e)
        {
            ChangePanelStatus();
            lblDescriptions.Text = "SETTINGS";
        }

        private void btnGetUpdate_Click(object sender, EventArgs e)
        {

        }

        private void pbCheckUpdates_Click(object sender, EventArgs e)
        {
            ShowSubMenuPanel(pnlNotificationSubMenu); //Need to Comment
            pnlProcessing.Visible = true;
            tmrProgress.Start();
            //CheckForUpdates();
        }
        private void ChangePanelStatus()
        {
            lblDescriptions.Visible = true;
            pnlProcessing.Visible = false;
        }

        private void CheckForUpdates()
        {
            try
            {
                NetworkShare.DisconnectFromShare(Global.ServerPath, true); //Disconnect in case we are currently connected with our credentials;
                NetworkShare.ConnectToShare(Global.ServerPath, Global.UserName, Global.Password); //Connect with the new credentials

                Global.SourceFile = @"\" + Global.ServerPath + "\\" + Global.SourceFileName + ".exe";
                if (File.Exists(Global.SourceFile))
                {
                    pnlProcessing.Visible = false;
                    tmrProgress.Stop();
                    lblDescriptions.Visible = true;
                    lblDescriptions.Text = "UPDATES AVAILABLE";
                    ShowSubMenuPanel(pnlNotificationSubMenu);
                }
                else
                {
                    pnlProcessing.Visible = false;
                    tmrProgress.Stop();
                    lblDescriptions.Visible = true;
                    lblDescriptions.Text = " NO UPDATES!";
                }

            }
            catch(Exception ex)
            {
                tmrProgress.Stop();
                pnlProcessing.Visible = false;
                lblDescriptions.Visible = true;
                lblDescriptions.Text = " NO UPDATES!";
            }
           
        }

        private void btnGetUpdate_Click_1(object sender, EventArgs e)
        {
            pnlProcessing.Visible = true;
            tmrProgress.Start();
            //SharedFileStorage();
        }
        private void SharedFileStorage()
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
                tmrProgress.Stop();
                pnlProcessing.Visible = false;
                lblDescriptions.Visible = true;
                lblDescriptions.Text = "UPDATE SUCCESSFULL";
                MessageBox.Show("The application will be restarted");
                Application.Restart();
            }
            catch (Exception ex)
            {

            }
        }

        private void tmrProgress_Tick(object sender, EventArgs e)
        {
            if (FlagprogressMove)
            {
                pnlSlideProgress.Left += 2;
                if (pnlSlideProgress.Left > 350)
                {
                    pnlSlideProgress.Left = 0;
                }
                if (pnlSlideProgress.Left > 0)
                {
                    ProgressMove = 2;
                }
            }

        }
        
    }
}
