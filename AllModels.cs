using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BHSNCJW
{
    public class AllModels
    {
        //
    }
    public class Response
    {
        public string MessageCode { get; set; }
        public string MessageText { get; set; }
    }
    public class EmployeeResponse
    {
        public string MessageCode { get; set; }
        public string MessageText { get; set; }
        public string EmployeeCode { get; set; }
        public string EmpEntryID { get; set; }
    }

    public class modAuthTokenRequest
    {
        [Required]
        //[StringLength(12, ErrorMessage = "OEM_Partner_Id length can't be more than 12.")]
        public string UserID { get; set; }

        [Required]
        //[StringLength(8, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        public string Password { get; set; }
    }//eof
    public class modAuthTokenRequestWithSiteID
    {
        [Required]
        //[StringLength(12, ErrorMessage = "OEM_Partner_Id length can't be more than 12.")]
        public string UserID { get; set; }

        [Required]
        //[StringLength(8, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        //[StringLength(8, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        public string SITEID { get; set; }

    }//eof
    public class JWTResponse
    {
        public string MessageCode { get; set; }
        public string MessageText { get; set; }
        public string JWTToken { get; set; }

    }

    public class modEmployee
    {
        [Required]
        public string EmployeeID { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        public string EmployeeDOB { get; set; }
        public string EmployeeDesig { get; set; }
        public string EmployeeSalary { get; set; }
    }//eof

    #region Model for UserController
    public class CreateFMUser()
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserGroup { get; set; }
        [Required]
        public string UserLocation { get; set; }
        [Required]
        public string UpdateOn {  get; set; }
    }//eof
    public class ReqDeleteUser()
    {
        [Required]
        public string UserID { get; set; }
    }
    public class CreateSiteID()
    {
        [Required]
        public string SITEID { get; set; }
        [Required]
        public string SITE_GROUP { get; set; }
        [Required]
        public string UpdateOn { get; set; }
        [Required]
        public string updateby {  get; set; }
    }

    public class ReqDeleteSiteID()
    {
        [Required]
        public string SITEID { get; set; }
    }
    public class CreateFMUserAccSite()
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public List<SiteID> SITE_IDs { get; set; } = new List<SiteID>();
    }
    public class SiteID()
    {
        [Required]
        public string SITEID { get; set; }
    }
    public class FMUserAccSite()
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string SITEID { get; set; } 
    }

    public class FMUserAccManager()
    {
        [Required]
        public string USERID { get; set; }
        [Required]
        public List<FMUserManagerModules> MODULE_NAMES { get; set; }
    }
    public class FMUserManagerModules()
    {
        public string MODULENAME { get; set; }
    }
    public class GenIDResponse
    {
        public string MessageCode { get; set; }
        public string MessageText { get; set; }
        public string GenID { get; set; }

    }

    #endregion

    #region Model for MDController
    public class CreateFixedEntity()
    {
        [Required]
        public string EntityType { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Description { get; set; }
        public string Value { get; set; }
    }
    public class UserAccessModule()
    {
        [Required]
        public string ModuleName { get; set; }
    }
    public class CreateFixedEntityBySiteID()
    {
        [Required]
        public string EntityType { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Description { get; set; }
        public string Value { get; set; }
        [Required]
        public string SiteID { get; set; }
    }
    #endregion

    #region Model for TranController
    public class ActCodeSet()
    {
        [Required]
        public string actCode {  get; set; }
        [Required]
        public string acGroup {  get; set; }
        [Required]
        public decimal acSubCode {  get; set; }
        [Required]
        public string typeOfAct {  get; set; }
        [Required]
        public string acName {  get; set; }
        [Required]
        public string Descriptions {  get; set; }
        [Required]
        public string SiteId {  get; set; }
        [Required]
        public string UpdateOn { get; set; }
        [Required]
        public string EntryDate {  get; set; }

    }
    public class DeleteActCodeSet()
    {
        [Required]
        public string ActCodeSet { get; set; }
        [Required]
        public string siteID { get; set; }
    }

    public class CreateBudget()
    {
        [Required]
        public string budgetCode { get; set; }
        [Required]
        public string SiteId { get; set; }
        [Required]
        public string months { get; set; }
        [Required]
        public decimal years { get; set; }
        [Required]
        public string acGroup { get; set; }
        [Required]
        public string actCode { get; set; }
        [Required]
        public decimal amount { get; set; }
        [Required]
        public string UpdateOn { get; set; }
        [Required]
        public string EntryDate { get; set; }
        
    }
    public class DeleteBudget()
    {
        [Required]
        public string budgetCode { get; set; }
        [Required]
        public string siteID { get; set; }
    }

    public class CreateIncome()
    {
        [Required]
        public string entryId {get;set;}
        [Required]
        public string months { get;set;}
        [Required]
        public decimal years { get;set;}
        [Required]
        public string userAc {  get; set; }
        [Required]
        public decimal amount { get;set;}
        [Required]
        public string SiteId { get;set;}
        [Required]
        public string UpdateOn { get;set;}
        [Required]
        public string EntryDate { get;set;}


    }
    public class DeleteIncome()
    {
        [Required]
        public string entryId { get; set; }
        [Required]
        public string SiteId { get; set; }
    }

    public class CreateDailyExp()
    {
        [Required]
        public string expId { get; set; }
        [Required]
        public string userAc { get; set; }
        [Required]
        public string actCode { get; set; }
        [Required]
        public string payBy { get; set; }
        [Required]
        public decimal amount { get; set; }
        [Required]
        public string remarks { get; set; }
        [Required]
        public string SiteId { get; set; }
        [Required]
        public string UpdateOn { get; set; }
        [Required]
        public string EntryDate { get; set; }
    }
    public class DeleteDailyExp()
    {
        [Required]
        public string expId { get; set; }
        [Required]
        public string SiteId { get; set; }
    }
    public class modSQLResponse
    {
        public string MessageCode { get; set; }
        public string MessageText { get; set; }
        public string ReturnRec { get; set; }
    }//eof
    public class modSQLRequest
    {
        [Required]
        public string SQLString { get; set; }
    }//eof

    #endregion

    #region For Info Controller
    public class HostInfo()
    {
        [Required]
        public string Sys_Config_PIN { get; set; }
    }
    #endregion

    #region Model For Employee Update
    public class modEmployeeUpdate()
    {
        [Required]
        public string EmployeeCode { get; set; }
        [Required]
        public string Mode { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string? EmployeeImageName { get; set; }
        [Required]
        public string DateAdded { get; set; }
        [Required]
        public string AddedBy { get; set; }
        [Required]
        public string UpdateOn { get; set; }
        [Required]
        public string UpdateBy { get; set; }
        [Required]
        public string SiteId { get; set; }
    }

    public class modEmployeeDelete()
    {
        [Required]
        public string EmpEntryID { get; set; }
        [Required]
        public string SiteId { get; set; }
    }
    public class modFileName()
    {
        public string FileName { get; set; }

    }
    public class modUploadFile()
    {
        public IFormFile file { get; set; }
        public string EmployeeImageName { get; set; }
    }

    #endregion
}
