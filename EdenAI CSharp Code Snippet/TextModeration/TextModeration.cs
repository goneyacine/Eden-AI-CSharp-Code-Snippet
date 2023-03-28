using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
public class TextModeration
    {
    static void Main(string[] args)
    {
        HttpClient httpClient = new HttpClient();



        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.edenai.run/v2/text/moderation"),
            Headers =
            {
               { "accept", "application/json" },
            },
            Content = new StringContent("{ \"providers\": \"openai\",\"text\":\"put your text here,\" ,\"language\" : \"en\"}")
            {
                Headers =
                {
                  ContentType = new MediaTypeHeaderValue("application/json")
                }
            }
        };


        //pass your api key here 
        request.Headers.Add("Authorization", "Bearer YOUR_API_KEY_HERE");


        using (HttpResponseMessage response = httpClient.Send(request))
        {
            JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine((json["openai"]["items"]).ToString());

            if (!response.IsSuccessStatusCode)
                Console.WriteLine(response.Content);
        }

        Console.ReadLine();

    }
}