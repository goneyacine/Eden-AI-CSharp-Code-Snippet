//instal Microsoft.AspNet.WebApi.Client first 

using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

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