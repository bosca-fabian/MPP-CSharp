using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MPPCSharp.Utils
{
    public enum AgeBrackets
    {
        [Description("6 - 8")]
        SIX_EIGHT,
        [Description("9 - 11")]
        NINE_ELEVEN,
        [Description("12 - 15")]
        TWELVE_FIFTEEN,
        [Description("NO AGE RESTRICTION")]
        NO_AGE_RESTRICTION
    }  
}
