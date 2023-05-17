using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPCSharp.Models.Validators
{
    public interface ChildValidatorInterface
    { 
        void validate(String firstName, String lastName, String age);
    }
}
