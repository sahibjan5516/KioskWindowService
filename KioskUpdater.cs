using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace KioskUpdater
{
    public partial class KioskUpdater : ServiceBase
    {
        private Timer _timer;
        private Timer _syncTimer;

        public KioskUpdater()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            int TimeTake = int.Parse(ConfigurationManager.AppSettings["TimeInterval"]);
            // Initialize and start the timer
            _timer = new Timer();
            _timer.Interval = 60000; // Set the timer interval (e.g., 60,000 milliseconds = 1 minute)
            _timer.Elapsed += ProcessUpdate;
            _timer.AutoReset = true;
            _timer.Enabled = true;


            // Initialize and start the sync timer
            _syncTimer = new Timer();
            _syncTimer.Interval = TimeTake * 60 * 1000; // 10 minutes
            _syncTimer.Elapsed += SyncKioskData;
            _syncTimer.AutoReset = true;
            _syncTimer.Enabled = true;
        }

        protected override void OnStop()
        {
            // Stop the timer
            _timer.Stop();
            _timer.Dispose();

            _syncTimer.Stop();
            _syncTimer.Dispose();
        }
        private void ProcessUpdate(object sender, ElapsedEventArgs e)
        {
            try
            {
                Logger.Log("Service started");

                PreventSleepMode precentMode = new PreventSleepMode();
                var apiHelper = new ApiHelper();
                var ApiHeaders = apiHelper.GetApiHeaderDataAsync();
                var ApiFooters = apiHelper.GetApiFooterDataAsync();
                //var ApiVideos = apiHelper.GetApiVideoDataAsync();
                var ApiProjects = apiHelper.GetApiProjectDataAsync();

                var databaseHelper = new DatabaseHelper();
                databaseHelper.InitializeDatabase();

                //its for delete existing all records
                //databaseHelper.DeleteRecords();

                var comparer = new Comparer();
                comparer.CompareAndDownloadHeaderImages(ApiHeaders);
                comparer.CompareAndDownloadFooterImages(ApiFooters);
               // comparer.CompareAndDownloadVideo(ApiVideos);
                comparer.CompareAndDownloadProject(ApiProjects);


                databaseHelper.UpdateLocalDatabaseHeader(ApiHeaders);
                databaseHelper.UpdateLocalDatabaseFooter(ApiFooters);
              //  databaseHelper.UpdateLocalDatabaseVideo(ApiVideos);
                databaseHelper.UpdateLocalDatabaseProject(ApiProjects);


                

                precentMode.PreventSleep();
                precentMode.DisableWindowsUpdateService();



            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Email service = new Email();
                service.SendERPExceptionEmail(ex.ToString());
            }
        }


        private async void SyncKioskData(object sender, ElapsedEventArgs e)
        {
            try
            {
                Logger.Log("Syncing Kiosk Data Service started");
               await SyncKioskCollection.SyncKioskData();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Email service = new Email();
                service.SendERPExceptionEmail(ex.ToString());
            }
        }
    }
}
