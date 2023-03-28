using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

public class CustomTextClassification
{
    static void Main(string[] args)
    {

        HttpClient httpClient = new HttpClient();



        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.edenai.run/v2/text/custom_classification"),
            Headers =
            {
               { "accept", "application/json" },
            },
            Content = new StringContent("{\"providers\": \"openai\"," +
            " \"labels\": [\"span\", \"not spam\"]," +
            "\"texts\": [\"Confirm your email address\",\"hey i need u to send some $\"]," +
            "\"examples\": [[ \"I need help please wire me $1000 right now\", \"spam\" ]," +
            "[ \"Dermatologists dont like her!\", \"spam\" ]," +
            "[ \"Pre-read for tomorrow\", \"not spam\" ]," +
            "[ \"Your parcel will be delivered today\", \"not spam\" ]]}")
            {
                Headers =
                {
                  ContentType = new MediaTypeHeaderValue("application/json")
                }
            }
        };


        //pass your api key here 
        request.Headers.Add("Authorization", "Bearer your_api_key_here");

        using (HttpResponseMessage response = httpClient.Send(request))
        {
            JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine((json).ToString());

            if (!response.IsSuccessStatusCode)
                Console.WriteLine(response.Content);
        }

        Console.ReadLine();

    }
}