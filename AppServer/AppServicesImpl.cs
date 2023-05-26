using MPPCSharp.Models;
using MPPCSharp.Repository;
using System;
using MPPCSharp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using AppPersistence.Interfaces;

namespace AppServer
{
    public class AppServicesImpl : IAppServices
    {

        private IEmployeeRepository employeeRepository;

        private Repository<Child> childRepository;

        private IChildTrialRepository childTrialRepository;

        private Repository<Trial> trialRepository;

        private readonly IDictionary<Guid, IAppObserver> loggedEmployees;

        public AppServicesImpl(IEmployeeRepository eRepo, Repository<Child> childRepository, IChildTrialRepository childTrialRepository, Repository<Trial> trialRepository)
        {

            this.employeeRepository = eRepo;
            this.childRepository = childRepository;
            this.childTrialRepository = childTrialRepository;
            this.trialRepository = trialRepository;
            this.loggedEmployees = new Dictionary<Guid, IAppObserver>();
        }

        public List<Child> getAllChildren()
        {
            List<Child> children = this.childRepository.findAll();
            if (children.Count == 0)
                throw new AppException("There are no children!");
            return children;
        }

        public List<Trial> getAllTrials()
        {
            List<Trial> trials = this.trialRepository.findAll();
            if (trials.Count == 0)
                throw new AppException("There are no children!");
            return trials;
        }

        public List<Guid> getChildTrials(Child child)
        {
            List<Guid> childTrials = this.childTrialRepository.readChildTrials(child.GetGuid());
            return childTrials;
        }

        public void login(Employee employee, IAppObserver client)
        {
            Employee employeeR = employeeRepository.findByUsername(employee.getUsername());
           
            if (employeeR != null)
            {
                if (!object.Equals(employeeR.getPassword(), employee.getPassword()))
                    throw new AppException("Wrong password");
                else if (loggedEmployees.ContainsKey(employeeR.GetGuid()))
                {
                    throw new AppException("employee already logged in.");
                }
                loggedEmployees.Add(employeeR.GetGuid(), client);
            }
            else
                throw new AppException("Authentication failed.");
        }

        public void logout(Employee employee)
        {
            Employee employeeR = employeeRepository.findByUsername(employee.getUsername());
            bool localClient = loggedEmployees.Remove(employeeR.GetGuid());
            if (localClient == false)
                throw new AppException("User " + employee.GetGuid() + " is not logged in.");
        }

        public void sendChild(Child child, List<Trial> trials)
        {
            if (loggedEmployees.Count == 0)
            {
                throw new AppException("Nobody is logged in");
            }

            this.childRepository.add(child);
            foreach(Trial trial in trials)
            {
                this.childTrialRepository.addChildTrial(child.GetGuid(), trial.GetGuid());
            }
            foreach (IAppObserver iAppObserver in loggedEmployees.Values)
            {
                Task.Run(() => iAppObserver.ChildReceived(child, trials));
            }
        }
    }
}
