using MPPCSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPCSharp.Services
{
    public interface IAppObserver
    {
        void ChildReceived(Child child, List<Trial> trials);

    }
}
