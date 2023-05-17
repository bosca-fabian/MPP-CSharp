using MPPCSharp.Models;
using MPPCSharp.Services;
using MPPCSharp.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPCSharp.Forms
{
    public partial class AddChildToTrialForm : Form
    {

        private IAppServices service;
        public AddChildToTrialForm()
        {
            InitializeComponent();
        }

        public AddChildToTrialForm(IAppServices service)
        {
            this.service = service;
            InitializeComponent();
            //childTableView = new DataGridView();
            handleTrials();
           
        }

        private void handleTrials()
        {
            List<Trial> trials = this.service.getAllTrials();
            trialTableDataGridView.DataSource = trials;
        }

        private void AddChildToTrialForm_Load(object sender, EventArgs e)
        {

        }

        private void addChild(object sender, EventArgs e)
        {
            string childFirstName = firstNameTextBox.Text;
            string childLastName = lastNameTextBox.Text;
            string age = ageTextBox.Text;
            List<Trial> wantedTrials = new List<Trial>();
            Int32 selectedRowCount =
            trialTableDataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {

                for (int i = 0; i < selectedRowCount; i++)
                {
                    wantedTrials.Add((Trial)trialTableDataGridView.SelectedRows[i].DataBoundItem);
                }

            }
            try
            {
                Child child = new Child(childFirstName, childLastName, Int32.Parse(age));
                this.service.sendChild(child, wantedTrials);
                //errorLabel.Text = "Child added with succes";

            }
            catch(Exception ex) 
            {
                errorLabel.Text = ex.Message;
            }
        }

    }
}
