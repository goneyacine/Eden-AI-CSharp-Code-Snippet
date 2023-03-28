using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
public class QuestionAnswering
    {
    static void Main(string[] args)
    {

        HttpClient httpClient = new HttpClient();



        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.edenai.run/v2/text/question_answer"),
            Headers =
            {
               { "accept", "application/json" },
            },
            Content = new StringContent("{\"providers\": \"openai\", \"texts\":[\"Linux is a family of open-source Unix-like operating systems based on the Linux kernel, an operating system kernel first released on September 17, 1991, by Linus Torvalds.\", \"Just like Windows, iOS, and Mac OS, Linux is an operating system. \"],\"question\":\"What is a competitor of Linux?\", \"examples_context\":\"In 2017, U.S. life expectancy was 78.6 years.\", \"examples\":[[\"What is human life expectancy in the United States?\", \"78 years.\"]]}")
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
            Console.WriteLine((json["openai"]["answers"]).ToString());

            if (!response.IsSuccessStatusCode)
                Console.WriteLine(response.Content);
        }

        Console.ReadLine();

    }
}