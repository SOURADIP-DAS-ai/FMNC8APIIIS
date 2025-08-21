using BHSNCJW.BO;
using BHSNCJW;
using bplib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using BHSAPIBaseNC8.JWTToken;

namespace FMNC8Api.Controllers
{
 
    public class MDController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MDController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [JWTToken]
        [HttpPost]
        [ActionName("UpdateFixedEntity")]
        [Route("FMNC8Api/MD/UpdateFixedEntity")]
        public IActionResult UpdateFixedEntity([FromBody] CreateFixedEntity dataKey)
        {
            Response returnRes = null;   
            bool DATA_OK = false;
            string strMsg = "";
            mngJWT mngApp= null;
            string MODULE_NAME = "System Variables.EDIT";
            try
            {
                //filter of invalid character
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();
                mngApp=new mngJWT();

                UpdateLog("Call UpdateFixedEntity()- ", DateTime.Now.ToString("MM-dd-yyyy")+"-Log");
                //Verify Model state
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }
                //Verification of user access
                if (mngApp.GetUserIDAndCheckAccess(Request.Headers["Authorization"].ToString().Substring("Bearer ".Length), MODULE_NAME,out string userID) == false)
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "ModeleName: "+ MODULE_NAME.ToUpper()+" for UserID: "+userID+" Access Denided." + strMsg;
                    return Ok(returnRes);
                }
                UpdateLog("UserID:"+userID+" ModuleName:"+ MODULE_NAME.ToUpper(), DateTime.Now.ToString("MM-dd-yyyy") + "-Log");

