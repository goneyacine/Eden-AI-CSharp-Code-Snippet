using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
public class NamedEntityRecognition
    {
        static void Main(string[] args)
        {

        HttpClient httpClient = new HttpClient();



        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.edenai.run/v2/text/named_entity_recognition"),
            Headers =
            {
               { "accept", "application/json" },
            },
            Content = new StringContent("{ \"providers\": \"google\",\"text\":\"I was born in Lyon in February 1996\" ,\"language\" : \"en\"}")
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
            Console.WriteLine((json["google"]["items"]).ToString());

            if (!response.IsSuccessStatusCode)
                Console.WriteLine(response.Content);
        }

        Console.ReadLine();

    }
}