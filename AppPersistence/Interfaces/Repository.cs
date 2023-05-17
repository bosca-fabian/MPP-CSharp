using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPCSharp.Repository
{
    public interface Repository<E>
    {


        void add(E entity);

        void delete(Guid entity);

        void update(E entity);

        int size();

        E findById(Guid id);
        
        List<E> findAll();


    }
}
