using KioskUpdater.Model;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KioskUpdater
{
    public class SyncKioskCollection
    {


        public static async Task SyncKioskData()
        {

            string apiUrl = ConfigurationManager.AppSettings["ApiUrl"]; // Replace with your API URL
            var dbdata = new DatabaseHelper();
            List<KioskDonationCollection> donations = dbdata.GetKioskDonationCollections();

            // Send data to another application
            //  string apiUrl = "http://localhost:8000/services/SHJC/process"; // Replace with your API URL
            if (donations.Count > 0)
            {
                Logger.Log("Sync Kiosk donation Process started");
                await SendDonationsToApi(donations, apiUrl);
                //Thread.Sleep(60000);
                Logger.Log("Sync Kiosk donation Process completed");
            }
        }

        private static async Task SendDonationsToApi(List<KioskDonationCollection> donations, string apiUrl)
        {

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(apiUrl);

                var parameters = new Request
                {
                    Type = "SyncWebKioskCollectionWithKioskMachine",
                    Value = new { KioskDonationCollection = donations }
                };

                // Serialize the payload to JSON
                var json = JsonConvert.SerializeObject(parameters);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Post the JSON data to the API
                HttpResponseMessage response = client.PostAsync("/services/SHJC/process", content).GetAwaiter().GetResult();
                Thread.Sleep(60000);
                if (response.IsSuccessStatusCode)
                {

                    string resultContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    Console.WriteLine("Data successfully sent. Response from API: " + resultContent);

                    // Deserialize the resultContent into an ApiResponse object
                    var responseObject = JsonConvert.DeserializeObject<ApiResponse>(JsonConvert.DeserializeObject<dynamic>(resultContent).Value.ToString());

                    List<ReturnModel> modelresult = new List<ReturnModel>();
                    modelresult = responseObject.ReturnModel;

                    var dbdata = new DatabaseHelper();

                    if (modelresult != null && modelresult.Count > 0)
                    {
                        // Update the IsProcessed column or other columns based on the returnItem
                        dbdata.UpdateKioskDonationCollection(modelresult);
                    }
                }
                else
                {
                    // Console.WriteLine($"Failed to send data. Status Code: {response.StatusCode}");
                    Logger.Log($"Failed to send data. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Error occured: {ex.Message}");

            }

        }

        public class ReturnModel
        {
            public Guid CollectionID { get; set; }
            public string TransactionID { get; set; }
            public Guid ProjectORTemplateId { get; set; }
            public string FromModule { get; set; }
            public bool IsProcessed { get; set; }
            public int? KioskId { get; set; }
            public string ApprovalCode { get; set; }
        }

        public class ApiResponse
        {
            public bool Status { get; set; }
            public string Message { get; set; }
            public List<ReturnModel> ReturnModel { get; set; }
        }




    }
}
