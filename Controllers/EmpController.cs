using BHSAPIBaseNC8.JWTToken;
using BHSNCJW.BO;
using BHSNCJW;
using bplib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;

namespace FMNC8Api.Controllers
{
    public class EmpController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EmpController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [JWTToken]
        [HttpPost]
        [ActionName("UpdateEmployee")]
        [Route("FMNC8Api/Emp/UpdateEmployee")]
        public IActionResult UpdateEmployee([FromBody] modEmployeeUpdate dataKey)
        {
            EmployeeResponse returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            mngJWT mngApp = null;
            string genID = "";
            DataSet dsLocalSite = null;
            DataSet dsLocal = null;
            clsApplication objApp = null;
            string MODULE_NAME = "Employee Update.EDIT";
            clsGenID getGenId = new clsGenID();
            try
            {

                //filter of invalid character
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new EmployeeResponse();
                mngApp = new mngJWT();
                objApp = new clsApplication();
                getGenId = new clsGenID();

                UpdateLog("Call UpdateEmployee()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");

                //Verification of user access
                if (mngApp.GetUserIDAndCheckAccess(Request.Headers["Authorization"].ToString().Substring("Bearer ".Length), MODULE_NAME, out string userID) == false)
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "ModeleName: " + MODULE_NAME.ToUpper() + " for UserID: " + userID + " Access Denided." + strMsg;
                    return Ok(returnRes);
                }
                UpdateLog("UserID:" + userID + " ModuleName:" + MODULE_NAME.ToUpper(), DateTime.Now.ToString("MM-dd-yyyy") + "-Log");


                //Verification of User Input
                if (DATA_OK == false)
                {
                    objApp.GetFMSiteID(out dsLocalSite, dataKey.SiteId);
                    if (dsLocalSite.Tables[0].Rows.Count == 0)
                    {
                        strMsg += "SiteID not Found.";
                    }
                    if (!clsWebLib.isEmail(dataKey.EmailAddress))
                    {
                        strMsg += "Please provide a valid email address...";
                    }
                    
                    if (dataKey.Mode == "ADD")
                    {
                        objApp.GetEmployeeEmail(dataKey.EmailAddress, out dsLocal);
                        if (dsLocal.Tables[0].Rows.Count > 0)
                        {
                            strMsg += "This email address alreay exits, please use diffrent email address...";
                        }
                    }
                    
                    if (!clsWebLib.IsValidPhoneNumber(dataKey.PhoneNumber))
                    {
                        strMsg += "The phone number must contain only numeric characters...";
                    }
                    if (!clsWebLib.IsDateOK3(dataKey.UpdateOn))
                    {
                        strMsg += "Update Date format should be (dd-MMM-yyyy)...";
                    }
                    if (!clsWebLib.IsDateOK3(dataKey.DateAdded))
                    {
                        strMsg += "DateAdded format should be (dd-MMM-yyyy)...";
                    }

                    if (dataKey.Mode == "ADD")
                    {
                        objApp.GetEmployeeByCode(dataKey.EmployeeCode, dataKey.SiteId, out dsLocal);
                        if (dsLocal.Tables[0].Rows.Count > 0)
                        {
                            strMsg += "This employee code alreay exits please use diffrent employee code...";
                        }
                    }
                    if (dataKey.Mode == "EDIT")
                    {
                        objApp.GetEmployeeByCode(dataKey.EmployeeCode, dataKey.SiteId, out dsLocal);
                        if (dsLocal.Tables[0].Rows.Count <= 0)
                        {
                            strMsg += "This employee code does not exits please use diffrent employee code...";
                        }
                    }
                    if(dataKey.Mode !="ADD" && dataKey.Mode != "EDIT")
                    {
                        strMsg += "This Mode only accept ADD or EDIT.";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }
                //Update Database on verifiaction successful
                if (DATA_OK == true)
                {

                    if (dataKey.Mode.ToUpper() == "ADD")
                    {
                        getGenId.GenID(System.DateTime.Now.ToShortDateString().ToString(), "Employee", out genID);
                        if (genID != "")
                        {
                            genID="E" + genID;
                        }
                    }

                    SaveEmployee(dataKey,genID);
                    objApp.GetEmployeeByCode(dataKey.EmployeeCode, dataKey.SiteId, out dsLocal);

                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful.Employee Update Successfully.";
                    returnRes.EmpEntryID = dsLocal.Tables[0].Rows[0]["EmpEntryID"].ToString();
                    returnRes.EmployeeCode = dsLocal.Tables[0].Rows[0]["EmployeeCode"].ToString();
                }
                else
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "API Execution failed.Employee not saved." + strMsg;
                }
                UpdateLog(returnRes.MessageText, DateTime.Now.ToString("MM-dd-yyyy") + "-Log");

                return Ok(returnRes);
            }
            catch (Exception ex)
            {
                UpdateLog(ex.Message, DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
                return BadRequest(ex.Message);
            }
            finally
            {
                dsLocal = null;
                dsLocalSite = null;
                objApp = null;
            }

        }

