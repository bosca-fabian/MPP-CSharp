using AppNetworking.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNetworking.RpcProtocols.RpcCommunicationProtocols
{
    //LOGIN, LOGOUT, SEND_CHILD, GET_ALL_CHILDREN, GET_ALL_TRIALS, GET_CHILD_TRIALS;

    internal class RPCCommunicationProtocols
    {
        public interface Request
        {
        }


        [Serializable]
        public class LOGIN : Request
        {
            private EmployeeDTO employee;
            public LOGIN(EmployeeDTO employee)
            {
                this.employee = employee;
            }

            public virtual EmployeeDTO data { get { return employee; } }

        }

        [Serializable]
        public class LOGOUT : Request
        {
            private EmployeeDTO employee;
            public LOGOUT(EmployeeDTO employee)
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
        public class SEND_CHILD : Request
        {
            private ChildTrialsDTO childTrials;

            public SEND_CHILD(ChildTrialsDTO childTrials)
            {
                this.childTrials = childTrials;
            }
            public virtual ChildTrialsDTO data { get { return childTrials; } }

        }
        [Serializable]
        public class GET_ALL_CHILDREN : Request
        {

        }

        [Serializable]
        public class GET_ALL_TRIALS : Request
        {

        }

        [Serializable]
        public class GET_CHILD_TRIALS : Request
        {
            private ChildDTO child;
            public GET_CHILD_TRIALS(ChildDTO child)
            {
                this.child = child;
            }
            public virtual ChildDTO data { get { return child; } }
        }
    }

    

}
