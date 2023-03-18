using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class FileTranscriber
{
    public static void TranscribeFiles(string inputFolder, string extention, string outputFolder, string provider, string lang, string apiKey, string lunchSpeechUrl, string getSpeechUrl, int timeout)
    {
        //finding all files with the specific extension in the inputFolder
        string[] files = Directory.GetFiles(inputFolder, extention);


        HttpClient httpClient = new HttpClient();
        List<string> public_IDs = new List<string>();

        //lunching all speech jobs
        foreach (string file in files)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();

            content.Add(new StreamContent(File.OpenRead(file)), "file", Path.GetFileName(file));
            content.Add(new StringContent(provider), "providers");
            content.Add(new StringContent(lang), "language");

            HttpRequestMessage jobLunchRequest = new HttpRequestMessage(HttpMethod.Post, new Uri(lunchSpeechUrl));
            jobLunchRequest.Content = content;


            jobLunchRequest.Headers.Add("Authorization", apiKey);


            using (HttpResponseMessage response = httpClient.Send(jobLunchRequest))
            {
                if (response.IsSuccessStatusCode)
                {
                    JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                    public_IDs.Add((string)json["public_id"]);
                }
            }

        }

        //getting all speech jobs results and saving them to the output folder
        while (true)
        {

            foreach (string id in public_IDs)
            {
                HttpRequestMessage jobResultsRequest = new HttpRequestMessage(HttpMethod.Get, new Uri($"{getSpeechUrl}{id}"));
                jobResultsRequest.Headers.Add("Authorization", apiKey);
                using (HttpResponseMessage response = httpClient.Send(jobResultsRequest))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                        if ((string)json["status"] == "finished")
                        {
                            string outputPath = Path.Combine(outputFolder, $"{Path.GetFileName(files[public_IDs.IndexOf(id)])} .txt");

                            using (FileStream fs = new FileStream(outputPath, FileMode.Create))
                            {
                                using (StreamWriter writer = new StreamWriter(fs))
                                {
                                    writer.Write((string)json["results"][provider]["text"]);
                                }
                            }
                            public_IDs.Remove(id);
                            //we break the loop to start again
                            break;
                        }
                    }


                }
            }
            if (public_IDs.Count == 0)
                break;

            Thread.Sleep(timeout);

        }
        Console.ReadLine();
    }
}

