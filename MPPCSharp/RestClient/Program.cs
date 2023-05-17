using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using MPPCSharp.Models;
using Newtonsoft.Json;


namespace CSharpRestClient
{
    class MainClass
    {

        static HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));

        private static string URL_Base = "http://localhost:8080/";

        public static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            RunAsync().Wait();
        }


        static async Task RunAsync()
        {

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



            Console.WriteLine("Size is:  ", await getSizeAsync("http://localhost:8080/app/trials/size"));

            Trial trial = new Trial { distance = 1500, trialName = "Incercamdinnou", trialDescription="Descrierea cea mai faina" };
            Console.WriteLine("Create trial {0}", trial);
            Trial result = await CreateTrialAsync("http://localhost:8080/app/trials/", trial);
            Console.WriteLine("Got {0}", result);


           
            String id = trial.id.ToString();
            Console.WriteLine("Get trial {0}", id);
            Trial result1 = await getTrialAsync("http://localhost:8080/app/trials/" + id);
            Console.WriteLine("Got {0}", result1);


            List<Trial> allRestuls = await readAllTrials("http://localhost:8080/app/trials/");

            foreach(Trial trialResult in allRestuls)
            {
                Console.WriteLine(trialResult);

            }

            Trial trialUpdate = new Trial { distance = 1500, trialName = "Incercamdinnou2", trialDescription = "Descrierea cea mai faina 2" };
            trialUpdate.id = trial.id;


            Console.WriteLine(await updateTrialAsync("http://localhost:8080/app/trials/", trialUpdate));


            id = trial.id.ToString();
            Console.WriteLine("Get trial {0}", id);
            Trial result2 = await getTrialAsync("http://localhost:8080/app/trials/" + id);
            Console.WriteLine("Got {0}", result2);


            Console.WriteLine(await deleteTrialAsync("http://localhost:8080/app/trials/" + id));

            while (true)
            {

            }



        }

        static async Task<String> deleteTrialAsync(string path)
        {
            HttpResponseMessage response =  await client.DeleteAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return "Deleted with success";
            }
            return "Failed to delete";
        }

        static async Task<int> getSizeAsync(string path)
        {
            int product = 0;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<int>();
            }
            return product;
        }

        static async Task<Trial> getTrialAsync(string path)
        {
            Trial product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Trial>();
            }
            return product;
        }


        static async Task<Trial> CreateTrialAsync(string path, Trial trial)
        {
            Trial result = null;
            HttpResponseMessage response = await client.PostAsJsonAsync(path, trial);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<Trial>();
            }
            return result;
        }

        static async Task<List<Trial>> readAllTrials(string path)
        {
            List<Trial> result = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<List<Trial>>();
            }
            return result;
        }

        static async Task<String> updateTrialAsync(string path, Trial trial)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(path, trial);
            if (response.IsSuccessStatusCode)
            {
                return "Update success";
            }

            return "Failed updated";
        }
    }
    public class Trial : Entity
    {
        //[JsonProperty("passwd")]
        public int distance { get; set; }
        //[JsonProperty("id")]
        public string trialName { get; set; }
        //[JsonProperty("name")]
        public string trialDescription { get; set; }

        public override string ToString()
        {
            return string.Format("[Trial: distance={0}, trialName={1}, trialDescription={2}, id={3}]", distance, trialName, trialDescription, id);
        }
    }

    public class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Request:");
            Console.WriteLine(request.ToString());
            if (request.Content != null)
            {
                Console.WriteLine(await request.Content.ReadAsStringAsync());
            }
            Console.WriteLine();

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            Console.WriteLine("Response:");
            Console.WriteLine(response.ToString());
            if (response.Content != null)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            Console.WriteLine();

            return response;
        }
    }

}
