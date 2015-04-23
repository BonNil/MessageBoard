using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;


namespace MessagePost
{

    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Write("Enter message to post (Enter \"!quit\" to quit): ");
                string message = Console.ReadLine();
                if (message.Equals("!quit") || message.Equals("\"!quit\"")) 
                {
                    break;
                }

                try
                { 
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:58443/api/messages");
                    httpWebRequest.ContentType = "text/json";
                    httpWebRequest.Method = "POST";
                
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = "{\"TextMessage\":\"" + message + "\"";

                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected error, exiting");
                    Console.WriteLine(e.StackTrace);
                    Console.Read();
                    Environment.Exit(0);
                }
                
            } while (true);


            
        }
    }
}
