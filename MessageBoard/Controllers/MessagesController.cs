using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;
using MessageBoard.Models;

namespace MessageBoard
{
    public class MessagesController : ApiController
    {
        static string MyConnectionString = "Server=188.166.99.147;Database=AteaMessages;Uid=TestUser;Pwd=TestPassword123";
        MySqlConnection connection = new MySqlConnection(MyConnectionString);

        // GET api/messages
        public IEnumerable<Message> Get()
        {
            List<Message> messages = new List<Message>();
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM message ORDER BY postTime DESC";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);

                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        messages.Add(new Message { Id = (int)row["id"], PostTime = row["postTime"].ToString(), TextMessage = row["textMessage"].ToString() });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return messages;
        }

        // POST api/messages
        public void Post([FromBody]Message value)
        {
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO message(postTime, textMessage) VALUES (NOW(), @textMessage)";
                cmd.Parameters.AddWithValue("@textMessage", value.TextMessage);
                Debug.WriteLine(cmd.CommandText.ToString());
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }
    }
}