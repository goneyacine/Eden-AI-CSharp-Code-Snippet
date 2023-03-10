//instal Microsoft.AspNet.WebApi.Client first 

using System;
using System.IO;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Json;

public class Program
{

    static void Main(string[] args)
    {
        Console.WriteLine("OCR tables Started...");

        string filePath = "Your File Path Here";
        string apiKey = "Bearer 🔑 Your_API_Key";


        HttpClient httpClient = new HttpClient();

        //OCR tables Job Lunch here
        MultipartFormDataContent content = new MultipartFormDataContent();

        content.Add(new StreamContent(File.OpenRead(filePath)), "file", Path.GetFileName(filePath));

        StringContent stringContent = new StringContent("{\"providers\":\"microsoft\",\"language\":\"en\"}");
        content.Add(new StringContent("microsoft"), "providers");
        content.Add(new StringContent("en"), "language");

        HttpRequestMessage jobLunchRequest = new HttpRequestMessage(HttpMethod.Post, new Uri("https://api.edenai.run/v2/ocr/ocr_tables_async"));
        jobLunchRequest.Content = content;

        
        jobLunchRequest.Headers.Add("Authorization", apiKey);


        string public_id = "";
        using (HttpResponseMessage response = httpClient.Send(jobLunchRequest))
        {
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("job lunch succeeded");
                JobLunchResults results = response.Content.ReadFromJsonAsync<JobLunchResults>().Result;
                public_id = results.Public_id;
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Console.WriteLine("job lunch failed");
                Console.WriteLine(response.StatusCode.ToString());
            }
        }
        //ORC tables get job results here

        //waiting until orc table processing is finished to get the results (we make a request every 5 seconds)
        while (true)
        {
            HttpRequestMessage jobResultsRequest = new HttpRequestMessage(HttpMethod.Get, new Uri($"https://api.edenai.run/v2/ocr/ocr_tables_async/{public_id}"));
            jobResultsRequest.Headers.Add("Authorization", apiKey);
            using (HttpResponseMessage response = httpClient.Send(jobResultsRequest))
            {
                if (response.IsSuccessStatusCode)
                {
                    JobGetResults jobGetResults = response.Content.ReadFromJsonAsync<JobGetResults>().Result;
                    if (jobGetResults.Status == "finished")
                    {
                        Console.WriteLine("get job results succeeded");
                        Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("get job results failed");
                    Console.WriteLine(response.StatusCode.ToString());
                    break;
                }

            }
            Console.WriteLine("Still Processing...");
            Thread.Sleep(5000);
        }


        Console.ReadLine();
    }

    public class JobLunchResults
    {
        public string Public_id { get; set; } = "";
    }
    public class JobGetResults
    {
        public string Status { get; set; } = "";
    }


}
