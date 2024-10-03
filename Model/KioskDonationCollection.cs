using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskUpdater.Model
{
    public class KioskDonationCollection
    {
        [PrimaryKey]
        public Guid CollectionID { get; set; }
        public string ProjectName { get; set; }
        public string DonorName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string ProjectNameByDonor { get; set; }
        public decimal TotalAmount { get; set; }
        public string ItemId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionID { get; set; }
        public string AccountBrand { get; set; }
        public string AccountExpiry { get; set; }
        public string AccountNumber { get; set; }
        public string AccountToken { get; set; }
        public string ApprovalCode { get; set; }
        public string Comments { get; set; }
        public DateTime DateCreated { get; set; }
        public int? ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public bool IsKiosk { get; set; }
        public string KioskResponseString { get; set; }
        public int? KioskId { get; set; }
        public int? Quantity { get; set; }
        public Guid? ProjectTypeId { get; set; }
        public bool? IsPartialFunded { get; set; }
        public Guid? ProjectORTemplateId { get; set; }
        public string FromModule { get; set; }
        public bool? IsProcessed { get; set; }
        public DateTime? ProcessDate { get; set; }
        public string MachineName { get; set; }
    }
}
