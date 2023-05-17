using log4net;
using Npgsql;
using MPPCSharp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AppPersistence.Interfaces;

namespace MPPCSharp.Repository
{
    public class ChildTrialRepository : ChildTrialRepoInterface
    {
        IDictionary<string, string> props;


        private static readonly log4net.ILog logger = LogManager.GetLogger("ChildRepository");
        NpgsqlConnection con;

        public ChildTrialRepository(IDictionary<string, string> props)
        {
            this.props = props;
            con = JdbcUtils.GetConnection(this.props);
        }

        public void addChildTrial(Guid childID, Guid trialID)
        {
            logger.InfoFormat("Saving task child {} trial {} into DB", childID, trialID);
            String sql = "INSERT INTO \"ChildTrial\"(\"Child_ID\", \"Trial_ID\") VALUES (@childID, @trialID)";
            try
            {

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("childID", childID.ToString());
                cmd.Parameters.AddWithValue("trialID", trialID.ToString());
                int result = cmd.ExecuteNonQuery();
                logger.Debug(result);
                con.Close();

            }
            catch (NpgsqlException e)
            {
                con.Close();
                Console.WriteLine(e);
                logger.Error("ChildTrial database error: " + e);
            }
        }

        public void deleteChildTrial(Guid childID, Guid trialID) 
        {
            logger.InfoFormat("Saving task, deleting child {} trial {} from db", childID, trialID);
            String sql = "DELETE FROM \"ChildTrial\" WHERE \"Child_ID\" = @childID AND \"Trial_ID\" = @trialID";
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("childID", childID.ToString());
                cmd.Parameters.AddWithValue("trialID", trialID.ToString());
                int result = cmd.ExecuteNonQuery();
                logger.Debug(result);
                con.Close() ;
            }
            catch (NpgsqlException e)
            {
                con.Close();
                Console.WriteLine(e);
                logger.Error("ChildTrial delete database error: " + e);
            }
        }

        public void deleteAllChildTrials(Guid childID) 
        {
            logger.InfoFormat("Saving task, deleting all child {} trials from db", childID);
            String sql = "DELETE FROM \"ChildTrial\" WHERE \"Child_ID\" = @childID";
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("childID", childID.ToString());
                int result = cmd.ExecuteNonQuery();
                logger.Debug(result);
                con.Close();
            }
            catch (NpgsqlException e)
            {
                con.Close();
                Console.WriteLine(e);
                logger.Error("All ChildTrial delete database error: " + e);
            }
        }

        public List<Guid> readChildTrials(Guid childID)
        {
            logger.InfoFormat("Saving task, reading all trials for children {}", childID);
            String sql = "SELECT * FROM \"ChildTrial\" WHERE \"Child_ID\" = @childID";
            List<Guid> result = new List<Guid>();
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("childID", childID.ToString());
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    String trialID = reader.GetString(1);
                    result.Add(Guid.Parse(trialID));
                }
                con.Close();
            }
            catch(NpgsqlException e)
            {
                con.Close();
                Console.WriteLine(e);
                logger.Error("Reading child trials error: " + e);
            }
            logger.Debug(result);
            return result;
        }
    }
}
