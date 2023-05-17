using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPCSharp.Models
{
    public class Trial : Entity
    {
        private int distance;
        private String trialName;
        private String trialDescription;

        public Trial(int distance, string trialName, string trialDescription)
        {
            this.distance = distance;
            this.trialName = trialName;
            this.trialDescription = trialDescription;
        }

        public virtual int Distance { get { return distance; }
        set { distance = value; } }
        public virtual String TrialName {
            get 
            {
                return trialName;
            }
            set 
            { 
                trialName = value; 
            } 
        }
        public virtual String TrialDescription {
            get
            {
                return trialDescription;
            }
            set 
            { 
                this.trialDescription = value;
            }
        }

        public override string ToString() {
            return this.trialName + ' ' + this.trialDescription + ' ' + this.distance;
        }
    }
}
