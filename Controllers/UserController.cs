using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FMNC8Api;
using BHSNCJW;
using BHSNCJW.BO;
using System.Data;
using bplib;
using Microsoft.Extensions.Configuration;
using System;


namespace FMNC8Api.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("UpdateFMUser")]
        [Route("FMNC8Api/User/UpdateFMUser")]
        public IActionResult UpdateFMUser([FromBody] CreateFMUser dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            try
            {
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();

                UpdateLog("Call UpdateFMUser()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }

                if (DATA_OK == false)
                {
                    if (dataKey.Password.Length < 8 || dataKey.Password.Length > 10)
                    {
                        strMsg += "Please Insert Password...(Minimum eight characters or maximum ten characters,one uppercase,one lowercase,one special character requested to be used)";
                    }
                    if(dataKey.UserGroup.Trim().ToUpper()=="SUPR" || dataKey.UserGroup.Trim().ToUpper() == "USER" || dataKey.UserGroup.Trim().ToUpper() == "REPO")
                    {
                        //
                    }
                    else
                    {
                        strMsg += "User Group should be SUPR,USER, or REPO";
                    }
                    if (!clsWebLib.IsDateOK3(dataKey.UpdateOn))
                    {
                        strMsg += "Date format should be (dd-MMM-yyyy)...";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }

                if (DATA_OK == true)
                {
                    SaveFMUser(dataKey);

                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful. FMUser saved successfully.";
                }
                else
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "API Execution failed. FMUser not saved." + strMsg;
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

        [AllowAnonymous]
        [HttpPost]
        [ActionName("DeleteFMUser")]
        [Route("FMNC8Api/User/DeleteFMUser")]
        public IActionResult DeleteFMUser([FromBody] ReqDeleteUser dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            DataSet dsLocal = null;
            clsApplication objApp = null;
            try
            {
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();
                objApp= new clsApplication();

                UpdateLog("Call DeleteFMUser()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }

                if (DATA_OK == false)
                {
                    
                    objApp.GetFMUser(out dsLocal,dataKey.UserID);
                    if (dsLocal.Tables[0].Rows.Count == 0)
                    {
                        strMsg += "User Not Found";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }

                if (DATA_OK == true)
                {
                    objApp.DeleteFMUser(dataKey.UserID);
                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful. Delete FMUser successfully.";
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

        [AllowAnonymous]
        [HttpPost]
        [ActionName("UpdateFMSiteID")]
        [Route("FMNC8Api/User/UpdateFMSiteID")]
        public IActionResult UpdateFMSiteID([FromBody] CreateSiteID dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            try
            {
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();

                UpdateLog("Call UpdateFMSiteID()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }

                if (DATA_OK == false)
                {
                    if (dataKey.SITEID.Length > 100)
                    {
                        strMsg += "Please Insert SiteID...(Max length 100)";
                    }
                    if (dataKey.SITE_GROUP.Length > 150)
                    {
                        strMsg += "Please Insert SiteGroup...(Max length 150)";
                    }
                    if (!clsWebLib.IsDateOK3(dataKey.UpdateOn))
                    {
                        strMsg += "Date format should be (dd-MMM-yyyy)...";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }

                if (DATA_OK == true)
                {
                    SaveFMSiteID(dataKey);

                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful. SiteID saved successfully.";
                }
                else
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "API Execution failed. SiteID not saved." + strMsg;
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

        [AllowAnonymous]
        [HttpPost]
        [ActionName("DeleteFMSiteID")]
        [Route("FMNC8Api/User/DeleteFMSiteID")]
        public IActionResult DeleteFMSiteID([FromBody] ReqDeleteSiteID dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            DataSet dsLocal = null;
            clsApplication objApp = null;
            try
            {
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();
                objApp= new clsApplication();

                UpdateLog("Call DeleteFMSiteID()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }

                if (DATA_OK == false)
                {

                    objApp.GetFMSiteID(out dsLocal, dataKey.SITEID);
                    if (dsLocal.Tables[0].Rows.Count == 0)
                    {
                        strMsg += "SiteID Not Found";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }

                if (DATA_OK == true)
                {
                    objApp.DeleteFMSiteID(dataKey.SITEID);
                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful. Delete SITEID successfully.";
                }
                else
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "API Execution failed. Delete Failed." + strMsg;
                }

                UpdateLog(returnRes.MessageText,DateTime.Now.ToString("MM-dd-yyyy") + "-Log");

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

        [AllowAnonymous]
        [HttpPost]
        [ActionName("UpdateFMUserAccSite")]
        [Route("FMNC8Api/User/UpdateFMUserAccSite")]
        public IActionResult UpdateFMUserAccSite([FromBody] CreateFMUserAccSite dataKey)
        {
            Response returnRes = null;
            DataSet dsLocalUser = null;
            DataSet dsLocalSite = null;

            bool DATA_OK = false;
            string strMsg = "";
            clsApplication objApp= new clsApplication();
            try
            {
                dataKey= clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();

                UpdateLog("Call UpdateFMUserAccSite()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }

                if (DATA_OK == false)
                {
                    objApp.GetFMUser(out dsLocalUser, dataKey.UserID);
                    if (dsLocalUser.Tables[0].Rows.Count == 0)
                    {
                        strMsg += "UserID Not Found. ";
                    } 

                    objApp.GetFMAllSiteID(out dsLocalSite);
                    if (dsLocalSite.Tables[0].Rows.Count > 0)
                    {
                        DataTable dtLocal = dsLocalSite.Tables[0];
                        for(int i=0; i < dataKey.SITE_IDs.Count; i++)
                        {
                            DataRow[] drLocal = dtLocal.Select("SITEID ='" + dataKey.SITE_IDs[i].SITEID + "'");
                            if (drLocal.Length <= 0) 
                            {
                                strMsg +=" SiteID:"+ dataKey.SITE_IDs[i].SITEID+" Not Found.";
                            }
                        }

                    }
                    else
                    {
                       
                    }
                    
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }

                if (DATA_OK == true)
                {
                    SaveFMUserAccSite(dataKey);

                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful. User Site Access saved successfully.";
                }
                else
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "API Execution failed. User Site Access not saved." + strMsg;
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

        [AllowAnonymous]
        [HttpPost]
        [ActionName("DeleteFMUserAccSite")]
        [Route("FMNC8Api/User/DeleteFMUserAccSite")]
        public IActionResult DeleteFMUserAccSite([FromBody] CreateFMUserAccSite dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            DataSet dsLocalUser = null;
            DataSet dsLocalSite = null;
            clsApplication objApp = null;
            try
            {
                dataKey= clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();
                objApp = new clsApplication();

                UpdateLog("Call DeleteFMUserAccSite()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }

                if (DATA_OK == false)
                {
                    objApp.GetFMUserAllAccSite(out dsLocalSite,dataKey.UserID);
                    if (dsLocalSite.Tables[0].Rows.Count > 0)
                    {
                        DataTable dtLocal = dsLocalSite.Tables[0];
                        for (int i = 0; i < dataKey.SITE_IDs.Count; i++)
                        {
                            DataRow[] drLocal = dtLocal.Select("SITEID ='" + dataKey.SITE_IDs[i].SITEID + "'");
                            if (drLocal.Length <= 0)
                            {
                                strMsg +=" SiteID:" + dataKey.SITE_IDs[i].SITEID + " Not Found; ";
                            }
                        }

                    }
                    else
                    {
                        strMsg += "UserID not found";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }

                if (DATA_OK == true)
                {
                    for(int i = 0; i < dataKey.SITE_IDs.Count; i++)
                    {
                    string userid=dataKey.UserID.ToString();
                        string siteid = dataKey.SITE_IDs[i].SITEID.ToString();
                    objApp.DeleteFMUserAccSite(userid,siteid);
                    }
                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful. Delete User Site Access successfully.";
                }
                else
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "API Execution failed. Delete User Site Access Failed." + strMsg;
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

        [AllowAnonymous]
        [HttpPost]
        [ActionName("UpdateFMUserAccManager")]
        [Route("FMNC8Api/User/UpdateFMUserAccManager")]
        public IActionResult UpdateFMUserAccManager([FromBody] FMUserAccManager dataKey)
        {
            Response returnRes = null;
            DataSet dsLocalUser = null;
            DataSet dsLocalSite = null;

            bool DATA_OK = false;
            string strMsg = "";
            clsApplication objApp = null; ;
            try
            {
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();
                objApp= new clsApplication();

                UpdateLog("Call UpdateFMUserAccManager()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }

                if (DATA_OK == false)
                {
                    objApp.GetFMUser(out dsLocalUser, dataKey.USERID);
                    if (dsLocalUser.Tables[0].Rows.Count == 0)
                    {
                        strMsg += "UserID Not Found. ";
                    }

                    objApp.GetFMUserAllModuleName(out dsLocalSite);
                    if (dsLocalSite.Tables[0].Rows.Count > 0)
                    {

                        DataTable dtLocal = dsLocalSite.Tables[0];
                        for (int i = 0; i < dataKey.MODULE_NAMES.Count; i++)
                        {
                            
                            int dotIndex=dataKey.MODULE_NAMES[i].MODULENAME.IndexOf('.');
                            if (dotIndex != -1)
                            {
                                DataRow[] drLocal = dtLocal.Select("ModuleName ='" + dataKey.MODULE_NAMES[i].MODULENAME.Substring(0, dotIndex) + "'");
                                if (drLocal.Length <= 0)
                                {
                                    strMsg += dataKey.MODULE_NAMES[i].MODULENAME + " :Not Found; ";
                                }
                            }
                            else
                            {
                                DataRow[] drLocal = dtLocal.Select("ModuleName ='" + dataKey.MODULE_NAMES[i].MODULENAME.ToString() + "'");
                                if (drLocal.Length <= 0)
                                {
                                    strMsg += "Module Name: "+ dataKey.MODULE_NAMES[i].MODULENAME + " Not Found. ";
                                }
                            }
                        }

                    }
                    else
                    {

                    }

                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }

                if (DATA_OK == true)
                {
                    SaveFMUserAccManager(dataKey);

                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful. User Access saved successfully.";
                }
                else
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "API Execution failed.User Access " + strMsg;
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

        [AllowAnonymous]
        [HttpPost]
        [ActionName("DeleteFMUserAccManager")]
        [Route("FMNC8Api/User/DeleteFMUserAccManager")]
        public IActionResult DeleteFMUserAccManager([FromBody] FMUserAccManager dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            DataSet dsLocalUser = null;
            DataSet dsLocalSite = null;
            clsApplication objApp = null;
            try
            {
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();
                objApp= new clsApplication();

                UpdateLog("Call DeleteFMUserAccManager()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }

                if (DATA_OK == false)
                {
                    objApp.GetFMUserAllAccManager(out dsLocalSite, dataKey.USERID);
                    if (dsLocalSite.Tables[0].Rows.Count > 0)
                    {
                        DataTable dtLocal = dsLocalSite.Tables[0];
                        for (int i = 0; i < dataKey.MODULE_NAMES.Count; i++)
                        {
                            DataRow[] drLocal = dtLocal.Select("MODULENAME ='" + dataKey.MODULE_NAMES[i].MODULENAME.ToUpper() + "'");
                            if (drLocal.Length <= 0)
                            {
                                strMsg +="Module Nmae: "+ dataKey.MODULE_NAMES[i].MODULENAME + " Not Found. ";
                            }
                        }

                    }
                    else
                    {
                        strMsg += "UserID not found";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }

                if (DATA_OK == true)
                {
                    for (int i = 0; i < dataKey.MODULE_NAMES.Count; i++)
                    {
                        string userid = dataKey.USERID.ToString();
                        string moduleName = dataKey.MODULE_NAMES[i].MODULENAME.ToString();
                        objApp.DeleteFMUserAccManager(userid, moduleName);
                    }
                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful. Delete User Access successfully.";
                }
                else
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "API Execution failed. Delete User Access Failed." + strMsg;
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

        [AllowAnonymous]
        [HttpPost]
        [ActionName("GetGenID")]
        [Route("FMNC8Api/User/GetGenID")]
        public IActionResult GetGenID([FromBody] FMUserManagerModules dataKey)
        {
            GenIDResponse returnRes = null;
            bool DATA_OK = false;
            string strResCode = "";
            string strMsg = "";
            clsGenID getGenId = null;
            try
            {
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new GenIDResponse();
                getGenId = new clsGenID();

                UpdateLog("Call GetGenID()- ", DateTime.Now.ToString("MM-dd-yyyy") + "-Log");

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }

                if (DATA_OK == false)
                {
                    if (dataKey.MODULENAME == "")
                    {
                        strMsg += "Please Enter Module Name";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }

                if (DATA_OK == true)
                {
                    getGenId.GenID(System.DateTime.Now.ToShortDateString().ToString(), dataKey.MODULENAME.ToString(), out strResCode);
                    if (strResCode != "")
                    {
                        string firstL = dataKey.MODULENAME.ToString().Substring(0, 1).ToUpper();

                        returnRes.GenID = firstL + "-" + strResCode;
                    }
                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful.Generate ID successfully.";
                }
                else
                {
                    returnRes.GenID = "";
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "API Execution failed. Generate Id Failed";
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

        #region Support Function for User
        private void SaveFMUser(CreateFMUser dataKey)
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
                    objApp.GetFMUser(out dsLocal, dataKey.UserID.Trim());
                    dtLocal = dsLocal.Tables[0];
                    dvLocal = new DataView();
                    dvLocal.Table = dtLocal;
                    dvLocal.RowFilter = "UserID ='" + dataKey.UserID.ToString() + "'";

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
        private void UpdateTheDataRowFMUser(string OPN_FLAG, CreateFMUser dataKey, ref System.Data.DataRow drLocal)
        {
            try
            {
                var pass = bplib.clsWebLib.RetValidLen(dataKey.Password.ToString().Trim(), 20).ToString();
                pass = BPEnCodeDecodeLib.clsEnCodeDeCode.EncodeHI(pass, "bhsbhs");
                if (OPN_FLAG == "ADDNEW")
                {
                    drLocal["UserID"] = clsWebLib.RetValidLen(dataKey.UserID.ToString().Trim(),250);
                }

                drLocal["Password"] = pass;
                drLocal["UserGroup"] = bplib.clsWebLib.RetValidLen(dataKey.UserGroup.ToString().Trim(), 50);
                drLocal["UserLocation"] = bplib.clsWebLib.RetValidLen(dataKey.UserLocation.ToString().Trim(), 250);
                drLocal["UpdateOn"] = bplib.clsWebLib.DateData_AppToDB(dataKey.UpdateOn.ToString().Trim(),clsWebLib.DB_DATE_FORMAT);
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

        #region Support Function for SiteID
        private void SaveFMSiteID(CreateSiteID dataKey)
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
                    objApp.GetFMSiteID(out dsLocal, dataKey.SITEID.Trim().ToUpper());
                    dtLocal = dsLocal.Tables[0];
                    dvLocal = new DataView();
                    dvLocal.Table = dtLocal;
                    dvLocal.RowFilter = "SITEID ='" + dataKey.SITEID.ToString().ToUpper() + "'";

                    if (dvLocal.Count == 0)
                    { // Add new block
                        drLocal = dtLocal.NewRow();
                        UpdateTheDataRowFMSiteID("ADDNEW", dataKey, ref drLocal);
                        dtLocal.Rows.Add(drLocal);
                    }
                    else
                    {//edit block
                        drLocal = dvLocal[0].Row;
                        drLocal.BeginEdit();
                        UpdateTheDataRowFMSiteID("EDIT", dataKey, ref drLocal);
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
        private void UpdateTheDataRowFMSiteID(string OPN_FLAG, CreateSiteID dataKey, ref System.Data.DataRow drLocal)
        {
            try
            {
                if (OPN_FLAG == "ADDNEW")
                {
                    drLocal["SITEID"] = clsWebLib.RetValidLen(dataKey.SITEID.ToString().Trim().ToUpper(), 100);
                }

                drLocal["SITE_GROUP"] = bplib.clsWebLib.RetValidLen(dataKey.SITE_GROUP.ToString().Trim().ToUpper(), 150);
                drLocal["UpdateOn"] = bplib.clsWebLib.DateData_AppToDB(dataKey.UpdateOn.ToString().Trim(), clsWebLib.DB_DATE_FORMAT);
                drLocal["updateby"] = bplib.clsWebLib.RetValidLen(dataKey.updateby.ToString().Trim().ToUpper(), 100);

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

        #region Support Function for UserAccess
        private void SaveFMUserAccSite(CreateFMUserAccSite dataKey)
        {
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            System.Data.DataRow drLocal = null;
            System.Data.DataView dvLocal = null;
            clsApplication objApp = null;

            try
            {
                objApp = new clsApplication();
                objApp.GetFMUserAllAccSite(out dsLocal,dataKey.UserID);
                dtLocal = dsLocal.Tables[0];
                dvLocal = new DataView();
                dvLocal.Table = dtLocal;
                for(int i= 0; i < dataKey.SITE_IDs.Count(); i++)
                {
                    string userid = dataKey.UserID.ToString();
                    string siteid = dataKey.SITE_IDs[i].SITEID.ToString();
                    dvLocal.RowFilter = "UserID ='" + userid + "' and SITEID= '" + siteid + "'";
                    if (dvLocal.Count == 0)
                    { // Add new block
                        drLocal = dtLocal.NewRow();
                        UpdateTheDataRowFMUserAccSite("ADDNEW", userid, siteid, ref drLocal);
                        dtLocal.Rows.Add(drLocal);
                    }
                    else
                    {//edit block
                        drLocal = dvLocal[0].Row;
                        drLocal.BeginEdit();
                        UpdateTheDataRowFMUserAccSite("EDIT", userid, siteid, ref drLocal);
                        drLocal.EndEdit();
                    }

                    dvLocal.RowFilter = null;
                }
               
                objApp.SaveData(dsLocal);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void UpdateTheDataRowFMUserAccSite(string OPN_FLAG, string userID,string siteID, ref System.Data.DataRow drLocal)
        {
            try
            {
                if (OPN_FLAG == "ADDNEW")
                {
                    drLocal["UserID"] = clsWebLib.RetValidLen(userID.ToString().Trim(), 250);
                }

                drLocal["SITEID"] = bplib.clsWebLib.RetValidLen(siteID.ToString().Trim(), 100);

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

        #region Support Function for UserAccess Manager
        private void SaveFMUserAccManager(FMUserAccManager dataKey)
        {
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            System.Data.DataRow drLocal = null;
            System.Data.DataView dvLocal = null;
            clsApplication objApp = null;

            try
            {
                objApp = new clsApplication();
                objApp.GetFMUserAllAccManager(out dsLocal, dataKey.USERID);
                dtLocal = dsLocal.Tables[0];
                dvLocal = new DataView();
                dvLocal.Table = dtLocal;
                for (int i = 0; i < dataKey.MODULE_NAMES.Count(); i++)
                {
                    string userid = dataKey.USERID.ToString();
                    string moduleName = dataKey.MODULE_NAMES[i].MODULENAME.ToString();
                    dvLocal.RowFilter = "UserID ='" + userid + "' and MODULENAME= '" + moduleName + "'";
                    if (dvLocal.Count == 0)
                    { // Add new block
                        drLocal = dtLocal.NewRow();
                        UpdateTheDataRowFMUserAccManager("ADDNEW", userid, moduleName, ref drLocal);
                        dtLocal.Rows.Add(drLocal);
                    }
                    else
                    {//edit block
                        drLocal = dvLocal[0].Row;
                        drLocal.BeginEdit();
                        UpdateTheDataRowFMUserAccManager("EDIT", userid, moduleName, ref drLocal);
                        drLocal.EndEdit();
                    }

                    dvLocal.RowFilter = null;
                }

                objApp.SaveData(dsLocal);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void UpdateTheDataRowFMUserAccManager(string OPN_FLAG, string userID, string moduleName, ref System.Data.DataRow drLocal)
        {
            try
            {
                if (OPN_FLAG == "ADDNEW")
                {
                    drLocal["USERID"] = clsWebLib.RetValidLen(userID.ToString().Trim(), 250);
                }

                drLocal["MODULENAME"] = bplib.clsWebLib.RetValidLen(moduleName.ToString().Trim().ToUpper(), 100);

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
