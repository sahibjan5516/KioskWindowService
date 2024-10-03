using KioskUpdater.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KioskUpdater
{
    public class ApiHelper
    {
        private string apiUrl = ConfigurationManager.AppSettings["ApiUrl"]; // Replace with your API URL

        

        public List<Footer> GetApiFooterDataAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                // Set the base address
                client.BaseAddress = new Uri(apiUrl);
                var parameters = new Request
                {
                    Type = "getAllKioskFooters",
                    Value = { }                
                };
                // Serialize the parameters to JSON
                string json = JsonConvert.SerializeObject(parameters);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // Send the POST request
                HttpResponseMessage response = client.PostAsync("/services/SHJC/gridprocess", content).GetAwaiter().GetResult();
                
                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                  //  return JsonConvert.DeserializeObject<List<Footer>>(responseContent).FirstOrDefault(h => h.isActive == true);
                    return JsonConvert.DeserializeObject<List<Footer>>(responseContent);
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                    return null;
                }                
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error calling API: {ex.Message}");
                return null;
            }
        }
        public List<Header> GetApiHeaderDataAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                // Set the base address
                client.BaseAddress = new Uri(apiUrl);
                var parameters = new Request
                {
                    Type = "getAllKioskHeaders",
                    Value = { }
                };
                // Serialize the parameters to JSON
                string json = JsonConvert.SerializeObject(parameters);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // Send the POST request
                HttpResponseMessage response =  client.PostAsync("/services/SHJC/gridprocess", content).GetAwaiter().GetResult();

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string responseContent =  response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                  //  return JsonConvert.DeserializeObject<List<Header>>(responseContent).FirstOrDefault(h=>h.isActive==true);
                    return JsonConvert.DeserializeObject<List<Header>>(responseContent);
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                    return null;
                }                
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error calling API: {ex.Message}");
                return null;
            }
        }
        public  List<Video> GetApiVideoDataAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                // Set the base address
                client.BaseAddress = new Uri(apiUrl);
                var parameters = new Request
                {
                    Type = "getAllKioskVedios",
                    Value = { }
                };
                // Serialize the parameters to JSON
                string json = JsonConvert.SerializeObject(parameters);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // Send the POST request
                HttpResponseMessage response = client.PostAsync("/services/SHJC/gridprocess", content).GetAwaiter().GetResult();

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return JsonConvert.DeserializeObject<List<Video>>(responseContent);
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                    return null;
                }                
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error calling API: {ex.Message}");
                return null;
            }
        }

        public List<Project> GetApiProjectDataAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                // Set the base address
                client.BaseAddress = new Uri(apiUrl);
                var parameters = new Request
                {
                    Type = "GetKioskPublishedProjectsList",
                    Value = new
                    {
                        IsInternal = true,
                        ProjectType = ""
                    }
                };
                // Serialize the parameters to JSON
                string json = JsonConvert.SerializeObject(parameters);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // Send the POST request
                HttpResponseMessage response = client.PostAsync("/services/SHJC/gridprocess", content).GetAwaiter().GetResult();

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return JsonConvert.DeserializeObject<List<Project>>(responseContent);
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error calling API: {ex.Message}");
                return null;
            }
        }
        public void DownloadFileAsync(string url, string destinationPath)
        {
            destinationPath = destinationPath.Replace("http://10.10.100.15:9999/KioskMediaData\\", "");
            url = url.Replace('\\','/');
            try
            {
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage response = httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).GetAwaiter().GetResult();
                    response.EnsureSuccessStatusCode();

                    using (Stream contentStream = response.Content.ReadAsStreamAsync().GetAwaiter().GetResult(),
                           fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        contentStream.CopyTo(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    } 
}
