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

namespace MultiMonitorURLDisplayTool
{
    public partial class configureURL : Form
    {
        public configureURL()
        {
            InitializeComponent();
        }

        private void masterConfigBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.masterConfigBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.configURLDataSet);

        }

        private void configureURL_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'configURLDataSet.MasterConfig' table. You can move, or remove it, as needed.
            this.masterConfigTableAdapter.Fill(this.configURLDataSet.MasterConfig);

        }
    }
}
