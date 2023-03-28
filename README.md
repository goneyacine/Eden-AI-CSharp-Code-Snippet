# EdenAI Code Snippet
### this repository contains code snippet and quickstart guide for EdenAI API with C#


## Content 
   - [Text](#text)
     - [Spell Check](#spell-check)
     - [Text Summarization](#text-summarization)
     - [Code Generation](#code-generation)
     - [Text Generation](#text-generation)
     - [Topic Extraction](#topic-extraction)
     - [Search](#text-search)
     - [Syntax Analysis](#syntax-analysis)
     - [Keyword Extraction](#keyword-extraction)
     - [Text Moderation](#text-moderation)
     - [Sentiment Analysis](#sentiment)
     - [Custom Entity Extraction](#custom-entity-extraction)
     - [Named Entity Recognition](#named-entity-recognition)
   - [Audio](#audio)
     - [Speech To Text](#speech-to-text)
     - [Text To Speech](#text-to-speech)
   - [OCR](#ocr)
     - [OCR Tables](#ocr-tables)


<a name="text"></a>
## Text 


<a name="spell-check"></a>
* ### Quickstart with Spell Check API

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
<a name="text-summarization"></a>
  * ### Quickstart with Text Summarization API
  
#### POST : https://api.edenai.run/v2/text/summarize
    
 ```cs
 
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
```
```cs

    public class TextSummarization
{
    static void Main(string[] args)
    {
        HttpClient httpClient = new HttpClient();


        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.edenai.run/v2/text/summarize"),
            Headers =
            {
               { "accept", "application/json" },
            },
            Content = new StringContent("{ \"providers\": \"openai\",\"language\": \"en\",\"text\": \"text to summarize here\"}")
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
                Console.WriteLine((string)json["openai"]["result"]);

                if(!response.IsSuccessStatusCode)
                Console.WriteLine(response.Content);
        }

        Console.ReadLine();
    }
}
```
  <a name="code-generation"></a>
 * ### Quickstart with Code Generation API
 
  #### POST : https://api.edenai.run/v2/text/code_generation
  
  ```cs
  using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
```
```cs

public class CodeGeneration
{
    static void Main(string[] args)
    {
        HttpClient httpClient = new HttpClient();


        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.edenai.run/v2/text/code_generation"),
            Headers =
            {
               { "accept", "application/json" },
            },
            Content = new StringContent("{ \"providers\": \"openai\",\"promot\": \"\",\"instruction\": \"write a code in c++ to calculate distance between to points in 4D space\",\"temprature\" : 0.1,\"max_tokens\":500}")
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
```
<a name="text-generation"></a>
* ### Quickstart with Text Generation API
  #### POST : https://api.edenai.run/v2/text/generation
  ```cs
  using System;
  using System.Net.Http;
  using System.Net.Http.Headers;
  using Newtonsoft.Json.Linq;
  ```
  ```cs
  
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
    ```
    <a name="topic-extraction"></a>
    * ### Quickstart with Topic Extraction API
    
    #### POST : https://api.edenai.run/v2/text/topic_extraction
    ```cs
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Newtonsoft.Json.Linq;
    ```
    ```cs
    public class TopicExtraction
    {
        static void Main(string[] args)
        {
        HttpClient httpClient = new HttpClient();


        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.edenai.run/v2/text/topic_extraction"),
            Headers =
            {
               { "accept", "application/json" },
            },
            Content = new StringContent("{ \"providers\": \"google\",\"text\": \"That actor on TV makes movies in Hollywood and also stars in a variety of popular new TV shows.\",\"language\" : \"en\"}")
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
            Console.WriteLine((json["google"]["items"]).ToString());

            if (!response.IsSuccessStatusCode)
                Console.WriteLine(response.Content);
        }

        Console.ReadLine();

    }
  }
  ```

<a name ="text-search"></a>

 * ### Quickstart with Search API
 
  #### POST : https://api.edenai.run/v2/text/search  
  
  ```cs
  using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
```
```cs
public class Search
    {
        static void Main(string[] args)
        {
        HttpClient httpClient = new HttpClient();

       

        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.edenai.run/v2/text/search"),
            Headers =
            {
               { "accept", "application/json" },
            },
            Content = new StringContent("{ \"providers\": \"openai\",\"texts\":[\"text one here , keyword\",\" text two here \"],\"query\" : \"keyword\"}")
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
```
<a name="syntax-analysis"></a>

* ### Quickstart with Syntax Analysis API
  #### POST : https://api.edenai.run/v2/text/syntax_analysis
  
```cs
  using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
```
```cs
public class SyntaxAnalysis
{
    static void Main(string[] args)
    {
        HttpClient httpClient = new HttpClient();



        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.edenai.run/v2/text/syntax_analysis"),
            Headers =
            {
               { "accept", "application/json" },
            },
            Content = new StringContent("{ \"providers\": \"google\",\"text\":\"put yourw texte heree,\" ,\"language\" : \"en\"}")
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
            Console.WriteLine((json["google"]["items"]).ToString());

            if (!response.IsSuccessStatusCode)
                Console.WriteLine(response.Content);
        }

        Console.ReadLine();

    }
}
```

<a name="keyword-extraction"></a>

* ### Quickstart with Keyword Extraction API

 #### POST : https://api.edenai.run/v2/text/keyword_extraction
```cs
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
```
```cs
public class KeyWordExtraction
{
    static void Main(string[] args)
    {
        HttpClient httpClient = new HttpClient();



        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.edenai.run/v2/text/keyword_extraction"),
            Headers =
            {
               { "accept", "application/json" },
            },
            Content = new StringContent("{ \"providers\": \"amazon\",\"text\":\"put your text here,\" ,\"language\" : \"en\"}")
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
            Console.WriteLine((json["amazon"]["items"]).ToString());

            if (!response.IsSuccessStatusCode)
                Console.WriteLine(response.Content);
        }

        Console.ReadLine();

    }
}

```

<a name="text-moderation"></a>
 * ### Quickstart with Text Moderation API
   #### POST : https://api.edenai.run/v2/text/moderation
   
```cs
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
```
```cs
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
```
<a name="sentiment"></a>

* ### Quickstart with Sentiment Analysics API
  #### POST : https://api.edenai.run/v2/text/sentiment_analysis
  
 ```cs
 using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
 ```
 ```cs
 public class SentimentAnalysis
{
    static void Main(string[] args)
    {
        HttpClient httpClient = new HttpClient();



        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.edenai.run/v2/text/sentiment_analysis"),
            Headers =
            {
               { "accept", "application/json" },
            },
            Content = new StringContent("{ \"providers\": \"google\",\"text\":\"put your text here,\" ,\"language\" : \"en\"}")
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
            Console.WriteLine((json["google"]["items"]).ToString());

            if (!response.IsSuccessStatusCode)
                Console.WriteLine(response.Content);
        }

        Console.ReadLine();

    }

}
 ```
 <a name="custom-entity-extraction"></a>
 * ### Quickstart with Custom Entity Extraction API
   #### POST : https://api.edenai.run/v2/text/custom_named_entity_recognition
   
```cs
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
```
```cs
public class CustomEntityExtraction
    {
    static void Main(string[] args)
    {
        HttpClient httpClient = new HttpClient();



        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.edenai.run/v2/text/custom_named_entity_recognition"),
            Headers =
            {
               { "accept", "application/json" },
            },
            Content = new StringContent("{ \"providers\": \"openai\",\"entities\": [\"person\", \"date\"],\"text\":\"I was born in Lyon in February 1996\" ,\"language\" : \"en\"}")
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
            Console.WriteLine((json["openai"]["items"]).ToString());

            if (!response.IsSuccessStatusCode)
                Console.WriteLine(response.Content);
        }

        Console.ReadLine();

    }

}
```
<a name="named-entity-recognition"></a>

  * ### Quickstart with Named Entity Recognition API
  
    #### POST : https://api.edenai.run/v2/text/named_entity_recognition
    
```cs
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
```
```cs
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
```
   

<a name="audio"></a> 
## Audio
   
<a name="speech-to-text"></a>
  * ### Quickstart with Speech To Text API
  
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

<a name="text-to-speech"></a>
  * ### Quickstart with Text To Speech API
  
   #### POST : https://api.edenai.run/v2/audio/text_to_speech
   ```cs
   using System;
   using System.Net.Http;
   using System.Net.Http.Headers;
   using Newtonsoft.Json.Linq;
   ```
   ```cs
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
   ```

<a name="ocr"></a>
## OCR
  
<a name="ocr-tables"></a>
   * ### Quickstart with OCR Tables API
  
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

  
  
   
