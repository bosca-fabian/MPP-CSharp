using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNetworking.DTO
{
    [Serializable]
    public class TrialDTO  
    {
    public Guid id;
    public String trialName;
    public String trialDescription;
    public int distance;

    public TrialDTO(Guid id, String trialName, String trialDescription, int distance)
    {
        this.id = id;
        this.trialDescription = trialDescription;
        this.trialName = trialName;
        this.distance = distance;
    }
}
}
