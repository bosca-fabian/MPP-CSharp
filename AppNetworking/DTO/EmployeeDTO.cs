using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AppNetworking.DTO
{
    [Serializable]
    public class EmployeeDTO  
    {
    public Guid id;
    public String username;
    public String password;

    public EmployeeDTO(Guid id, String username, String password)
    {
        this.id = id;
        this.username = username;
        this.password = password;
    }

    public String toString()
    {
        return "EmployeeDTO{" +
                "id=" + id +
                ", username='" + username + '\'' +
                ", password='" + password + '\'' +
                '}';
    }
}
}
