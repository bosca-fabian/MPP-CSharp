using log4net;
using log4net.Repository.Hierarchy;
using MPPCSharp.Models;
using MPPCSharp.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Data.Common;

namespace MPPCSharp.Repository
{
    public class ChildRepository : Repository<Child>
    {
        IDictionary<string, string> props;


        private static readonly log4net.ILog logger = LogManager.GetLogger("ChildRepository");

        NpgsqlConnection connection;

        public ChildRepository(IDictionary<string, string> props) {
            this.props = props;
            connection = JdbcUtils.GetConnection(this.props);
        }

         ~ChildRepository() {
        }

        public void add(Child entity)
        {
            String sql = "insert into \"Child\" (id, \"firstName\", \"lastName\", \"age\") values (@id, @fName, @lName, @age);";
            logger.Debug(sql);
            
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("id", entity.GetGuid().ToString());
                command.Parameters.AddWithValue("fName", entity.getFirstName());
                command.Parameters.AddWithValue("lName", entity.getLastName());
                command.Parameters.AddWithValue("age", entity.getAge());
                int result = command.ExecuteNonQuery();
                logger.Info(result);
                connection.Close();
            }
            catch(NpgsqlException e)
            {
                connection.Close();
                Console.WriteLine(e);
                logger.Error(e);
            }
            
        }

        public void delete(Guid entity)
        {
            String sql = "DELETE FROM \"Child\" WHERE id = @id;";
            logger.Debug(sql);
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("id", entity.ToString());
                int result = command.ExecuteNonQuery();
                logger.Info(result);
                connection.Close();
            }
            catch(NpgsqlException e)
            {
                connection.Close();
                Console.WriteLine(e);
                logger.Error(e);
            }
         
        }

        public List<Child> findAll()
        {
            logger.Info("Reading all children!");
            String sql = "select *  from \"Child\"";
            List<Child> children = new List<Child>();
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) {
                    String readID = reader["id"].ToString();
                    String firstName = reader["firstName"].ToString();
                    String lastName = reader["lastName"].ToString();
                    int age, noTrials;
                    Int32.TryParse(reader["age"].ToString(), out age);
                    Int32.TryParse(reader["noTrials"].ToString(), out noTrials);
                    Child readChild = new Child(firstName, lastName, age, noTrials);
                    readChild.setGuid(Guid.Parse(readID));
                    children.Add(readChild);
                }
                connection.Close();

            }

            catch (NpgsqlException e)
            {
                connection.Close();
                Console.WriteLine(e);
                logger.Error(e);

            }
            logger.Info(children);
            return children;
        }

        public Child findById(Guid id)
        {
            logger.InfoFormat("Reading child {}", id.ToString());
            String sql = "select * from \"Child\" where id=@id";
            try 
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("id", id.ToString());
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                String readID = reader["id"].ToString();
                String firstName = reader["firstName"].ToString();
                String lastName = reader["lastName"].ToString();
                int age, noTrials; 
                Int32.TryParse(reader["age"].ToString(), out age);
                Int32.TryParse(reader["noTrials"].ToString(), out noTrials);
                Child readChild = new Child(firstName, lastName, age, noTrials);
                readChild.setGuid(Guid.Parse(readID));
                logger.Debug(readChild);
                connection.Close();
                return readChild;
                
            }
            catch (NpgsqlException e) 
            {
                connection.Close();
                Console.WriteLine(e);
                logger.ErrorFormat("Database error: ", e);
            }
            return null;
        }

        public int size()
        {
            logger.Info("Reading number of children");
            String sql = "SELECT COUNT(*) as totalChildren FROM \"Child\"";
            
            int noChildren;
            try
            {

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                Int32.TryParse(reader["totalChildren"].ToString(), out noChildren);
                logger.InfoFormat("Number of children read: ", noChildren);
                connection.Close();
                return noChildren;
            }
            catch(NpgsqlException e)
            {
                connection.Close();
                Console.WriteLine(e);
                logger.ErrorFormat("Database error: ", e);
            }
            return 0;
        }

        public void update(Child entity)
        {
            logger.InfoFormat("Updating entity with ID {}", entity.GetGuid());
         
            String sql = "UPDATE \"Child\" SET \"firstName\" = @fname, \"lastName\" = @lName, \"age\" = age WHERE id = @id;";
             
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("id", entity.GetGuid().ToString());
                command.Parameters.AddWithValue("fName", entity.getFirstName());
                command.Parameters.AddWithValue("id", entity.getLastName());
                command.Parameters.AddWithValue("id", entity.getAge());
                int result = command.ExecuteNonQuery();
                logger.DebugFormat("Update result {}", result);
                connection.Close();
            }
            catch (NpgsqlException e)
            {
                connection.Close();
                Console.WriteLine(e);
                logger.ErrorFormat("Database error {}", e);
            }
        }
    }
}