        [JWTToken]
        [HttpPost]
        [ActionName("DeleteEmployee")]
        [Route("FMNC8Api/Emp/DeleteEmployee")]
        public IActionResult DeleteEmployee([FromBody] modEmployeeDelete dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            DataSet dsLocal = null;
            string MODULE_NAME = "Employee Update.DELETE";
            clsApplication objApp = null;
            mngJWT mngApp = null;
            try
            {
                //filter of invalid character
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();
                objApp = new clsApplication();
                mngApp = new mngJWT();
                UpdateLog("Call DeleteEmployee()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
                //Verify Model state
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }
                //Verification of user access
                if (mngApp.GetUserIDAndCheckAccess(Request.Headers["Authorization"].ToString().Substring("Bearer ".Length), MODULE_NAME, out string userID) == false)
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "ModeleName: " + MODULE_NAME.ToUpper() + " for UserID: " + userID + " Access Denided." + strMsg;
                    return Ok(returnRes);
                }
                UpdateLog("UserID:" + userID + " ModuleName:" + MODULE_NAME.ToUpper(), DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
                //Verification of User Input
                if (DATA_OK == false)
                {

                    objApp.GetEmployeeByEntryID( dataKey.EmpEntryID.ToUpper(), dataKey.SiteId.ToUpper(), out dsLocal);
                    if (dsLocal.Tables[0].Rows.Count == 0)
                    {
                        strMsg += "Employee not found";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }

                if (DATA_OK == true)
                {
                    objApp.DeleteEmployeeByCode(dataKey.EmpEntryID, dataKey.SiteId);
                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful. Delete Employee successfully.";
                }
                else
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "API Execution failed. Delete Failed." + strMsg;
                }

                UpdateLog(returnRes.MessageText, DateTime.Now.ToString("MM-dd-yyyy") + "-Log");

                return Ok(returnRes);
            }
            catch (Exception ex)
            {
                UpdateLog(ex.Message, DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
                return BadRequest(ex.Message);
            }
            finally
            {
                //
            }

        }

        [JWTToken]
        [HttpPost]
        [ActionName("GetEmpImage")]
        [Route("FMNC8Api/Emp/GetEmpImage")]
        public IActionResult GetEmpImage([FromBody] modFileName modFile)
        {
            try
            {
                UpdateLog("Call GetEmpImage()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");       
                var basePath = _configuration.GetSection("DocumetFolder:CompanyDoc").Value;
                var extension = Path.GetExtension(modFile.FileName);
                if (string.IsNullOrEmpty(extension))
                {
                    modFile.FileName = modFile.FileName + ".JPG";
                }
                var filePath = Path.Combine(basePath, modFile.FileName);
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound(new { Message = "File not found." });
                }
                var contentType = "application/octet-stream";
                var fileExtension = Path.GetExtension(filePath).ToLower();

                var mimeTypes = new Dictionary<string, string>
                {
                    //{ ".txt", "text/plain" },
                    //{ ".pdf", "application/pdf" },
                    //{ ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
                    //{ ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
                    { ".jpg", "image/jpeg" },
                    //{ ".png", "image/png" }
                };

                if (mimeTypes.ContainsKey(fileExtension))
                {
                    contentType = mimeTypes[fileExtension];
                }

                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                UpdateLog("Get File " + modFile.FileName, DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
                return File(fileBytes, contentType, modFile.FileName);
            }
            catch (Exception ex)
            {
                UpdateLog("Get File " + modFile.FileName + ex.Message, DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
                return StatusCode(StatusCodes.Status400BadRequest, new { Message = $"Error: {ex.Message}" });
            }
        }

        [JWTToken]
        [HttpPost]
        [ActionName("UploadEmpImage")]
        [Route("FMNC8Api/Emp/UploadEmpImage")]
        public IActionResult UploadEmpImage(modUploadFile dataKey)
        {
            clsApplication objApp = null;
            Response returnRes = null;
            try
            {
                if (!ModelState.IsValid)
                {
                   return BadRequest("Invalid data");
                }
                objApp = new clsApplication();
                UpdateLog("Call UploadEmpImage()", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
                returnRes = new Response();
                if (dataKey.file == null || dataKey.file.Length == 0)
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "File is not selected or is empty.";
                    return Ok(returnRes);
                }
                var fullImageName = dataKey.EmployeeImageName + ".jpg";
                var basePath = _configuration.GetSection("DocumetFolder:CompanyDoc").Value;
                var filePath = Path.Combine(basePath, fullImageName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    dataKey.file.CopyTo(fileStream);
                }
                returnRes.MessageCode = "1002";
                returnRes.MessageText = "File uploaded successfully.";
                objApp.UpdateEmpImageName(dataKey.EmployeeImageName, fullImageName);
                return Ok(returnRes);
               
            }
            catch (Exception ex)
            {
                UpdateLog(ex.Message, DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
                return BadRequest(ex.Message);
            }

        }


        #region Support Function
        private void SaveEmployee(modEmployeeUpdate dataKey,string entryID)
        {
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            System.Data.DataRow drLocal = null;
            System.Data.DataView dvLocal = null;
            clsApplication objApp = null;
            bool DATA_OK = false;

            try
            {
                if (DATA_OK == false)
                {
                    DATA_OK = true;
                }
                if (DATA_OK == true)
                {
                    objApp = new clsApplication();
                    objApp.GetEmployeeByCode(dataKey.EmployeeCode,dataKey.SiteId,out dsLocal);
                    dtLocal = dsLocal.Tables[0];
                    dvLocal = new DataView();
                    dvLocal.Table = dtLocal;
                    dvLocal.RowFilter = "EmployeeCode ='" + dataKey.EmployeeCode + "'";

                    if (dvLocal.Count == 0)
                    { // Add new block
                        drLocal = dtLocal.NewRow();
                        UpdateTheDataRowEmployee("ADDNEW", dataKey, entryID, ref drLocal);
                        dtLocal.Rows.Add(drLocal);
                    }
                    else
                    {//edit block
                        drLocal = dvLocal[0].Row;
                        drLocal.BeginEdit();
                        UpdateTheDataRowEmployee("EDIT", dataKey, entryID, ref drLocal);
                        drLocal.EndEdit();
                    }

                    dvLocal.RowFilter = null;
                    objApp.SaveData(dsLocal);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void UpdateTheDataRowEmployee(string OPN_FLAG, modEmployeeUpdate dataKey,string entryID, ref System.Data.DataRow drLocal)
        {
            try
            {
                if (OPN_FLAG == "ADDNEW")
                {
                    drLocal["EmployeeCode"] = clsWebLib.RetValidLen(dataKey.EmployeeCode.ToString().Trim(), 50);
                    drLocal["EmpEntryID"] = clsWebLib.RetValidLen(entryID.ToString().Trim(), 50);
                    drLocal["DateAdded"] = bplib.clsWebLib.DateData_AppToDB(dataKey.DateAdded, bplib.clsWebLib.DB_DATE_FORMAT);
                    drLocal["AddedBy"] = clsWebLib.RetValidLen(dataKey.AddedBy.ToString().Trim(), 150);
                }

                drLocal["EmployeeName"] = bplib.clsWebLib.RetValidLen(dataKey.EmployeeName.ToString().Trim(), 150);
                drLocal["Department"] = bplib.clsWebLib.RetValidLen(dataKey.Department.ToString().Trim(), 150);
                drLocal["Designation"] = bplib.clsWebLib.RetValidLen(dataKey.Designation.ToString().Trim(), 150);
                drLocal["Address"] = bplib.clsWebLib.RetValidLen(dataKey.Address.ToString().Trim(), 150);
                drLocal["EmailAddress"] = bplib.clsWebLib.RetValidLen(dataKey.EmailAddress.ToString().Trim(), 150);
                drLocal["PhoneNumber"] = bplib.clsWebLib.RetValidLen(dataKey.PhoneNumber.ToString().Trim(), 15);
                drLocal["EmployeeImageName"] = bplib.clsWebLib.RetValidLen(clsWebLib.ChkDBNull2(dataKey.EmployeeImageName), 150);
                drLocal["UpdateOn"] = bplib.clsWebLib.DateData_AppToDB(dataKey.UpdateOn, bplib.clsWebLib.DB_DATE_FORMAT);
                drLocal["UpdateBy"] = bplib.clsWebLib.RetValidLen(dataKey.UpdateBy.ToString().Trim(), 50);
                drLocal["SiteId"] = bplib.clsWebLib.RetValidLen(dataKey.SiteId.ToString().Trim(), 50);
               
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //
            }
        } //eof
        #endregion

        #region Common Funtion
        private void UpdateLog(string strLogData, string strRefID)
        {
            var sectionValue = _configuration.GetSection("FeedBackToLog:PathToLogFolder:LogFilePath").Value;

            string MyLogFile = bplib.clsWebLib.MyLogFile(strRefID.ToString().Trim(), sectionValue);

            bplib.clsWebLib.FeedBackToLog(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "| " + strLogData, MyLogFile);
        }//eof
        #endregion


    }
}
