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
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;


namespace MultiMonitorURLDisplayTool
{
    public partial class Form1 : Form
    {
        MonitorManipulation MonitorManipulation = new MonitorManipulation();

        DataSet MonitorDataSet = new DataSets.MonitorDataSet();
        DataSet MonitorInstructionProvider = new DataSet("MonitorInstructionProvider");

        dynamic cs = Properties.Settings.Default.ConfigURLConnectionString;
   
        List<Thread> threadlist = new List<Thread>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //First thing, let's get the number of monitors....
            {
                MonitorManipulation.numMonitors = System.Windows.Forms.SystemInformation.MonitorCount;
                txtNumMon.Text = Convert.ToString(MonitorManipulation.numMonitors);

                //Now gather and populate the monitor stats....
                for (int monitorID = 0; monitorID <= MonitorManipulation.numMonitors - 1; monitorID++)
                {
                    string[] resolution = null;
                    string[] location = null;
                    int monitorx = 0;
                    int monitory = 0;
                    int monitorlocx = 0;
                    int monitorlocy = 0;

                    resolution = MonitorManipulation.GetScreenResolution(monitorID).Split('x');
                    monitorx = Convert.ToInt32(resolution.GetValue(0));
                    monitory = Convert.ToInt32(resolution.GetValue(1));

                    location = MonitorManipulation.GetScreenLocation(monitorID).Split('x');
                    monitorlocx = Convert.ToInt32(location.GetValue(0));
                    monitorlocy = Convert.ToInt32(location.GetValue(1));
                   
                    DataRow newMonitorRow = MonitorDataSet.Tables["datatableMonitor"].NewRow();
                    
                    newMonitorRow["MonitorID"] = Convert.ToString(monitorID);
                    newMonitorRow["MonitorX"] = Convert.ToString(monitorx);
                    newMonitorRow["MonitorY"] = Convert.ToString(monitory);
                    newMonitorRow["MonitorLocX"] = Convert.ToString(monitorlocx);
                    newMonitorRow["MonitorLocY"] = Convert.ToString(monitorlocy);

                    //Now add the row to the dataTable
                    
                    MonitorDataSet.Tables["datatableMonitor"].Rows.Add(newMonitorRow);
                }
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            //For each monitor that is connected, make a datatable of the action items for that monitors display

            {
                foreach (DataRow monitor in MonitorDataSet.Tables["datatableMonitor"].Rows)
                {
                    //We will pass the monitor data row and a datatable of just that monitors action items
                    SqlConnection SqlConnection = new SqlConnection(cs);
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader = default(SqlDataReader);

                    cmd.CommandText = "[getMonitorActionItems]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@monitorID", monitor["MonitorID"].ToString());
                    cmd.Connection = SqlConnection;

                    SqlConnection.Open();

                    reader = cmd.ExecuteReader();
                    // Data is accessible through the DataReader object here.

                    //Let's declare a temporary datatable
                    DataTable tempTable = new DataTable(monitor["MonitorID"].ToString() + "actionlist");
                    tempTable.Columns.Add("Enabled");
                    tempTable.Columns.Add("Title");
                    tempTable.Columns.Add("URL");
                    tempTable.Columns.Add("MonitorID");
                    tempTable.Columns.Add("ShowDuration");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tempTable.Rows.Add(reader[1], reader[2], reader[3], reader[4], reader[5]);
                        }
                        if (tempTable.Rows.Count > 0)
                        {
                            MonitorInstructionProvider.Tables.Add(tempTable);
                        }
                    }

                    SqlConnection.Close();
                }
                //Now, for debug, let's do through the MonitorInstructionProvider to see what tables have been made.
                foreach (DataTable table in MonitorInstructionProvider.Tables)
                {
                    
                    //Look at each one's first entry and ID the monitor....
                    int tempMonitorID = Convert.ToInt32(table.Rows[0]["MonitorID"]);
                    Thread.Sleep(1000);
                    //DataRow monitorConfig = MonitorDataSet.Tables["datatableMonitor"].Select[string.Format("MonitorID = '{0}'", tempMonitorID)].First;
                    DataRow monitorConfig = MonitorDataSet.Tables["dataTableMonitor"].Select(string.Format("MonitorID = '{0}'", tempMonitorID)).First();
                    Thread t1 = new Thread(() => NewForm(table, monitorConfig));
                    t1.SetApartmentState(ApartmentState.STA);
                    t1.Start();
                    threadlist.Add(t1);
                }
            }
        }

        public void NewForm(DataTable datatable, DataRow monitorConfig)
        {
            Form blah = new Browser(datatable,monitorConfig);
            blah.ShowDialog();
        }

        private void btnKillThreads_Click(object sender, EventArgs e)
        {
            List<Thread> threadstokill = new List<Thread>();
            foreach (Thread item in threadlist)
            {
                threadstokill.Add(item);
            }
            foreach (Thread item in threadstokill)
            {
                item.Abort();
            }
            threadstokill.Clear();
            threadlist.Clear();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            btnKillThreads_Click(sender, e);

            Application.Exit();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            configureURL configure = new configureURL();
            configure.Show();
        }
    }
}
