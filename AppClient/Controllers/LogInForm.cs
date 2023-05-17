using MPPCSharp.Models;
using MPPCSharp.Services;
using System;
using System.Windows.Forms;

namespace MPPCSharp.Forms
{
    public partial class LogInForm : Form
    {

        private IAppServices service;
        private MainForm mainForm;

        public LogInForm(IAppServices service, MainForm appObserver)
        {
            InitializeComponent();
            this.service = service;
            this.mainForm = appObserver;
        }

        private void logInClick(object sender, EventArgs e)
        {
            Employee employee = new Employee(usernameTextBox.Text, passwordTextBox.Text);
            service.login(employee, mainForm);
            mainForm.setSetup(service, employee);
            this.Hide();
            mainForm.ShowDialog();

        }

        private void registerClick(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void LogInForm_Load(object sender, EventArgs e)
        {

        }
    }
}
