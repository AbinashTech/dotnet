using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShareFileTest1
{
    public static class Global
    {
        public static string UserName = "swathi";
        public static string Password = "lcsswd";
        public static string IpAddress = "10.2.238.1";
        public static string ServerPath = "\\10.2.238.1\\DATA1\\TEST1";
        public static string DestinationPath = string.Empty;
        public static string SourceFileName = "ShareFileTest1";
        public static string SourceFile = string.Empty;
        public static string BackUpFilePath = Application.StartupPath + "\\EXEBACKUP\\";
        public static string BackUpFileName = BackUpFilePath + SourceFileName + ".bak";
        //\10.2.238.1\DATA1\TEST
    }
}
