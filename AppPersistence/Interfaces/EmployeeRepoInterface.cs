using MPPCSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPCSharp.Repository
{
    public interface EmployeeRepoInterface
    {
        void add(Employee entity);
        void delete(Guid entity);
        void update(Employee entity);
        int size();
        Employee findById(Guid entityID);
        Employee findByUsername(String username);
        List<Employee> findAll();
    }
}
