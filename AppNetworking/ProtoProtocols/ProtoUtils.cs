using Mpp.Protocol;
using MPPCSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AppNetworking.ProtoProtocols
{
    public class ProtoUtils
    {
        public static Respone createOkResponse()
        {
            Respone response = new Respone
            {
                Type = Respone.Types.Type.Ok
            };                    
            return response;
        }
        public static Respone createFailResponse(String errorText)
        {
            Respone respone = new Respone
            {
                Type = Respone.Types.Type.Error,
                Error = errorText
            };
            return respone;
        }

        private static Mpp.Protocol.Child createProtoChild(MPPCSharp.Models.Child child)
        {
            return new Mpp.Protocol.Child
            {
                Id = child.GetGuid().ToString(),
                FirstName = child.getFirstName(),
                LastName = child.getLastName(),
                Age = child.getAge(),
                NoTrials = child.getNoTrials(),
            };
        }
        private static Mpp.Protocol.Trial createProtoTrial(MPPCSharp.Models.Trial trial)
        {
            return new Mpp.Protocol.Trial
            {
                Id=trial.GetGuid().ToString(),
                Distance=trial.Distance,
                TrialDescription=trial.TrialDescription,
                TrialName=trial.TrialName,
            };
        }

        public static Respone createNewChildRespone(MPPCSharp.Models.Child child, List<MPPCSharp.Models.Trial> trials) 
        {
            Respone respone = new Respone
            {
                Type = Respone.Types.Type.NewChild,
                ChildTrial = new ChildTrial { Child = createProtoChild(child) }
            };
            respone.ChildTrial.Trials.Add(trials.Select(createProtoTrial));
            return respone;
        }


        public static Respone sendAllChildrenResponse(List<MPPCSharp.Models.Child> children)
        {
            Respone respone = new Respone
            {
                Type = Respone.Types.Type.SendAllChildren,
                Children = new Children {

                }
            };
          
            respone.Children.Children_.Add(children.Select(createProtoChild));
            //foreach(MPPCSharp.Models.Child child in children)
            //{
            //    respone.Children.Children_.Add(createProtoChild(child));
            //}
            return respone;
        }

        public static Respone sendAllTrialsResponse(List<MPPCSharp.Models.Trial> trials)
        {
            Respone respone = new Respone
            {
                Trial = new Mpp.Protocol.Trials
                {

                }
            };
            respone.Trial.Trials_.Add(trials.Select(createProtoTrial)); 
            return respone;
        }

        private static string convertToString(Guid guid)
        {
            return guid.ToString();
        }

        public static Respone sendChildsTrialsResponse(List<Guid> trialsID)
        {
           
            AChildsTrials aChildsTrials = new AChildsTrials();
            aChildsTrials.TrialsID.Add(trialsID.Select(convertToString));
            Respone respone = new Respone
            {
                ChildTrialsID = aChildsTrials
            };
            return respone;
        }

        private static MPPCSharp.Models.Employee getModelEmployee(Mpp.Protocol.Employee protoEmployee)
        {
            MPPCSharp.Models.Employee modelEmployee = new MPPCSharp.Models.Employee(protoEmployee.Username, protoEmployee.Password);   
            modelEmployee.setGuid(Guid.Parse(protoEmployee.Id));
            return modelEmployee;
        }

        private static MPPCSharp.Models.Child getModelChild(Mpp.Protocol.Child protoChild)
        {
            MPPCSharp.Models.Child modelChild = new MPPCSharp.Models.Child(protoChild.FirstName, protoChild.LastName, protoChild.Age);
            modelChild.setGuid(Guid.Parse(protoChild.Id));
            return modelChild;

        }

        private static MPPCSharp.Models.Trial getModelTrial(Mpp.Protocol.Trial protoTrial)
        {
            MPPCSharp.Models.Trial modelTrial = new MPPCSharp.Models.Trial(protoTrial.Distance, protoTrial.TrialName, protoTrial.TrialDescription);
            modelTrial.setGuid(Guid.Parse(protoTrial.Id));
            return modelTrial;
        }

        private static List<MPPCSharp.Models.Trial> getListModelTrials(Mpp.Protocol.ChildTrial childTrial) 
        {
            List<MPPCSharp.Models.Trial> trials = new List<MPPCSharp.Models.Trial>();
            foreach(Mpp.Protocol.Trial trial in childTrial.Trials)
            {
                trials.Add(getModelTrial(trial));
            }
            return trials;
        }

        public static MPPCSharp.Models.Employee getEmployeeFromRequest(Request request)
        {
            return getModelEmployee(request.Employee);
        }

        public static MPPCSharp.Models.Child getChildFromChildTrialRequest(Request request)
        {
            return getModelChild(request.ChildTrial.Child);
        }


        public static List<MPPCSharp.Models.Trial> getTrialsFromChildTrialRequest(Request request)
        {
            return getListModelTrials(request.ChildTrial);
        }

        public static MPPCSharp.Models.Child getChildFromRequest(Request request)
        {
            return getModelChild(request.Child);
        }
    }
}
