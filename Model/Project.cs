using SQLite;
using System;

namespace KioskUpdater.Model
{
    //public class Project
    //{        
    //    public Guid uId { get; set; }
    //    public Guid projectORTemplateId { get; set; }
    //    public string projectNameAr { get; set; }
    //    public string projectNameEn { get; set; }
    //    public string projectDescriptionAr { get; set; }
    //    public string projectDescriptionEn { get; set; }
    //    public string fromModule { get; set; }
    //    public decimal totalCost { get; set; }
    //    public string projectType { get; set; }
    //    public string projectTypeAr { get; set; }
    //    public bool isActive { get; set; }
    //    public Guid insertedBy { get; set; }
    //    public DateTime insertedDate { get; set; }
    //    public Guid? updatedBy { get; set; }
    //    public DateTime? updatedDate { get; set; }
    //    public bool isDeleted { get; set; }
    //    public string deletedBy { get; set; }
    //    public DateTime? deletedDate { get; set; }    
    //    public decimal? collectionAmount { get; set; }
    //    public bool? isInternal { get; set; }
    //    public string projectImage { get; set; }
    //    public string projectImagePath { get; set; }        
    //}
    public class Project
    {
        public Guid uId { get; set; }
        public Guid? projectORTemplateId { get; set; }
        public string projectNameAr { get; set; }
        public string projectNameEn { get; set; }
        public string projectDescriptionAr { get; set; }
        public string projectDescriptionEn { get; set; }
        public string fromModule { get; set; }
        public decimal? totalCost { get; set; }
        public decimal? collectionAmount { get; set; }
        public string projectType { get; set; }
        public string projectTypeAr { get; set; }
        public bool? isInternal { get; set; }
        public string projectImage { get; set; }
        public string projectImagePath { get; set; }
        public bool? isActive { get; set; }
        public Guid? insertedBy { get; set; }
        public DateTime? insertedDate { get; set; }
        public Guid? updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public bool? isDeleted { get; set; }
        public Guid? deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }
        public Guid? projectTypeId { get; set; }
        public Guid? subContinentId { get; set; }
        public Guid? countryId { get; set; }
        public int capacity { get; set; }
        public decimal length { get; set; }
        public decimal width { get; set; }
        public Guid? roofTypeId { get; set; }
        public bool carpet { get; set; }
        public int bathRoom { get; set; }
        public bool soundSystem { get; set; }
        public bool ablation { get; set; }
        public int ablutionQuantity { get; set; }
        public bool isSponsorImam { get; set; }
        public bool withQuran { get; set; }
        public decimal depth { get; set; }
        public Guid? wellTypeId { get; set; }
        public Guid? floorTypeId { get; set; }
        public decimal wellTankVolume { get; set; }
        public Guid? managementTypeId { get; set; }
        public int hall { get; set; }
        public int totalRooms { get; set; }
        public Guid? organizationId { get; set; }
        public decimal contractorCost { get; set; }
        public bool isZeroPercentage { get; set; }
        public decimal percentage { get; set; }
        public decimal organizationPercentage { get; set; }
        public int duration { get; set; }
        public decimal difference { get; set; }
        public bool lightHouse { get; set; }
        public decimal lightHouseLength { get; set; }
        public bool kitchen { get; set; }
        public bool equipments { get; set; }
        public int statusId { get; set; }
        public bool isPartiallyFunded { get; set; }
        public string descriptionHtml { get; set; }
        public int contractorDuration { get; set; }
        public string contractDescriptionHtml { get; set; }
        public string comments { get; set; }
        public string commentsEnglish { get; set; }
        public int quantity { get; set; }
        public bool isInsight { get; set; }
        public bool isMosamia { get; set; }
        public bool isMaintanance { get; set; }
        public bool isManagementApproved { get; set; }
        public bool isOldImported { get; set; }
        public decimal? oldProjectCost { get; set; }
        public bool? isCreateWebProject { get; set; }
        public bool? isCreateSmartLinkProject { get; set; }
        public bool? isCampaignProject { get; set; }
        public Guid? webProjectId { get; set; }
        public bool? isCreateFromWeb { get; set; }
        public bool? isOfflineWebsiteProjectStatus { get; set; }
        public decimal? ministryPercent { get; set; }
        public string educationTypeIds { get; set; }
        public bool? isLibrary { get; set; }
        public bool? isManagementOffice { get; set; }
        public bool? isCanteen { get; set; }
        public bool? isPlayGround { get; set; }
        public int? examinationRoom { get; set; }
        public int? operationRoom { get; set; }
        public int? waitingRoom { get; set; }
        public int? wardNumber { get; set; }
        public bool? isKiosk { get; set; }
        public string requesterName { get; set; }
        public string requesterPhone { get; set; }
        public Guid? erpTemplateId { get; set; }
        public long? idNumber { get; set; }
        public string projectNumber { get; set; }
        public int? typeId { get; set; }
        public decimal? unitPrice { get; set; }
        public decimal? targeted { get; set; }
        public Guid? webSiteId { get; set; }
        public bool? isAltakamul { get; set; }
        public long? altakamulProjectId { get; set; }
        public int? categoryId { get; set; }
        public string countryNameAr { get; set; }
        public string countryNameEn { get; set; }
        public int? subCategoryId { get; set; }
        public string image { get; set; }
        public int? status { get; set; }
        public bool? isSmartLink { get; set; }
        public int? smartLinkId { get; set; }
        public int? receivedQuantity { get; set; }
        public int? leftQuantity { get; set; }
        public decimal? receivedCollection { get; set; }
        public decimal? leftCollection { get; set; }
        public decimal? smartLinkCollection { get; set; }
        public decimal? webSiteCollection { get; set; }
        public decimal? erpCollection { get; set; }
        public decimal? altakmulCollection { get; set; }
        public bool? isActiveERP { get; set; }
        public bool? isActiveWebSite { get; set; }
        public bool? isActiveSmartLink { get; set; }
        public int? ecouponId { get; set; }
        public int? price { get; set; }
        public bool? isAlsoForCouponApp { get; set; }
        public string imagePathForCouponApp { get; set; }
        public Guid? attachmentStreamId { get; set; }
        public DateTime? streamIdUpdatedOn { get; set; }
    }


}



