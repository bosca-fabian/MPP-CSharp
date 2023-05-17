using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPersistence.Interfaces
{
    public interface ChildTrialRepoInterface
    {
        void addChildTrial(Guid childID, Guid trialID);

        void deleteChildTrial(Guid childID, Guid trialID);

        void deleteAllChildTrials(Guid childID);
        List<Guid> readChildTrials(Guid childID);

    }
}
