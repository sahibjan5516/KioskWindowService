using KioskUpdater.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskUpdater
{
    public class Comparer
    {
        private string directoryImagePath = ConfigurationManager.AppSettings["imagedistinationpath"];
        private string sourcepath = ConfigurationManager.AppSettings["sourcepath"];
               
        public void CompareAndDownloadHeaderImages(Header ApiHeaders)
        {
            
            try
            {
                var apiHelper = new ApiHelper();                

                List<string> fileNames = GetFileNames(directoryImagePath);

                              
                    if (!fileNames.Contains(ApiHeaders.centerImage))
                    {
                        apiHelper.DownloadFileAsync(sourcepath + ApiHeaders.centerImage, ConfigurationManager.AppSettings["imagedistinationpath"] + ApiHeaders.centerImage);
                    }                   
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

        }

        public void CompareAndDownloadFooterImages(Footer ApiFooters)
        {

            try
            {
                var apiHelper = new ApiHelper();
                

                List<string> fileNames = GetFileNames(directoryImagePath);
                          
                   
                    if (!fileNames.Contains(ApiFooters.centerImage))
                    {
                        apiHelper.DownloadFileAsync(sourcepath + ApiFooters.centerImage, ConfigurationManager.AppSettings["imagedistinationpath"] + ApiFooters.centerImage);
                    }                 
             
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

        }
        public void CompareAndDownloadVideo(List<Video> ApiVideos)
        {

            try
            {
                var apiHelper = new ApiHelper();
                

                List<string> fileNames = GetFileNames(directoryImagePath);

                foreach (Video fileName in ApiVideos.ToList())
                {
                    if (!fileNames.Contains(fileName.videoLink.Replace(@"http:\\10.10.100.15:9999\KioskMediaData\", "")))
                    {
                        apiHelper.DownloadFileAsync(fileName.videoLink, ConfigurationManager.AppSettings["imagedistinationpath"] + fileName.videoLink.Replace(@"http:\\10.10.100.15:9999\KioskMediaData\", ""));
                    }                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

        }
        public void CompareAndDownloadProject(List<Project> ApiProjects)
        {

            try
            {
                var apiHelper = new ApiHelper();


                List<string> fileNames = GetFileNames(directoryImagePath);

                foreach (Project fileName in ApiProjects.ToList())
                {
                    if (!fileNames.Contains(fileName.projectImage))
                    {
                        apiHelper.DownloadFileAsync(sourcepath + fileName.projectImage, ConfigurationManager.AppSettings["imagedistinationpath"] + fileName.projectImage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

        }

        static List<string> GetFileNames(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException("The specified directory does not exist: " + directoryPath);
            }

            string[] files = Directory.GetFiles(directoryPath);
            return new List<string>(files);
        }
    }
}
