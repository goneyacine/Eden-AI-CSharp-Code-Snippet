using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

public class TextToSpeech
{
        static void Main(string[] args)
        {

        HttpClient httpClient = new HttpClient();


        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.edenai.run/v2/audio/text_to_speech"),
            Headers =
            {
               { "accept", "application/json" },
            },
            Content = new StringContent("{ \"providers\": \"google\", \"language\": \"en-US\", \"option\":\"MALE\", \"text\": \"this is a test\"}")
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
            if (response.IsSuccessStatusCode)
            {
                JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                byte[] bytes = Convert.FromBase64String((string)json["google"]["audio"]); // convert string to byte array

                //saving audio file
                using (FileStream fs = new FileStream("audio_file.wav", FileMode.CreateNew))
                {
                    fs.Write(bytes, 0, bytes.Length); 
                }
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