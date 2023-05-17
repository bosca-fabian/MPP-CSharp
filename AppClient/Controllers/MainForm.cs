using System;
using System.Windows.Forms;
using MPPCSharp.Models;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using MPPCSharp.Utils;
using MPPCSharp.Services;

namespace MPPCSharp.Forms
{

    public partial class MainForm : Form, IAppObserver
    {
        
        private IAppServices appService;

        private Employee loggedEmployee;
        public MainForm(IAppServices service, Employee loggedEmployee)
        {
            this.appService = service;
            this.loggedEmployee = loggedEmployee;

            InitializeComponent();
            //childTableView = new DataGridView();
            handleChildren();
            trialsComboBox.DataSource = this.appService.getAllTrials();
            ageBracketsComboBox.DataSource = Enum.GetValues(typeof(AgeBrackets));
        }       
        public MainForm() { }
        public void setSetup(IAppServices appService, Employee loggedEmployee)
        {
            this.appService = appService;
            this.loggedEmployee = loggedEmployee;
            InitializeComponent();
            //childTableView = new DataGridView();
            handleChildren();
            trialsComboBox.DataSource = this.appService.getAllTrials();
            ageBracketsComboBox.DataSource = Enum.GetValues(typeof(AgeBrackets));
        }
        private void handleChildren()
        {
            List<Child> children = this.appService.getAllChildren();
            childTableDataGridView.DataSource = children;
            childTableDataGridView.Refresh();
        }

        //private void MainForm_Load(object sender, EventArgs e)
        //{
        //    List<Child> children = this.appService.readAllChildren();
        //    childTableView.DataSource = children;
            
        //}

        private void childTableView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {

        }

        private void onFilterButtonClick(object sender, EventArgs e)
        {
         List<Child> allChildren = this.appService.getAllChildren();
            allChildren.RemoveAll(trialFilter);
            allChildren.RemoveAll(ageFilter);
            childTableDataGridView.DataSource = allChildren;
        }

        private void onResetFilterButtonClick(object sender, EventArgs e)
        {
            childTableDataGridView.DataSource = this.appService.getAllChildren();
        }

        private void onAddChildToCompetitionButtonClick(object sender, EventArgs e)
        {
            AddChildToTrialForm addChildToTrialForm = new AddChildToTrialForm(this.appService);
            addChildToTrialForm.ShowDialog();

        }

        private void onDisconnectClick(object sender, EventArgs e)
        {
            appService.logout(loggedEmployee);
        }

        public void update()
        {
            handleChildren();
        }

        private bool trialFilter(Child chosenChild)
        {
            Trial selectedTrial = (Trial)trialsComboBox.SelectedItem;
            
            if(!this.appService.getChildTrials(chosenChild).Contains(selectedTrial.GetGuid()))
            {
                return true;
            }

            return false;
        }

        private bool ageFilter(Child chosenChild)
        {
            AgeBrackets selectedBracket = (AgeBrackets)ageBracketsComboBox.SelectedItem;
            switch (selectedBracket)
            {
                case AgeBrackets.SIX_EIGHT:
                    {
                        if (chosenChild.age < 6 || chosenChild.age > 8)
                            return true;
                        break;
                    }
                case AgeBrackets.NINE_ELEVEN:
                    {
                        if (chosenChild.age < 9 || chosenChild.age > 11)
                            return true;
                        break;
                    }
                case AgeBrackets.TWELVE_FIFTEEN:
                    {
                        if (chosenChild.age < 12 || chosenChild.age > 15)
                            return true;
                        break;
                    }
                default:
                    return false;
            }
            return false;
        }

        public void ChildReceived(Child child, List<Trial> trials)
        {
            //List<Child> currentChildren = new List<Child>();
            //currentChildren = (List<Child>)childTableDataGridView.DataSource;
            //currentChildren.Add(child);
            //childTableDataGridView.DataSource = currentChildren;
            childTableDataGridView.BeginInvoke((Action)delegate { handleChildren(); });
        }
    }
}
