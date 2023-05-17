using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MPPCSharp.Models;


namespace MPPCSharp.Services
{
    public interface IAppServices
    {
        void login(Employee employee, IAppObserver observer);
        void sendChild(Child child, List<Trial> trials);
        void logout(Employee employee);

        List<Child> getAllChildren();
        List<Trial> getAllTrials();

        List<Guid> getChildTrials(Child child);
    }
}
