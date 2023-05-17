using log4net;
using MPPCSharp.Repository;
using Npgsql;
using MPPCSharp.Models;
using MPPCSharp.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPCSharp.Repository
{
    public class EmployeeRepository : EmployeeRepoInterface
    {
        IDictionary<string, string> props;

        private static readonly log4net.ILog logger = LogManager.GetLogger("EmployeeRepository");

        NpgsqlConnection con;

        public EmployeeRepository(IDictionary<string, string> props)
        {
            this.props = props;
            con = JdbcUtils.GetConnection(this.props);
        }

        public void add(Employee entity)
        {
            logger.InfoFormat("saving task {}", entity);
            String sql = "insert into \"Employee\"(id, username, password) values (@id, @user, @pass);";
            try {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.Parameters.AddWithValue("id", entity.GetGuid());
                command.Parameters.AddWithValue("user", entity.getUsername());
                command.Parameters.AddWithValue("pass", entity.getPassword());
                int result = command.ExecuteNonQuery();
                logger.DebugFormat("Result after add employee {}", result);
                con.Close();
            }
            catch (NpgsqlException e) 
            {
                con.Close();
                Console.WriteLine(e);
                logger.Error("Database add employee error : " + e);
            }
        }

        public void delete(Guid entity)
        {
            logger.InfoFormat("Saving task, deleteting employee entity with id {}",entity.ToString());
            String sql = "DELETE FROM \"Employee\" WHERE id = @id;";
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.Parameters.AddWithValue("@id", entity.ToString());
                int result = command.ExecuteNonQuery();
                logger.DebugFormat("Result after delete employee {}", result);
                con.Close();
            }
            catch(NpgsqlException e) 
            {
                con.Close();
                Console.WriteLine (e);
                logger.Error("Database delete employee error : " + e); 
            }
        }

        public List<Employee> findAll()
        {
            logger.Info("Reading all employees");
            String sql = "select * from \"Employee\"";
            List<Employee> employees = new List<Employee>();
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    String id = reader["id"].ToString();
                    String username = reader["username"].ToString();
                    String password = reader["password"].ToString();
                    Employee readEmployee = new Employee(username, password);
                    readEmployee.setGuid(Guid.Parse(id));
                    employees.Add(readEmployee);
                }
                con.Close();
            }
            catch (NpgsqlException e) {
                con.Close();
                Console.WriteLine (e); 
                logger.Error ("Database employee read all employees error : " + e);
            }
            logger.Debug(employees);

            return employees;
        }

        public Employee findById(Guid id)
        {
            logger.InfoFormat("Reading employee {}", id.ToString());
            String sql = "select *  from \"Employee\" where id = @id";
            try
            {

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.Parameters.AddWithValue("@id", id.ToString());
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                String readID = reader["id"].ToString();
                String username = reader["username"].ToString();
                String password = reader["password"].ToString();
                Employee readEmployee = new Employee(username, password);
                readEmployee.setGuid(Guid.Parse(readID));
                logger.Debug(readEmployee);
                con.Close();
                return readEmployee;

            }
            catch (NpgsqlException e)
            {
                con.Close();
                Console.WriteLine(e);
                logger.ErrorFormat("Database error reading employee: ", e);
            }
            return null;
        }

        public int size()
        {
            logger.Info("Reading number of employees");
            String sql = "SELECT COUNT(*) as totalEmployee FROM \"Employee\"";

            int noEmployees;
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                Int32.TryParse(reader["totalEmployee"].ToString(), out noEmployees);
                logger.InfoFormat("Number of employees read: ", noEmployees);
                con.Close() ;
                return noEmployees;
            }
            catch (NpgsqlException e)
            {
                con.Close();
                Console.WriteLine(e);
                logger.ErrorFormat("Database error providing size, number of employees: ", e);
            }
            return 0;
        }

        public void update(Employee entity)
        {
            logger.InfoFormat("Updating entity with ID {}", entity.GetGuid());
            String sql = "UPDATE \"Employee\" SET \"username\" = @user, \"password\" = @pass WHERE id = @id;";
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.Parameters.AddWithValue("id", entity.GetGuid());
                command.Parameters.AddWithValue("user", entity.getUsername());
                command.Parameters.AddWithValue("pass", entity.getPassword());
                int result = command.ExecuteNonQuery();
                logger.DebugFormat("Update result {}", result);
                con.Close();
            }
            catch (NpgsqlException e)
            {
                con.Close();
                Console.WriteLine(e);
                logger.ErrorFormat("Database error updating employee {}", e);
            }
        }

        public Employee findByUsername(string eUsername)
        {
            logger.InfoFormat("Reading employee {}", eUsername);
            String sql = "select *  from \"Employee\" where \"username\" = @user";
            try
            {

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.Parameters.AddWithValue("@user", eUsername);
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                String readID = reader["id"].ToString();
                String username = reader["username"].ToString();
                String password = reader["password"].ToString();
                Employee readEmployee = new Employee(username, password);
                readEmployee.setGuid(Guid.Parse(readID));
                logger.Debug(readEmployee);
                con.Close();
                return readEmployee;

            }
            catch (NpgsqlException e)
            {
                con.Close();
                Console.WriteLine(e);
                logger.ErrorFormat("Database error reading employee: ", e);
            }
            return null;
        }
    }
}
