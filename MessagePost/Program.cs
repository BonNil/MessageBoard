using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;


namespace MessagePost
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            do
            {
                Console.Write("Enter message to post (Enter \"!quit\" to quit): ");
                string text = Console.ReadLine();
                if (text.Equals("!quit") || text.Equals("\"!quit\""))
                {
                    break;
                }
                try
                {
                    string apiUrl = "http://localhost:58443/api/messages";
                    var client = new HttpClient();
                    var message = new Dictionary<string, string>()
                    {
                        {"TextMessage", text}
                    };
                    var content = new FormUrlEncodedContent(message);

                    var response = await client.PostAsync(apiUrl, content);
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException hre)
                {
                    Console.WriteLine(hre.Message);
                    Console.WriteLine("The http request or response was not successful.");
                    continue;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Unexpected error, exiting.");
                    Console.Read();
                    Environment.Exit(0);
                }
            } while (true);    
        }
    }
}
