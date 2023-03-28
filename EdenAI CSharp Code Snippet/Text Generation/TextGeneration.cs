using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

public class TextGeneration
{
        static void Main(string[] args)
        {
        HttpClient httpClient = new HttpClient();


        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.edenai.run/v2/text/generation"),
            Headers =
            {
               { "accept", "application/json" },
            },
            Content = new StringContent("{ \"providers\": \"openai\",\"text\": \"put your text here\",\"temprature\" : 0.1,\"max_tokens\":500}")
            {
                Headers =
                {
                  ContentType = new MediaTypeHeaderValue("application/json")
                }
            }
        };


        //pass your api key here 
        request.Headers.Add("Authorization", "Bearer Your_Api_Key_Here");


        using (HttpResponseMessage response = httpClient.Send(request))
        {
            JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine((string)json["openai"]["generated_text"]);

            if (!response.IsSuccessStatusCode)
                Console.WriteLine(response.Content);
        }

        Console.ReadLine();

    }
}