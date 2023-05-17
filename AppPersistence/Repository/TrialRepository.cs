using log4net;
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
    public class TrialRepository : Repository<Trial>
    {
        IDictionary<string, string> props;

        private static readonly log4net.ILog logger = LogManager.GetLogger("TrialRepository");

        NpgsqlConnection con;

        public TrialRepository(IDictionary<string, string> props)
        {
            this.props = props;
            con = JdbcUtils.GetConnection(this.props);
           
        }

        ~TrialRepository() {
            /*con.Close()*/;
        }

        public void add(Trial entity)
        {   
            logger.InfoFormat("saving task {}", entity);
            String sql = "insert into \"Trial\"(id, distance, \"trialName\", \"trialDescription\") values (@id, @dist, @tName, @tDesc)";
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.Parameters.AddWithValue("id", entity.GetGuid());
                command.Parameters.AddWithValue("dist", entity.Distance);
                command.Parameters.AddWithValue("tName", entity.TrialName);
                command.Parameters.AddWithValue("tDesc", entity.TrialDescription);
                int result = command.ExecuteNonQuery();
                logger.DebugFormat("Result after add trial {}", result);
                con.Close();
            }
            catch (NpgsqlException e)
            {
                con.Close();
                Console.WriteLine(e);
                logger.Error("Database add trial error : " + e);

            }
        
        }

        public void delete(Guid entity)
        {
            logger.InfoFormat("Saving task, deleteting trial entity with id {}", entity.ToString());
            String sql = "DELETE FROM \"Trial\" WHERE id = @id;";
            try
            {

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.Parameters.AddWithValue("id", entity.ToString());
                int result = command.ExecuteNonQuery();
                logger.DebugFormat("Result after delete trial {}", result);
                con.Close() ;
            }
            catch (NpgsqlException e)
            {
                con.Close();
                Console.WriteLine(e);
                logger.Error("Database delete trial error : " + e);
            }
        }

        public List<Trial> findAll()
        {
            logger.Info("Reading all trials");
            String sql = "select * from \"Trial\"";
            List<Trial> trials = new List<Trial>();
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
                    int distance;
                    Int32.TryParse(reader["distance"].ToString(), out distance);
                    String trialName = reader["trialName"].ToString();
                    String trialDesc = reader["trialDescription"].ToString();
                    Trial readTrial = new Trial(distance, trialName, trialDesc);
                    readTrial.setGuid(Guid.Parse(id));
                    trials.Add(readTrial);
                }
                con.Close() ;
            }
            catch (NpgsqlException e)
            {
                con.Close();
                Console.WriteLine(e);
                logger.Error("Database trial read all trials error : " + e);
            }
            logger.Debug(trials);
            return trials;
        }

        public Trial findById(Guid id)
        {
            logger.InfoFormat("Reading trial {}", id.ToString());
            String sql = "select *  from \"Trial\" where id = @id";
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
                String readID = reader.GetString(1);
                int distance = reader.GetInt32(2);
                String trialName = reader["trialName"].ToString();
                String trialDesc = reader["trialDescription"].ToString();
                Trial readTrial = new Trial(distance, trialName, trialDesc);
                readTrial.setGuid(Guid.Parse(readID));
                logger.Debug(readTrial);
                con.Close();
                return readTrial;

            }
            catch (NpgsqlException e)
            {
                con.Close();
                Console.WriteLine(e);
                logger.ErrorFormat("Database error reading trial: ", e);
            }
            return null;
        }

        public int size()
        {
            logger.Info("Reading number of trials");
            String sql = "SELECT COUNT(*) as totalTrials FROM \"Trial\"";

            int noTrials;
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                Int32.TryParse(reader["totalTrials"].ToString(), out noTrials);
                logger.InfoFormat("Number of trials read: ", noTrials);
                con.Close();
                return noTrials;
            }
            catch (NpgsqlException e)
            {
                con.Close();
                Console.WriteLine(e);
                logger.ErrorFormat("Database error providing size, number of trials: ", e);
            }
            con.Close();
            return 0;
        }

        public void update(Trial entity)
        {
            logger.InfoFormat("Updating entity with ID {}", entity.GetGuid());
            String sql = "UPDATE \"Trial\" SET distance = @dist, \"trialName\" = @tName, \"trialDescription\" = @tDesc WHERE id=@id;";
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.Parameters.AddWithValue("id", entity.GetGuid().ToString());
                command.Parameters.AddWithValue("dist", entity.Distance);
                command.Parameters.AddWithValue("tName", entity.TrialName);
                command.Parameters.AddWithValue("tDesc", entity.TrialDescription);
                int result = command.ExecuteNonQuery();
                logger.DebugFormat("Update result {}", result);
                con.Close() ;
            }
            catch (NpgsqlException e)
            {
                con.Close();
                Console.WriteLine(e);
                logger.ErrorFormat("Database error updating trial {}", e);
            }
        }
    }
}
