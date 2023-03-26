# EdenAI Code Snippet
### this repository contains code snippet and quickstart guide for EdenAI API with C#


* ## Quickstart with Spell Check API

  #### POST : https://api.edenai.run/v2/text/spell_check
  
  Before you start you should make sure to instal Microsoft.AspNet.WebApi.Client
  
```cs

using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

```

```cs
public class Program
{

    static void Main(string[] args)
    {
        Console.WriteLine("Spell Check Started...");
        HttpClient httpClient = new HttpClient();
       

        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.edenai.run/v2/text/spell_check"),
            Headers =
            {
               { "accept", "application/json" },
            },
            Content = new StringContent("{\"response_as_dict\":true,\"attributes_as_list\":false,\"show_original_response\":false,\"providers\":\"microsoft\",\"text\":\"helowe world,ths is a txte\",\"language\":\"en\"}")
            {
                Headers =
                {
                  ContentType = new MediaTypeHeaderValue("application/json")
                }
            }
        };


        //pass your api key here 
        request.Headers.Add("Authorization", "Bearer 🔑 Your_API_Key");


        using (HttpResponseMessage response = httpClient.Send(request))
        {
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("succeeded...");
               Console.Write(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Console.WriteLine("failed...");
                Console.WriteLine(response.StatusCode.ToString());
            }
        }

        Console.ReadLine();
    }


  
}
  ```
  
  * ## Quickstart with Speech To Text API
  
   #### POST : https://api.edenai.run/v2/audio/speech_to_text_async
   #### GET : https://api.edenai.run/v2/audio/speech_to_text_async/{public_id}
   
  Before you start you should make sure to instal Microsoft.AspNet.WebApi.Client
  
```cs
using System;
using System.IO;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Json;
```

```cs
 public class Program
{    
    static void Main(string[] args)
    {
        Console.WriteLine("Speech To Text Started...");

        string filePath = "Your File Path Here";
        string apiKey = "Bearer 🔑 Your_API_Key";


        HttpClient httpClient = new HttpClient();

        //Speech To Text Job Lunch here
        MultipartFormDataContent content = new MultipartFormDataContent();

        content.Add(new StreamContent(File.OpenRead(filePath)),"file",Path.GetFileName(filePath));
        content.Add(new StringContent("microsoft"),"providers");
        content.Add(new StringContent("en"), "language");

        HttpRequestMessage jobLunchRequest = new HttpRequestMessage(HttpMethod.Post, new Uri("https://api.edenai.run/v2/audio/speech_to_text_async"));
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
 
        //Speech to text get job results here

        //waiting until speech processing is finished to get the results (we make a request every 5 seconds)
        while (true)
        {
            HttpRequestMessage jobResultsRequest = new HttpRequestMessage(HttpMethod.Get, new Uri($"https://api.edenai.run/v2/audio/speech_to_text_async/{public_id}"));
            jobResultsRequest.Headers.Add("Authorization", apiKey);
            using (HttpResponseMessage response = httpClient.Send(jobResultsRequest))
            {
                if (response.IsSuccessStatusCode)
                {
                    JobGetResults jobGetResults = response.Content.ReadFromJsonAsync<JobGetResults>().Result;
                    if (jobGetResults.Status == "finished" )
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
```
   * ## Quickstart with OCR Tables API
  
   #### POST : https://api.edenai.run/v2/ocr/ocr_tables_async
   #### GET : https://api.edenai.run/v2/ocr/ocr_tables_async/{public_id}
   
  Before you start you should make sure to instal Microsoft.AspNet.WebApi.Client
  
  ```cs
  using System;
using System.IO;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Json;
```

```cs

public class Program
{

    static void Main(string[] args)
    {
        Console.WriteLine("OCR Tables Started...");

        string filePath = "Your File Path Here";
        string apiKey = "Bearer 🔑 Your_API_Key";


        HttpClient httpClient = new HttpClient();

        //OCR tables Job Lunch here
        MultipartFormDataContent content = new MultipartFormDataContent();

        content.Add(new StreamContent(File.OpenRead(filePath)), "file", Path.GetFileName(filePath));
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
```

  
  
   
