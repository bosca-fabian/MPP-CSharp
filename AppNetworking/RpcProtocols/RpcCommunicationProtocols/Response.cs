using AppNetworking.DTO;
using MPPCSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNetworking.RpcProtocols.RpcCommunicationProtocols
{
    //OK, ERROR, NEW_CHILD, EMPLOYEE_LOGGED_IN, EMPLOYEE_LOGGED_OUT, SEND_ALL_CHILDREN,SEND_ALL_TRIALS, SEND_CHILD_TRIALS;

    internal class ObjectRequestProtocols
    {
        public interface Response
        {
        }

        [Serializable]
        public class OK : Response { }

        [Serializable]
        public class UPDATE_RESPONSE : Response { }

        [Serializable]
        public class ERROR : Response
        {
            private string message;

            public ERROR(string message)
            {
                this.message = message;
            }

            public virtual string data
            {
                get
                {
                    return message;
                }
            }
        }

        [Serializable]
        public class NEW_CHILD : UPDATE_RESPONSE
        {
            ChildTrialsDTO childTrials;

            public NEW_CHILD(ChildTrialsDTO childTrials)
            {
                this.childTrials = childTrials;
            }

            public virtual ChildTrialsDTO data
            {
                get
                {
                    return childTrials;
                }
            }

        }


        [Serializable]
        public class EMPLOYEE_LOGGED_IN : Response
        {
            private EmployeeDTO employee;

            public EMPLOYEE_LOGGED_IN(EmployeeDTO employee)
            {
                this.employee = employee;
            }

            public virtual EmployeeDTO data
            {
                get
                {
                    return employee;
                }
            }
        }

        [Serializable]
        public class EMPLOYEE_LOGGED_OUT : Response
        {
            private EmployeeDTO employee;

            public EMPLOYEE_LOGGED_OUT(EmployeeDTO employee)
            {
                this.employee = employee;
            }

            public virtual EmployeeDTO data
            {
                get
                {
                    return employee;
                }
            }
        }

        [Serializable]
        public class SEND_ALL_CHILDREN : Response
        {
            private List<ChildDTO> children;

            public SEND_ALL_CHILDREN(List<ChildDTO> children)
            {
                this.children = children;
            }

            public virtual List<ChildDTO> data
            {
                get
                {
                    return children;
                }
            }
        }
        [Serializable]
        public class SEND_ALL_TRIALS : Response
        {
            private List<TrialDTO> trials;

            public SEND_ALL_TRIALS(List<TrialDTO> trials)
            {
                this.trials = trials;
            }

            public virtual List<TrialDTO> data
            {
                get
                {
                    return trials;
                }
            }
        }
        [Serializable]
        public class SEND_CHILD_TRIALS : Response
        {
            private List<Guid> childTrials;

            public SEND_CHILD_TRIALS(List<Guid> childrenTrials)
            {
                this.childTrials = childrenTrials;
            }

            public virtual List<Guid> data
            {
                get
                {
                    return childTrials;
                }
            }
        }
    }
   

}
