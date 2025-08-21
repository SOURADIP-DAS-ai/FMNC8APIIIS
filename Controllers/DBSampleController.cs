using BHSAPIBaseNC8.JWTToken;
using BHSNCJW.BO;
using bplib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BHSNCJW.Controllers
{
    public class DBSampleController : ControllerBase
    {
        //Iconfiguration object initiated to read appsetting.json.
        //Program.cs accessed in constructor to get the file path        
        private readonly IConfiguration _configuration;
        public DBSampleController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        [JWTToken]
        [HttpPost]
        [ActionName("UpdateEmployee")]
        [Route("BHSNCJW/DBSample/UpdateEmployee")]
        public IActionResult UpdateEmployee([FromBody] modEmployee dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            try
            {
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                returnRes = new Response();

                UpdateLog("Call UpdateEmployee()- ", dataKey.EmployeeID);

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }

                if (DATA_OK == false)
                {
                    if (dataKey.EmployeeID.Length > 50)
                    {
                        strMsg = "Please Insert EmployeeID...(Max length 50)";
                    }
                    if (clsWebLib.IsDateOK(dataKey.EmployeeDOB) == false)
                    {
                        strMsg = "Please enter Date Of Bitrh...(allowed format is  dd-MMM-yyyy ex: '01-jan-2008')";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }

                if (DATA_OK == true)
                {
                    SaveEmployee(dataKey);

                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful. Employee saved successfully.";
                }
                else
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "API Execution failed. Employee not saved." + strMsg;
                }

                UpdateLog(returnRes.MessageText, dataKey.EmployeeID);

                return Ok(returnRes);
            }
            catch (Exception ex)
            {
                UpdateLog(ex.Message, dataKey.EmployeeID);
                return BadRequest(ex.Message);
            }
            finally
            {
                //
            }

        }//EOF


        #region Support Function
        private void SaveEmployee(modEmployee dataKey)
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
                    objApp.GetEmployee(out dsLocal, dataKey.EmployeeID.Trim());
                    dtLocal = dsLocal.Tables[0];
                    dvLocal = new DataView();
                    dvLocal.Table = dtLocal;
                    dvLocal.RowFilter = "EmpID ='" + dataKey.EmployeeID.ToString() + "'";

                    if (dvLocal.Count == 0)
                    { // Add new block
                        drLocal = dtLocal.NewRow();
                        UpdateTheDataRowEmployee("ADDNEW", dataKey, ref drLocal);
                        dtLocal.Rows.Add(drLocal);
                    }
                    else
                    {//edit block
                        drLocal = dvLocal[0].Row;
                        drLocal.BeginEdit();
                        UpdateTheDataRowEmployee("EDIT", dataKey, ref drLocal);
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
        private void UpdateTheDataRowEmployee(string OPN_FLAG, modEmployee dataKey, ref System.Data.DataRow drLocal)
        {
            try
            {
                if (OPN_FLAG == "ADDNEW")
                {
                    drLocal["EmpID"] = clsWebLib.RetValidLen(dataKey.EmployeeID.ToString().Trim(), 50);
                }

                drLocal["EmployeeName"] = bplib.clsWebLib.RetValidLen(dataKey.EmployeeName.ToString().Trim(), 100);
                drLocal["EmpDesig"] = bplib.clsWebLib.RetValidLen(dataKey.EmployeeDesig.ToString().Trim(), 50);
                drLocal["EmpSalary"] = bplib.clsWebLib.RetValidLen(dataKey.EmployeeSalary.ToString().Trim(), 50);
                drLocal["DateOfBirth"] = "" + bplib.clsWebLib.DateData_AppToDB(dataKey.EmployeeDOB, bplib.clsWebLib.DB_DATE_FORMAT);
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
