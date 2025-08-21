using BHSNCJW.BO;
using BHSNCJW;
using bplib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using BHSAPIBaseNC8.JWTToken;
using Newtonsoft.Json;

namespace FMNC8Api.Controllers
{
    public class TranController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TranController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [JWTToken]
        [HttpPost]
        [ActionName("UpdateAcCodeSet")]
        [Route("FMNC8Api/Tran/UpdateAcCodeSet")]
        public IActionResult UpdateAcCodeSet([FromBody] ActCodeSet dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            mngJWT mngApp = null;
            DataSet dsLocalSite = null;
            clsApplication objApp = null;
            string MODULE_NAME = "Account Code settings.EDIT";
            try
            {
                //filter of invalid character
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();
                mngApp = new mngJWT();
                objApp = new clsApplication();

                UpdateLog("Call UpdateAcCodeSet()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
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

                    objApp.GetFMSiteID(out dsLocalSite, dataKey.SiteId);
                    if (dsLocalSite.Tables[0].Rows.Count == 0)
                    {
                        strMsg += "SiteID not Found";
                    }
                    if (dataKey.typeOfAct.Trim().ToUpper() == "SUB" || dataKey.typeOfAct.Trim().ToUpper() == "MAIN")
                    {
                        //
                    }
                    else
                    {
                        strMsg += " Account Group should be SUB or MAIN";
                    }
                    if (!clsWebLib.IsDateOK3(dataKey.UpdateOn))
                    {
                        strMsg += "Update Date format should be (dd-MMM-yyyy)...";
                    }
                    if (!clsWebLib.IsDateOK3(dataKey.EntryDate))
                    {
                        strMsg += "Entry Date format should be (dd-MMM-yyyy)...";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }
                //Update Database on verifiaction successful
                if (DATA_OK == true)
                {
                    SaveActCodeSet(dataKey,userID);

                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful.Account Code Set saved successfully.";
                }
                else
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "API Execution failed.Account Code Set not saved." + strMsg;
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
        [ActionName("DeletAcCodeSet")]
        [Route("FMNC8Api/Tran/DeletAcCodeSet")]
        public IActionResult DeletAcCodeSet([FromBody] DeleteActCodeSet dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            DataSet dsLocal = null;
            string MODULE_NAME = "Account Code settings.DELETE";
            clsApplication objApp = null;
            mngJWT mngApp = null;
            try
            {
                //filter of invalid character
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();
                objApp = new clsApplication();
                mngApp = new mngJWT();
                UpdateLog("Call DeletAcCodeSet()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
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

                    objApp.GetAccCodeSet(out dsLocal, dataKey.ActCodeSet.ToUpper(), dataKey.siteID.ToUpper());
                    if (dsLocal.Tables[0].Rows.Count == 0)
                    {
                        strMsg += "Account Code Not Found";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }

                if (DATA_OK == true)
                {
                    objApp.DeleteAccCodeSet(dataKey.ActCodeSet, dataKey.siteID);
                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful. Delete Account Code Set successfully.";
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
        [ActionName("UpdateBudget")]
        [Route("FMNC8Api/Tran/UpdateBudget")]
        public IActionResult UpdateBudget([FromBody] CreateBudget dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            mngJWT mngApp = null;
            DataSet dsLocalSite = null;
            clsApplication objApp = null;
            string MODULE_NAME = "Budget Settings.EDIT";
            try
            {
                //filter of invalid character
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();
                mngApp = new mngJWT();
                objApp = new clsApplication();

                UpdateLog("Call UpdateBudget()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
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

                    objApp.GetAccCodeSet(out dsLocalSite,dataKey.actCode, dataKey.SiteId);
                    if (dsLocalSite.Tables[0].Rows.Count == 0)
                    {
                        strMsg += "This Acount Code and Corresponding SiteID not found";
                    }
                    if (!clsWebLib.IsDateOK3(dataKey.UpdateOn))
                    {
                        strMsg += "Update Date format should be (dd-MMM-yyyy)...";
                    }
                    if (!clsWebLib.IsDateOK3(dataKey.EntryDate))
                    {
                        strMsg += "Entry Date format should be (dd-MMM-yyyy)...";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }
                //Update Database on verifiaction successful
                if (DATA_OK == true)
                {
                    SaveBudget(dataKey, userID);

                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful.Budget saved successfully.";
                }
                else
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "API Execution failed.Budget not saved." + strMsg;
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
        [ActionName("DeleteBudget")]
        [Route("FMNC8Api/Tran/DeleteBudget")]
        public IActionResult DeleteBudget([FromBody] DeleteBudget dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            DataSet dsLocal = null;
            string MODULE_NAME = "Budget Settings.DELETE";
            clsApplication objApp = null;
            mngJWT mngApp = null;
            try
            {
                //filter of invalid character
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();
                objApp = new clsApplication();
                mngApp = new mngJWT();
                UpdateLog("Call DeleteBudget()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
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

                    objApp.GetBudget(out dsLocal, dataKey.budgetCode.ToUpper(), dataKey.siteID.ToUpper());
                    if (dsLocal.Tables[0].Rows.Count == 0)
                    {
                        strMsg += "Budget Not found";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }

                if (DATA_OK == true)
                {
                    objApp.DeleteBudget(dataKey.budgetCode, dataKey.siteID);
                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful. Delete Budget successfully.";
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
        [ActionName("UpdateIncome")]
        [Route("FMNC8Api/Tran/UpdateIncome")]
        public IActionResult UpdateIncome([FromBody] CreateIncome dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            mngJWT mngApp = null;
            DataSet dsLocalSite = null;
            clsApplication objApp = null;
            string MODULE_NAME = "Income Update.EDIT";
            try
            {
                //filter of invalid character
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();
                mngApp = new mngJWT();
                objApp = new clsApplication();

                UpdateLog("Call UpdateIncome()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
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

                    objApp.GetFMSiteID(out dsLocalSite, dataKey.SiteId);
                    if (dsLocalSite.Tables[0].Rows.Count == 0)
                    {
                        strMsg += "SiteID not Found";
                    }
                    if (!clsWebLib.IsDateOK3(dataKey.UpdateOn))
                    {
                        strMsg += "Update Date format should be (dd-MMM-yyyy)...";
                    }
                    if (!clsWebLib.IsDateOK3(dataKey.EntryDate))
                    {
                        strMsg += "Entry Date format should be (dd-MMM-yyyy)...";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }
                //Update Database on verifiaction successful
                if (DATA_OK == true)
                {
                    SaveIncome(dataKey, userID);

                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful.Income saved successfully.";
                }
                else
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "API Execution failed.Income not saved." + strMsg;
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
        [ActionName("DeleteIncome")]
        [Route("FMNC8Api/Tran/DeleteIncome")]
        public IActionResult DeleteIncome([FromBody] DeleteIncome dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            DataSet dsLocal = null;
            string MODULE_NAME = "Income Update.DELETE";
            clsApplication objApp = null;
            mngJWT mngApp = null;
            try
            {
                //filter of invalid character
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();
                objApp = new clsApplication();
                mngApp = new mngJWT();
                UpdateLog("Call DeleteIncome()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
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

                    objApp.GetIncome(out dsLocal, dataKey.entryId.ToUpper(), dataKey.SiteId.ToUpper());
                    if (dsLocal.Tables[0].Rows.Count == 0)
                    {
                        strMsg += "Income Not found";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }

                if (DATA_OK == true)
                {
                    objApp.DeleteIncome(dataKey.entryId, dataKey.SiteId);
                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful. Delete Income successfully.";
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
        [ActionName("UpdateDailyExp")]
        [Route("FMNC8Api/Tran/UpdateDailyExp")]
        public IActionResult UpdateDailyExp([FromBody] CreateDailyExp dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            mngJWT mngApp = null;
            DataSet dsLocalSite = null;
            DataSet dsLocalIncome = null;
            clsApplication objApp = null;
            string MODULE_NAME = "Daily Expense Update.EDIT";
            try
            {
                //filter of invalid character
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();
                mngApp = new mngJWT();
                objApp = new clsApplication();

                UpdateLog("Call UpdateDailyExp()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
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

                    objApp.GetAccCodeSet(out dsLocalSite, dataKey.actCode, dataKey.SiteId);
                    if (dsLocalSite.Tables[0].Rows.Count == 0)
                    {
                        strMsg += "This Acount Code and Corresponding SiteID not found.";
                    }
                    objApp.GetUserActIncome(out dsLocalIncome, dataKey.userAc, dataKey.SiteId);
                    if (dsLocalIncome.Tables[0].Rows.Count == 0)
                    {
                        strMsg += "This User Account and Corresponding SiteID not found.";
                    }
                    if (!clsWebLib.IsDateOK3(dataKey.EntryDate))
                    {
                        strMsg += "Entry Date format should be (dd-MMM-yyyy)...";
                    }
                    if (!clsWebLib.IsDateOK3(dataKey.UpdateOn))
                    {
                        strMsg += "Update Date format should be (dd-MMM-yyyy)...";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }
                //Update Database on verifiaction successful
                if (DATA_OK == true)
                {
                    SaveDailyExp(dataKey, userID);

                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful.Daily Expensive saved successfully.";
                }
                else
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "API Execution failed.Daily Expensive not saved." + strMsg;
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
        [ActionName("DeleteDailyExp")]
        [Route("FMNC8Api/Tran/DeleteDailyExp")]
        public IActionResult DeleteDailyExp([FromBody] DeleteDailyExp dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            DataSet dsLocal = null;
            string MODULE_NAME = "Daily Expense Update.DELETE";
            clsApplication objApp = null;
            mngJWT mngApp = null;
            try
            {
                //filter of invalid character
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();
                objApp = new clsApplication();
                mngApp = new mngJWT();
                UpdateLog("Call DeleteDailyExp()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
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

                    objApp.GetDailyExp(out dsLocal, dataKey.expId.ToUpper(), dataKey.SiteId.ToUpper());
                    if (dsLocal.Tables[0].Rows.Count == 0)
                    {
                        strMsg += "Daily Expensive Not found";
                    }

                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }

                if (DATA_OK == true)
                {
                    objApp.DeleteDailyExp(dataKey.expId, dataKey.SiteId);
                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful. Delete Daily Expensive successfully.";
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
        [ActionName("GetSQLOutput")]
        [Route("FMNC8Api/Tran/GetSQLOutput")]
        public IActionResult GetSQLOutput([FromBody]modSQLRequest dataKey)
        {
            modSQLResponse objResponse = null;
            System.Data.DataSet dsLocal = null;
            bool DATA_OK = false;
            clsApplication objApp = null;

            try
            {
                objResponse = new modSQLResponse();
                objApp = new clsApplication();

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }
                if (DATA_OK == false)
                {
                    if (dataKey.SQLString.ToUpper().Contains("UPDATE ") || dataKey.SQLString.ToUpper().Contains("DELETE ") || dataKey.SQLString.ToUpper().Contains("DROP "))
                    {
                        objResponse.MessageCode = "1001";
                        objResponse.MessageText = "Query execution failed. Update or delete command can not be used in 'SQLString'.";

                        DATA_OK = false;
                    }
                    else
                    {
                        DATA_OK = true;
                    }
                }
                if (DATA_OK == true)
                {
                    objApp.GetSQLOutput(dataKey.SQLString, out dsLocal);
                    if (dsLocal.Tables[0].Rows.Count > 0)
                    {
                        objResponse.MessageCode = "1002";
                        objResponse.MessageText = "Query execution successful. Data found.";
                        objResponse.ReturnRec =JsonConvert.SerializeObject(dsLocal.Tables[0]);
                    }
                    else
                    {
                        objResponse.MessageCode = "1001";
                        objResponse.MessageText = "Query execution successful. No data found.";
                    }
                }

                return Ok(objResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                objApp = null;
                dsLocal = null;
            }
        }//eof

        #region Support Function for Account code Set
        private void SaveActCodeSet(ActCodeSet dataKey,string userID)
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
                    objApp.GetAccCodeSet(out dsLocal, dataKey.actCode.Trim().ToUpper(), dataKey.SiteId.Trim());
                    dtLocal = dsLocal.Tables[0];
                    dvLocal = new DataView();
                    dvLocal.Table = dtLocal;
                    dvLocal.RowFilter = "actCode ='" + dataKey.actCode.ToString() + "'and SiteId='" + dataKey.SiteId + "'";

                    if (dvLocal.Count == 0)
                    { // Add new block
                        drLocal = dtLocal.NewRow();
                        UpdateTheDataRowAccCode("ADDNEW", dataKey, ref drLocal, userID);
                        dtLocal.Rows.Add(drLocal);
                    }
                    else
                    {//edit block
                        drLocal = dvLocal[0].Row;
                        drLocal.BeginEdit();
                        UpdateTheDataRowAccCode("EDIT", dataKey, ref drLocal,userID);
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
        private void UpdateTheDataRowAccCode(string OPN_FLAG, ActCodeSet dataKey, ref System.Data.DataRow drLocal,string userId)
        {
            try
            {
                if (OPN_FLAG == "ADDNEW")
                {
                    drLocal["actCode"] = clsWebLib.RetValidLen(dataKey.actCode.ToString().Trim().ToUpper(), 50);
                    drLocal["SiteId"] = bplib.clsWebLib.RetValidLen(dataKey.SiteId.ToString().Trim(), 50);
                }

                drLocal["acGroup"] = bplib.clsWebLib.RetValidLen(dataKey.acGroup.ToString().Trim(), 150);
                drLocal["acSubCode"] = bplib.clsWebLib.RetValidLen(dataKey.acSubCode.ToString().Trim(), 150);
                drLocal["typeOfAct"] = bplib.clsWebLib.RetValidLen(dataKey.typeOfAct.ToString().Trim(), 150);
                drLocal["entryDate"] = bplib.clsWebLib.DateData_AppToDB(dataKey.UpdateOn.ToString().Trim(), clsWebLib.DB_DATE_FORMAT);
                drLocal["acName"] = bplib.clsWebLib.RetValidLen(dataKey.acName.ToString().Trim(), 150);
                drLocal["Descriptions"] = bplib.clsWebLib.RetValidLen(dataKey.Descriptions.ToString().Trim(), 150);
                drLocal["UpdateOn"] = bplib.clsWebLib.DateData_AppToDB(dataKey.UpdateOn.ToString().Trim(), clsWebLib.DB_DATE_FORMAT);
                drLocal["UpdateBy"] = userId;
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

        #region Support Function for budget
        private void SaveBudget(CreateBudget dataKey, string userID)
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
                    objApp.GetBudget(out dsLocal, dataKey.budgetCode.Trim().ToUpper(), dataKey.SiteId.Trim());
                    dtLocal = dsLocal.Tables[0];
                    dvLocal = new DataView();
                    dvLocal.Table = dtLocal;
                    dvLocal.RowFilter = "budgetCode ='" + dataKey.budgetCode.ToString() + "'and SiteId='" + dataKey.SiteId + "'";

                    if (dvLocal.Count == 0)
                    { // Add new block
                        drLocal = dtLocal.NewRow();
                        UpdateTheDataRowBudget("ADDNEW", dataKey, ref drLocal, userID);
                        dtLocal.Rows.Add(drLocal);
                    }
                    else
                    {//edit block
                        drLocal = dvLocal[0].Row;
                        drLocal.BeginEdit();
                        UpdateTheDataRowBudget("EDIT", dataKey, ref drLocal, userID);
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
        private void UpdateTheDataRowBudget(string OPN_FLAG, CreateBudget dataKey, ref System.Data.DataRow drLocal, string userId)
        {
            try
            {
                if (OPN_FLAG == "ADDNEW")
                {
                    drLocal["budgetCode"] = clsWebLib.RetValidLen(dataKey.budgetCode.ToString().Trim().ToUpper(), 200);
                    drLocal["SiteId"] = bplib.clsWebLib.RetValidLen(dataKey.SiteId.ToString().Trim().ToUpper(), 20);
                }

                drLocal["entryDate"] = bplib.clsWebLib.DateData_AppToDB(dataKey.EntryDate.ToString().Trim(), clsWebLib.DB_DATE_FORMAT);
                drLocal["months"] = bplib.clsWebLib.RetValidLen(dataKey.months.ToString().Trim(), 15);
                drLocal["years"] = bplib.clsWebLib.RetValidLen(dataKey.years.ToString().Trim(), 18);
                drLocal["acGroup"] = bplib.clsWebLib.RetValidLen(dataKey.acGroup.ToString().Trim(), 150);
                drLocal["actCode"] = bplib.clsWebLib.RetValidLen(dataKey.actCode.ToString().Trim(), 200);
                drLocal["amount"] = bplib.clsWebLib.RetValidLen(dataKey.amount.ToString().Trim(), 18);
                drLocal["UpdateOn"] = bplib.clsWebLib.DateData_AppToDB(dataKey.UpdateOn.ToString().Trim(), clsWebLib.DB_DATE_FORMAT);
                drLocal["UpdateBy"] = userId;
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

        #region Support Function for Income
        private void SaveIncome(CreateIncome dataKey, string userID)
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
                    
                    objApp.GetIncome(out dsLocal, dataKey.entryId.Trim().ToUpper(), dataKey.SiteId.Trim());
                    dtLocal = dsLocal.Tables[0];
                    dvLocal = new DataView();
                    dvLocal.Table = dtLocal;
                    dvLocal.RowFilter = "entryId ='" + dataKey.entryId.ToString() + "'and SiteId='" + dataKey.SiteId + "'";

                    if (dvLocal.Count == 0)
                    { // Add new block
                        drLocal = dtLocal.NewRow();
                        UpdateTheDataRowIncome("ADDNEW", dataKey, ref drLocal, userID);
                        dtLocal.Rows.Add(drLocal);
                    }
                    else
                    {//edit block
                        drLocal = dvLocal[0].Row;
                        drLocal.BeginEdit();
                        UpdateTheDataRowIncome("EDIT", dataKey, ref drLocal, userID);
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
        private void UpdateTheDataRowIncome(string OPN_FLAG, CreateIncome dataKey, ref System.Data.DataRow drLocal, string userId)
        {
            try
            {
                if (OPN_FLAG == "ADDNEW")
                {
                    drLocal["entryId"] = clsWebLib.RetValidLen(dataKey.entryId.ToString().Trim().ToUpper(), 200);
                    drLocal["SiteId"] = bplib.clsWebLib.RetValidLen(dataKey.SiteId.ToString().Trim().ToUpper(), 20);
                }

                drLocal["entryDate"] = bplib.clsWebLib.DateData_AppToDB(dataKey.EntryDate.ToString().Trim(), clsWebLib.DB_DATE_FORMAT);
                drLocal["months"] = bplib.clsWebLib.RetValidLen(dataKey.months.ToString().Trim().ToUpper(), 15);
                drLocal["years"] = bplib.clsWebLib.RetValidLen(dataKey.years.ToString().Trim(), 18);
                drLocal["userAc"] = bplib.clsWebLib.RetValidLen(dataKey.userAc.ToString().Trim().ToUpper(), 150);
                drLocal["amount"] = bplib.clsWebLib.RetValidLen(dataKey.amount.ToString().Trim(), 18);
                drLocal["UpdateOn"] = bplib.clsWebLib.DateData_AppToDB(dataKey.UpdateOn.ToString().Trim(), clsWebLib.DB_DATE_FORMAT);
                drLocal["UpdateBy"] = userId;
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
        
        #region Support Function for Daily Expensive
        private void SaveDailyExp(CreateDailyExp dataKey, string userID)
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
                    objApp.GetDailyExp(out dsLocal, dataKey.expId.Trim().ToUpper(), dataKey.SiteId.Trim());
                    dtLocal = dsLocal.Tables[0];
                    dvLocal = new DataView();
                    dvLocal.Table = dtLocal;
                    dvLocal.RowFilter = "expId ='" + dataKey.expId.ToString() + "'and SiteId='" + dataKey.SiteId + "'";

                    if (dvLocal.Count == 0)
                    { // Add new block
                        drLocal = dtLocal.NewRow();
                        UpdateTheDataRowDailyExp("ADDNEW", dataKey, ref drLocal, userID);
                        dtLocal.Rows.Add(drLocal);
                    }
                    else
                    {//edit block
                        drLocal = dvLocal[0].Row;
                        drLocal.BeginEdit();
                        UpdateTheDataRowDailyExp("EDIT", dataKey, ref drLocal, userID);
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
        private void UpdateTheDataRowDailyExp(string OPN_FLAG, CreateDailyExp dataKey, ref System.Data.DataRow drLocal, string userId)
        {
            try
            {
                if (OPN_FLAG == "ADDNEW")
                {
                    drLocal["expId"] = clsWebLib.RetValidLen(dataKey.expId.ToString().Trim().ToUpper(), 200);
                    drLocal["SiteId"] = bplib.clsWebLib.RetValidLen(dataKey.SiteId.ToString().Trim().ToUpper(), 20);
                }

                drLocal["entryDate"] = bplib.clsWebLib.DateData_AppToDB(dataKey.EntryDate.ToString().Trim(), clsWebLib.DB_DATE_FORMAT);
                drLocal["userAc"] = bplib.clsWebLib.RetValidLen(dataKey.userAc.ToString().Trim().ToUpper(), 150);
                drLocal["actCode"] = bplib.clsWebLib.RetValidLen(dataKey.actCode.ToString().Trim(), 200);
                drLocal["payBy"] = bplib.clsWebLib.RetValidLen(dataKey.payBy.ToString().Trim().ToUpper(), 150);
                drLocal["amount"] = bplib.clsWebLib.RetValidLen(dataKey.amount.ToString().Trim(), 18);
                drLocal["remarks"] = bplib.clsWebLib.RetValidLen(dataKey.remarks.ToString().Trim().ToUpper(), 255);
                drLocal["UpdateOn"] = bplib.clsWebLib.DateData_AppToDB(dataKey.UpdateOn.ToString().Trim(), clsWebLib.DB_DATE_FORMAT);
                drLocal["UpdateBy"] = userId;
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
