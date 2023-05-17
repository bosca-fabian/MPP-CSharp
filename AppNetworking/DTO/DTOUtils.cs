using MPPCSharp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNetworking.DTO
{
    public class DTOUtils
    {
        public static Employee getFromDTO(EmployeeDTO empDTO)
        {
            Guid id = empDTO.id;
            String username = empDTO.username;
            String password = empDTO.password;
            Employee newEmployee = new Employee(username, password);
            newEmployee.setGuid(id);
            return newEmployee;
        }

        public static EmployeeDTO getDTO(Employee employee)
        {
            Guid id = employee.GetGuid();
            String username = employee.getUsername();
            String password = employee.getPassword();
            return new EmployeeDTO(id, username, password);
        }

        public static Child getFromDTO(ChildDTO childDTO)
        {
            Guid id = childDTO.id;
            String firstName = childDTO.firstName;
            String lastName = childDTO.lastName;
            int age = childDTO.age;
            int noTrials = childDTO.noTrials;
            Child newChild = new Child(firstName, lastName, age, noTrials);
            newChild.setGuid(id);
            return newChild;

        }

        public static ChildDTO getDTO(Child child)
        {
            Guid id = child.GetGuid();
            String firstName = child.getFirstName();
            String lastName = child.getLastName();
            int age = child.getAge();
            int noTrials = child.getNoTrials();
            return new ChildDTO(id, firstName, lastName, age, noTrials);
        }

        public static TrialDTO getDTO(Trial trial)
        {
            Guid id = trial.GetGuid();
            String trialName = trial.TrialName;
            String trialDescription = trial.TrialDescription;
            int trialDistance = trial.Distance;
            return new TrialDTO(id, trialName, trialDescription, trialDistance);
        }


        public static Trial getFromDTO(TrialDTO trialDTO)
        {
            Guid id = trialDTO.id;
            String trialName = trialDTO.trialName;
            String trialDescription = trialDTO.trialDescription;
            int trialDistance = trialDTO.distance;
            Trial newTrial = new Trial(trialDistance, trialName, trialDescription);
            newTrial.setGuid(id);
            return newTrial;
        }

        public static ChildTrialsDTO getDTO(Child child, List<Trial> trials)
        {
            ChildDTO childDTO = getDTO(child);
            List<TrialDTO> trialsDTO = new List<TrialDTO>();
            foreach (Trial trial in trials
                 )
            {
                trialsDTO.Add(getDTO(trial));
            }
            return new ChildTrialsDTO(childDTO, trialsDTO);
        }

        public static Child getChildFromChildTrialsDTO(ChildTrialsDTO childTrialsDTO)
        {
            return getFromDTO(childTrialsDTO.childDTO);
        }

        public static List<Trial> getTrialsFromChildTrialsDTO(ChildTrialsDTO childTrialsDTO)
        {
            List<Trial> trials = new List<Trial>();
            foreach (TrialDTO trialDTO in childTrialsDTO.trialsDTOLIst
                 )
            {
                trials.Add(getFromDTO(trialDTO));
            }
            return trials;
        }


    }
}
