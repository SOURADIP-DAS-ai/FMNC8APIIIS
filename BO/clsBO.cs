using BHSNCJW;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BHSAPIBaseNC8.BO
{
    public class clsBO
    {
        public bool CheckUserID(string username, string password)
        {
            bool OEMIDExist = false;
            System.Data.DataSet dsLocal = null;
            //connect to database standard way
            //check tblWhiteAgentIDList for WhiteAgentID as username and related SecretKey
            //If both match OEMIDExist = True; else False
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                password = BPEnCodeDecodeLib.clsEnCodeDeCode.EncodeHI(password, "bhsbhs");
                strSQl = @"select * from tblUser
                Where Userid='"+ username + "' And Password='"+ password + "'";

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsLocal, false, "1");

                if(dsLocal.Tables[0].Rows.Count>0)
                {
                    OEMIDExist = true;
                }

                return OEMIDExist;
            }
            catch (System.Exception ex)
            { 
                throw (ex); 
            }
            finally
            {
                objCon = null;
            }
           
        }

        public Response CheckUserIDWithSiteID(string username, string password,string siteID)
        {
            bool OEMIDExist = false;
            System.Data.DataSet dsLocal = null;
            Response response = null;
            //connect to database standard way
            //check tblWhiteAgentIDList for WhiteAgentID as username and related SecretKey
            //If both match OEMIDExist = True; else False
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                response = new Response();
                password = BPEnCodeDecodeLib.clsEnCodeDeCode.EncodeHI(password, "bhsbhs");
                strSQl = @"select * from tblUser
                Where Userid='" + username + "' And Password='" + password + "'";

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsLocal, false, "1");

                if (dsLocal.Tables[0].Rows.Count > 0)
                {
                    response.MessageCode = "1";
                    response.MessageText = "User Id and Password Verification Successful. | ";
                }
                else
                {
                    response.MessageCode = "0";
                    response.MessageText = "User Id and Password Verification Failed";
                }
                if (response.MessageCode == "1")
                {
                    strSQl = @"select * from USER_ACC_IN_SITE where UserID='" + username + "' and SITEID='" + siteID + "'";
                    objCon = new ConnectionManager.DAL.ConManager("1");
                    objCon.OpenDataSetThroughAdapter(strSQl, out dsLocal, false, "1");

                    if (dsLocal.Tables[0].Rows.Count > 0)
                    {
                        response.MessageCode = "1";
                        response.MessageText += "SiteID Verification Successful"; 
                    }
                    else
                    {
                        response.MessageCode = "0";
                        response.MessageText += "SiteID Verification Failed";
                    }
                }
                return response;
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }

        }

    }

}