                //Verification of User Input
                if (DATA_OK == false)
                {
                    if (dataKey.EntityType.Count()>50)
                    {
                        strMsg += "Please Insert Entity Type...(Minimum Length 50)";
                    }
                    if (dataKey.Code.Count()>50)
                    {
                        strMsg += "Please Insert Code...(Minimum Length 50)";
                    }
                    if(dataKey.Description.Count()>150)
                    {
                        strMsg += "Please Insert Description...(Minimum Length 150)";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }
                //Update Database on verifiaction successful
                if (DATA_OK == true)
                {
                    SaveFMFixedEntity(dataKey);

                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful.Fixed Entity saved successfully.";
                }
                else
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "API Execution failed.Fixed Entity not saved." + strMsg;
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
        [ActionName("DeleteFixedEntity")]
        [Route("FMNC8Api/MD/DeleteFixedEntity")]
        public IActionResult DeleteFixedEntity([FromBody] CreateFixedEntity dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            DataSet dsLocal = null;
            string MODULE_NAME = "System Variables.DELETE";
            clsApplication objApp = null;
            mngJWT mngApp = null;
            try
            {
                //filter of invalid character
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();
                objApp=new clsApplication();
                mngApp= new mngJWT();
                UpdateLog("Call DeleteFixedEntity()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
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

                    objApp.GetFMFixedEntity(out dsLocal, dataKey.EntityType.ToUpper(),dataKey.Code);
                    if (dsLocal.Tables[0].Rows.Count == 0)
                    {
                        strMsg += "Fixed Entity Not Found";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }

                if (DATA_OK == true)
                {
                    objApp.DeleteFMFixedEntity(dataKey.EntityType,dataKey.Code);
                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful. Delete Fixed Entity successfully.";
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

        //Update Table UpdateFixedEntityBySiteID  which is site id specific
        [JWTToken]
        [HttpPost]
        [ActionName("UpdateFixedEntityBySiteID")]
        [Route("FMNC8Api/MD/UpdateFixedEntityBySiteID")]
        public IActionResult UpdateFixedEntityBySiteID([FromBody] CreateFixedEntityBySiteID dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            mngJWT mngApp = null;
            string MODULE_NAME = "SITEID WISE.EDIT";
            try
            {
                //filter of invalid character
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();
                mngApp = new mngJWT();

                UpdateLog("Call UpdateFixedEntityBySiteID()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
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
                    if (dataKey.EntityType.Count() > 50)
                    {
                        strMsg += "Please Insert Entity Type...(Minimum Length 50)";
                    }
                    if (dataKey.Code.Count() > 50)
                    {
                        strMsg += "Please Insert Code...(Minimum Length 50)";
                    }
                    if (dataKey.Description.Count() > 150)
                    {
                        strMsg += "Please Insert Description...(Minimum Length 150)";
                    }
                    if (dataKey.SiteID.Count() > 50)
                    {
                        strMsg += "Please Insert SiteID...(Minimum Length 50)";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }
                //Update Database on verifiaction successful
                if (DATA_OK == true)
                {
                    SaveFMFixedEntityBySiteID(dataKey);

                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful.Fixed Entity saved successfully.";
                }
                else
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "API Execution failed.Fixed Entity not saved." + strMsg;
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
        [ActionName("DeleteFixedEntityBySiteID")]
        [Route("FMNC8Api/MD/DeleteFixedEntityBySiteID")]
        public IActionResult DeleteFixedEntityBySiteID([FromBody] CreateFixedEntityBySiteID dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            DataSet dsLocal = null;
            string MODULE_NAME = "SITEID WISE.DELETE";
            clsApplication objApp = null;
            mngJWT mngApp = null;
            try
            {
                //filter of invalid character
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();
                objApp = new clsApplication();
                mngApp = new mngJWT();
                UpdateLog("Call DeleteFixedEntityBySiteID()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
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

                    objApp.GetFMFixedEntityBySiteID(out dsLocal, dataKey.EntityType.ToUpper(), dataKey.Code,dataKey.SiteID);
                    if (dsLocal.Tables[0].Rows.Count == 0)
                    {
                        strMsg += "Fixed Variable Entity Not Found";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }

                if (DATA_OK == true)
                {
                    objApp.DeleteFMFixedEntityBySiteID(dataKey.EntityType, dataKey.Code,dataKey.SiteID);
                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful. Delete Fixed Entity successfully.";
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
        [ActionName("ModuleAccess")]
        [Route("FMNC8Api/MD/ModuleAccess")]
        public IActionResult ModuleAccess([FromBody] UserAccessModule dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            mngJWT mngApp = null;
            string MODULE_NAME = dataKey.ModuleName.ToUpper();
            try
            {
                //filter of invalid character
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();
                mngApp = new mngJWT();

                UpdateLog("Accessing: "+dataKey.ModuleName, DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
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
                else
                {
                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "Module Access Aprove.";
                }
                UpdateLog("UserID:" + userID + " ModuleName:" + MODULE_NAME.ToUpper()+" Status: "+returnRes.MessageText, DateTime.Now.ToString("MM-dd-yyyy") + "-Log");
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


        #region Support Function for Fixed Entity
        private void SaveFMFixedEntity(CreateFixedEntity dataKey)
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
                    objApp.GetFMFixedEntity(out dsLocal, dataKey.EntityType.Trim().ToUpper(),dataKey.Code.Trim());
                    dtLocal = dsLocal.Tables[0];
                    dvLocal = new DataView();
                    dvLocal.Table = dtLocal;
                    dvLocal.RowFilter = "EntityType ='" + dataKey.EntityType.ToString()+"'and code='"+dataKey.Code+"'";

                    if (dvLocal.Count == 0)
                    { // Add new block
                        drLocal = dtLocal.NewRow();
                        UpdateTheDataRowFMUser("ADDNEW", dataKey, ref drLocal);
                        dtLocal.Rows.Add(drLocal);
                    }
                    else
                    {//edit block
                        drLocal = dvLocal[0].Row;
                        drLocal.BeginEdit();
                        UpdateTheDataRowFMUser("EDIT", dataKey, ref drLocal);
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
        private void UpdateTheDataRowFMUser(string OPN_FLAG, CreateFixedEntity dataKey, ref System.Data.DataRow drLocal)
        {
            try
            {
                if (OPN_FLAG == "ADDNEW")
                {
                    drLocal["EntityType"] = clsWebLib.RetValidLen(dataKey.EntityType.ToString().Trim().ToUpper(), 50);
                    drLocal["Code"] = bplib.clsWebLib.RetValidLen(dataKey.Code.ToString().Trim(), 50);
                }

                drLocal["Description"] = bplib.clsWebLib.RetValidLen(dataKey.Description.ToString().Trim().ToUpper(), 150);
                drLocal["Value"] = bplib.clsWebLib.RetValidLen(dataKey.Value.ToString().Trim(), 18);
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

        #region Support Function for Fixed Entity By SiteID
        private void SaveFMFixedEntityBySiteID(CreateFixedEntityBySiteID dataKey)
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
                    objApp.GetFMFixedEntityBySiteID(out dsLocal, dataKey.EntityType.Trim().ToUpper(), dataKey.Code.Trim(),dataKey.SiteID);
                    dtLocal = dsLocal.Tables[0];
                    dvLocal = new DataView();
                    dvLocal.Table = dtLocal;
                    dvLocal.RowFilter = "EntityType ='" + dataKey.EntityType.ToString() + "'and code='" + dataKey.Code + "'";

                    if (dvLocal.Count == 0)
                    { // Add new block
                        drLocal = dtLocal.NewRow();
                        UpdateTheDataRowEntityBySiteID("ADDNEW", dataKey, ref drLocal);
                        dtLocal.Rows.Add(drLocal);
                    }
                    else
                    {//edit block
                        drLocal = dvLocal[0].Row;
                        drLocal.BeginEdit();
                        UpdateTheDataRowEntityBySiteID("EDIT", dataKey, ref drLocal);
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
        private void UpdateTheDataRowEntityBySiteID(string OPN_FLAG, CreateFixedEntityBySiteID dataKey, ref System.Data.DataRow drLocal)
        {
            try
            {
                if (OPN_FLAG == "ADDNEW")
                {
                    drLocal["EntityType"] = clsWebLib.RetValidLen(dataKey.EntityType.ToString().Trim().ToUpper(), 50);
                    drLocal["Code"] = bplib.clsWebLib.RetValidLen(dataKey.Code.ToString().Trim(), 50);
                }

                drLocal["Description"] = bplib.clsWebLib.RetValidLen(dataKey.Description.ToString().Trim().ToUpper(), 150);
                drLocal["Value"] = bplib.clsWebLib.RetValidLen(dataKey.Value.ToString().Trim(), 18);
                drLocal["SiteID"] = bplib.clsWebLib.RetValidLen(dataKey.SiteID.ToString().Trim(), 50);
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
