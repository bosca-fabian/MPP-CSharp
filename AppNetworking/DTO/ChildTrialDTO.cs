using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNetworking.DTO
{
    [Serializable]
    public class ChildTrialsDTO 
    {

    public ChildDTO childDTO;

    public List<TrialDTO> trialsDTOLIst;

    public ChildTrialsDTO(ChildDTO childDTO, List<TrialDTO> trialsDTOLIst)
    {
        this.childDTO = childDTO;
        this.trialsDTOLIst = trialsDTOLIst;
    }
}
}
