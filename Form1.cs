using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KioskUpdater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // var databaseHelper2 = new DatabaseHelper();
            //  var KioskCollection = databaseHelper2.GetKioskDonationCollections();

            
            var apiHelper = new ApiHelper();
            var ApiHeaders = apiHelper.GetApiHeaderDataAsync();
            var ApiFooters = apiHelper.GetApiFooterDataAsync();
           // var ApiVideos = apiHelper.GetApiVideoDataAsync();
            var ApiProjects = apiHelper.GetApiProjectDataAsync();

            var databaseHelper = new DatabaseHelper();
            databaseHelper.InitializeDatabase();
            // databaseHelper.DeleteRecords();
            var comparer = new Comparer();
            comparer.CompareAndDownloadHeaderImages(ApiHeaders);
            comparer.CompareAndDownloadFooterImages(ApiFooters);
           // comparer.CompareAndDownloadVideo(ApiVideos);
            comparer.CompareAndDownloadProject(ApiProjects);


            databaseHelper.UpdateLocalDatabaseHeader(ApiHeaders);
            databaseHelper.UpdateLocalDatabaseFooter(ApiFooters);
           // databaseHelper.UpdateLocalDatabaseVideo(ApiVideos);
            databaseHelper.UpdateLocalDatabaseProject(ApiProjects);
        }

        
        private async  void btnSyncDonation_Click(object sender, EventArgs e)
        {
            try
            {
                 
                await SyncKioskCollection.SyncKioskData();
                
                MessageBox.Show("Kiosk data synchronized successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
