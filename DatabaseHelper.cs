using KioskUpdater.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using static KioskUpdater.SyncKioskCollection;

namespace KioskUpdater
{
    public class DatabaseHelper
    {
        private string dbFilePath = Path.Combine(ConfigurationManager.AppSettings["databasepath"], "app.db");
        public void InitializeDatabase()
        {
            // Initialize SQLite connection and create tables if they do not exist
            using (var connection = new SQLiteConnection(dbFilePath))
            {
                connection.CreateTable<Footer>();
                connection.CreateTable<Header>();
                connection.CreateTable<Video>();
                connection.CreateTable<Project>();
            }
        }
        public void DeleteRecords()
        {
            InitializeDatabase();

            using (var connection = new SQLiteConnection(dbFilePath))
            {
                connection.DeleteAll<Footer>();
                connection.DeleteAll<Header>();
                connection.DeleteAll<Video>();
                connection.DeleteAll<Project>();
            }
        }
        public List<Footer> GetFooters()
        {
            // Retrieve all records from tbl_Footer
            using (var connection = new SQLiteConnection(dbFilePath))
            {
                return connection.Table<Footer>().ToList();
            }
        }

        public List<Header> GetHeaders()
        {
            // Retrieve all records from tbl_Header
            using (var connection = new SQLiteConnection(dbFilePath))
            {
                return connection.Table<Header>().ToList();
            }
        }

        public List<Video> GetVideos()
        {
            // Retrieve all records from tbl_Videos
            using (var connection = new SQLiteConnection(dbFilePath))
            {
                return connection.Table<Video>().ToList();
            }
        }
        public List<Project> GetProjects()
        {
            // Retrieve all records from tbl_Videos
            using (var connection = new SQLiteConnection(dbFilePath))
            {
                return connection.Table<Project>().ToList();
            }
        }

        public List<KioskDonationCollection> GetKioskDonationCollections()
        {
            //int TimeTake = int.Parse(ConfigurationManager.AppSettings["TimeInterval"]);
            //int interval = TimeTake * 60 * 1000;
            // Retrieve all records from tbl_Videos
            using (var connection = new SQLiteConnection(dbFilePath))
            {
                return connection.Table<KioskDonationCollection>().Where(x=> x.IsProcessed==false || x.IsProcessed==null).ToList();
               // return connection.Table<KioskDonationCollection>().ToList();
            }
        }

        public void UpdateLocalDatabaseHeader(Header apiData)
        {
            var localData = GetHeaders();
            var apiHelper = new ApiHelper();
            using (var db = new SQLiteConnection(dbFilePath))
            {

                var localRow = localData.Find(l => l.Id == apiData.Id);
                if (localRow != null)
                {
                    if (apiData.centerImagePath != localRow.centerImagePath)
                    {
                        db.Update(apiData);

                    }
                }
                else
                {
                    db.Insert(apiData);
                }

            }
        }
        public void UpdateLocalDatabaseFooter(Footer apiData)
        {
            var localData = GetFooters();

            using (var db = new SQLiteConnection(dbFilePath))
            {

                var localRow = localData.Find(l => l.Id == apiData.Id);
                if (localRow != null)
                {
                    if (apiData.centerImagePath != localRow.centerImagePath || apiData.projectNameAr != localRow.projectDescriptionAr || apiData.projectDescriptionEn != localRow.projectDescriptionEn || apiData.projectDescriptionAr != localRow.projectDescriptionAr || apiData.projectDescriptionEn != localRow.projectDescriptionEn || apiData.projectId != localRow.projectId || apiData.totalCost != localRow.totalCost || apiData.collectionAmount != localRow.collectionAmount || apiData.isActive != localRow.isActive)
                    {
                        db.Update(apiData);
                    }
                }
                else
                {
                    db.Insert(apiData);
                }

            }
        }
        public void UpdateLocalDatabaseVideo(List<Video> apiData)
        {
            var localData = GetVideos();

            using (var db = new SQLiteConnection(dbFilePath))
            {
                foreach (var apiRow in apiData)
                {
                    var localRow = localData.Find(l => l.Id == apiRow.Id);
                    if (localRow != null)
                    {
                        if (apiRow.videoLink != localRow.videoLink || apiRow.videoName != localRow.videoName)
                        {
                            db.Update(apiRow);
                        }
                    }
                    else
                    {
                        db.Insert(apiRow);
                    }
                }
            }
        }

        public void UpdateLocalDatabaseProject(List<Project> apiData)
        {
            var localData = GetProjects();

            using (var db = new SQLiteConnection(dbFilePath))
            {
                foreach (var apiRow in apiData)
                {
                    var localRow = localData.Find(l => l.uId == apiRow.uId);
                    if (localRow != null)
                    {
                        if (apiRow.projectNameEn != localRow.projectNameEn || apiRow.projectDescriptionEn != localRow.projectDescriptionEn || apiRow.projectNameAr != localRow.projectNameAr || apiRow.projectDescriptionAr != localRow.projectDescriptionAr || apiRow.projectORTemplateId != localRow.projectORTemplateId || apiRow.projectImage != localRow.projectImage || apiRow.collectionAmount != localRow.collectionAmount || apiRow.isInternal != localRow.isInternal || apiRow.isActive != localRow.isActive || apiRow.countryNameEn != localRow.countryNameEn)
                        {
                            db.Update(apiRow);
                        }
                    }
                    else
                    {
                        db.Insert(apiRow);
                    }
                }
            }
        }




 





        public void UpdateKioskDonationCollection(List<ReturnModel> apiData)
        {
            // Fetch local data from SQLite
            var localData = GetKioskDonationCollections();
          
            using (var db = new SQLiteConnection(dbFilePath))
            {
               // db.Open(); // Open the SQLite connection once at the start

                foreach (var apiRow in apiData)
                {
                    // Check if the local data contains a record with the same CollectionID
                    var localRow = localData.Find(l => l.KioskId == apiRow.KioskId && l.ApprovalCode==apiRow.ApprovalCode);

                    if (localRow != null)
                    {
                        localRow.IsProcessed= true;
                        localRow.ProcessDate = DateTime.Now;
                        db.Update(localRow);
                        // If the record exists, update the IsProcessed field

                    }
                    
                }

                 
            }
        }

    }
}
