using MPPCSharp.Models;
using MPPCSharp.Repository;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppPersistence.ORM
{
    public class ChildRepoORM : Repository<Child>
    {
        IDictionary<string, string> props;
        //string conString = "Host=localhost;Username=postgres;Password=postgres;Database=mpp";
        ISessionFactory sessionFactory;
        public ChildRepoORM(IDictionary<string, string> props)
        {
            this.props = props;
            this.sessionFactory = new Configuration().Configure().BuildSessionFactory();
        }

        public void add(Child entity)
        {
            using(var session = sessionFactory.OpenSession())
            {
                session.Save(entity);
                session.Flush();
            }
        }

        public void delete(Guid entity)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var child = session.Get<Child> (entity.ToString());
                session.Delete(child);
                session.Flush();
            }
        }

        public List<Child> findAll()
        {
            using (var session = sessionFactory.OpenSession())
            {
                List<Child> list = new List<Child>();
                var children = session.CreateCriteria<Child>().List<Child>();
                children.ToList().ForEach(child =>
                {
                    list.Add(child);
                });
                return list; 
            }
        }

        public Child findById(Guid id)
        {   
            using(var session = sessionFactory.OpenSession())
            {
                var child = session.Get<Child>(id.ToString());
                return child;
            }
        }

        public int size()
        {
            using (var session = sessionFactory.OpenSession())
            {
                var count = session.QueryOver<Child>().RowCount();
                return (int)count;
            }

        }

        public void update(Child entity)
        {
            throw new NotImplementedException();
        }
    }
}
