//Copyright 2014 Webtrends Inc.
//Authored by:  Warren Bebout
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using WebBrowserControlDialogs;
using System.Net;
using System.Runtime.InteropServices;

namespace MultiMonitorURLDisplayTool
{
    public partial class Browser : Form
    {
        const int FEATURE_DISABLE_NAVIGATION_SOUNDS = 21;
        const int SET_FEATURE_ON_PROCESS = 0x00000002;

        [DllImport("urlmon.dll")]
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        static extern int CoInternetSetFeatureEnabled(
            int FeatureEntry,
            [MarshalAs(UnmanagedType.U4)] int dwFlags,
            bool fEnable);

        private DataTable instructionTable;

        private DataRow monitorCfg;

        private int recordNumber = 0;
        System.Timers.Timer timer = new System.Timers.Timer();

        int duration;

        private WindowsInterop WindowsInterop = new WindowsInterop();

        public Browser(DataTable tempinstructionTable, DataRow monitorConfig)
        {
            // Tell the WidowsInterop to Hook messages
            
            WindowsInterop.Hook();

            InitializeComponent();
            
            instructionTable = tempinstructionTable;
            
            monitorCfg = monitorConfig;
            
            timer.Elapsed += new ElapsedEventHandler(Browser_Shown);

            WindowsInterop.SecurityAlertDialogWillBeShown += new GenericDelegate<Boolean, Boolean>(this.WindowsInterop_SecurityAlertDialogWillBeShown);

            WindowsInterop.ConnectToDialogWillBeShown += new GenericDelegate<String, String, Boolean>(this.WindowsInterop_ConnectToDialogWillBeShown);

            // Subscribe to the WebBrowser Control's DocumentCompleted event
            this.WebBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.WebBrowser1_DocumentCompleted);

            //MessageBox.Show(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
            
            // Tell the WidowsInterop to Unhook
            //WindowsInterop.Unhook();
        }

        public void New(DataTable tempinstructionTable, DataRow monitorConfig)
        {
            InitializeComponent();

            instructionTable = tempinstructionTable;

            monitorCfg = monitorConfig;

            timer.Elapsed += new ElapsedEventHandler(Browser_Shown);
        }

        static void DisableClickSounds()
        {
            CoInternetSetFeatureEnabled(
                FEATURE_DISABLE_NAVIGATION_SOUNDS,
                SET_FEATURE_ON_PROCESS,
                true);
        }

        private Boolean WindowsInterop_SecurityAlertDialogWillBeShown(Boolean blnIsSSLDialog)
        {
            // Return true to ignore and not show the 
            // "Security Alert" dialog to the user
            return true;
        }

        private Boolean WindowsInterop_ConnectToDialogWillBeShown(ref String sUsername, ref String sPassword)
        {
            // (Fill in the blanks in order to be able 
            // to return the appropriate Username and Password)
            sUsername = "";
            sPassword = "";
        
            // Return true to auto populate credentials and not 
            // show the "Connect To ..." dialog to the user
            return true;
        }

        private void Handler()
        {
            //Let's see if we have a row that lines up....
            if (recordNumber < (instructionTable.Rows.Count - 1))
            {
                //Let's get this record ready for use.
                //BrowseToURL(recordNumber)
                recordNumber = recordNumber + 1;
            }
            else
            {
                recordNumber = 0;
                //BrowseToURL(recordNumber)
            }
        }

        private void BrowseToURL(int recordNum)
        {
            bool enabledBit = Convert.ToBoolean(instructionTable.Rows[recordNumber]["Enabled"]);
            string title = instructionTable.Rows[recordNumber]["Title"].ToString();
            string url = instructionTable.Rows[recordNumber]["URL"].ToString();
            duration = Convert.ToInt32(instructionTable.Rows[recordNumber]["ShowDuration"]);

            this.WebBrowser1.Navigate(new Uri(url));
            
        }

        private void Browser_Load(object sender, EventArgs e)
        {
            //For Each instruction As DataRow In instructionTable.Rows
            //    TextBox1.Text = instruction.Item("Title").ToString
            //Next
            timer.Stop();

            //The location we want to be is held in the position attributes of the monitor config
            int formXLocation = Convert.ToInt32(monitorCfg["MonitorLocX"]);
            int formYLocation = Convert.ToInt32(monitorCfg["MonitorLocY"]);
            int formWidth = Convert.ToInt32(monitorCfg["MonitorX"]);
            int formHeight = Convert.ToInt32(monitorCfg["MonitorY"]);

            //Let's move the form to the above location.
            this.Location = new Point(formXLocation, formYLocation);
            this.Size = new Size(formWidth, formHeight);

            this.WebBrowser1.Size = new Size(this.Size.Width, this.Size.Height);

            DataGridView1.DataSource = instructionTable;
        }

        private void Browser_Shown(object sender, EventArgs e)
        {
            //MsgBox(recordNumber)
            //ServicePointManager.ServerCertificateValidationCallback = New RemoteCertificateValidationCallback(AddressOf AcceptAllCertifications)
            //Disable click sounds
            DisableClickSounds();
            BrowseToURL(recordNumber);
            Handler();
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //Do nothing
            timer.Interval = duration * 1000;
            timer.Start();
        }
    }
}
