using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AppNetworking.DTO
{
    [Serializable]
    public class ChildDTO 
    {
    public Guid id;
    public String firstName;
    public String lastName;
    public int age;
    public int noTrials;

    public ChildDTO(Guid id, String firstName, String lastName, int age, int noTrials)
    {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
        this.noTrials = noTrials;
    }

    public String toString()
    {
        return "ChildDTO{" +
                "id=" + id +
                ", firstName='" + firstName + '\'' +
                ", lastName='" + lastName + '\'' +
                ", age=" + age +
                ", noTrials=" + noTrials +
                '}';
    }
}
}
