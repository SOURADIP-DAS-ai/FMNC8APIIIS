using System.Data;

namespace BHSNCJW.BO
{
    public class clsApplication
    {
        public clsApplication() { }

        public void SaveData(System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.SaveDataSetThroughAdapter(ref dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End of Function
        public void GetEmployee(out System.Data.DataSet dsRef, string strEmpID)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Select *, FORMAT(DateOfBirth, 'dd-MMM-yyyy') as DOB from tblEmployee ";
                if (strEmpID.Trim() != "")
                {
                    strSql += " where EmpID = '" + strEmpID.Trim() + "' ";
                }

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function

        #region TblUser CRUD
        public void GetFMUser(out System.Data.DataSet dsRef, string strFMUserID)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Select * from tblUser ";
                if (strFMUserID.Trim() != "")
                {
                    strSql += " where UserID = '" + strFMUserID.Trim() + "' ";
                }

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function\
        public void DeleteFMUser(string strFMUserID)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Delete from tblUser where UserID = '" + strFMUserID.Trim() + "' ";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper(strSql, true, "1");
                objCon.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function
        #endregion

        #region TblUser_Site CRUD
        public void GetFMSiteID(out System.Data.DataSet dsRef, string strFMSiteID)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Select * from USER_SITE ";
                if (strFMSiteID.Trim() != "")
                {
                    strSql += " where SITEID = '" + strFMSiteID.Trim() + "' ";
                }

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function\
        public void DeleteFMSiteID(string strFMSiteID)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Delete from USER_SITE where Siteid = '" + strFMSiteID.Trim() + "' ";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper(strSql, true, "1");
                objCon.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function
        public void GetFMAllSiteID(out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Select SiteId from USER_SITE ";

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
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
        #endregion

        #region USER_ACC_IN_SITE CRUD
        public void GetFMUserAllAccSite(out System.Data.DataSet dsRef, string strFMUser)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Select * from USER_ACC_IN_SITE where userID = '" + strFMUser + "' ";


                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function
        public void DeleteFMUserAccSite(string userid,string siteid)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Delete from USER_ACC_IN_SITE where userID = '" + userid + "' and SiteID='"+ siteid + "' ";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper(strSql, true, "1");
                objCon.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function
        #endregion

        #region USER_ACC_MANAGER CRUD
        public void GetFMUserAllAccManager(out System.Data.DataSet dsRef, string strFMUser)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Select * from USER_ACC_MANAGER where userID = '" + strFMUser + "' ";


                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function
        public void GetFMUserAllModuleName(out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Select * from ApplicationModule";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function
        public void DeleteFMUserAccManager(string userid, string moduleName)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Delete from USER_ACC_MANAGER where userID = '" + userid + "' and MODULENAME='" + moduleName + "' ";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper(strSql, true, "1");
                objCon.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function
        public bool GetFMUserAccessCheck(string UserID, string moduleName)
        {
            ConnectionManager.DAL.ConManager objCon;
            System.Data.DataSet dsRef=null;
            string strSql = "";
            try
            {
                strSql = "Select * from USER_ACC_MANAGER where userID = '" + UserID + "' and MODULENAME='" + moduleName + "' ";


                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
                if(dsRef.Tables[0].Rows.Count>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function
        #endregion

        #region EntityFixedVariables CRUD
        public void GetFMFixedEntity(out System.Data.DataSet dsRef, string entityType, string entityCode)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Select * from EntityFixedVariables where entitytype= '"+ entityType + "' and code= '"+entityCode+"'";
                

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function\
        public void DeleteFMFixedEntity(string entityType, string entityCode)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Delete from EntityFixedVariables where entitytype= '" + entityType + "' and code= '" + entityCode + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper(strSql, true, "1");
                objCon.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function
        #endregion

        #region EntityFixedVariablesBySiteID CRUD
        public void GetFMFixedEntityBySiteID(out System.Data.DataSet dsRef, string entityType, string entityCode,string siteID)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Select * from EntityFixedVariablesBySiteID where entitytype= '" + entityType + "' and code= '" + entityCode + "' and SiteID='"+ siteID + "'";


                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function\
        public void DeleteFMFixedEntityBySiteID(string entityType, string entityCode,string siteID)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Delete from EntityFixedVariablesBySiteID where entitytype= '" + entityType + "' and code= '" + entityCode + "' and SiteID = '"+ siteID +"'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper(strSql, true, "1");
                objCon.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function
        #endregion

        #region tblAcCodeSet CRUD
        public void GetAccCodeSet(out System.Data.DataSet dsRef, string actCode, string siteID)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Select * from tblAcCodeSet where actCode= '" + actCode + "' and SiteID= '" + siteID + "'";


                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function\
        public void DeleteAccCodeSet(string accCode, string SiteID)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Delete from tblAcCodeSet where actcode= '" + accCode + "' and siteID= '" + SiteID + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper(strSql, true, "1");
                objCon.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function
        #endregion

        #region tblBudget CRUD
        public void GetBudget(out System.Data.DataSet dsRef, string budgetCode, string siteID)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Select * from tblBudget where budgetCode= '" + budgetCode + "' and SiteID= '" + siteID + "'";


                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function\
        public void DeleteBudget(string budgetCode, string SiteID)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Delete from tblBudget where budgetCode = '" + budgetCode + "' and siteID= '" + SiteID + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper(strSql, true, "1");
                objCon.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function
        #endregion

        #region tblIncome CRUD
        public void GetIncome(out System.Data.DataSet dsRef, string entryId, string siteID)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Select * from tblIncome where entryId= '" + entryId + "' and SiteID= '" + siteID + "'";


                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function\
        public void DeleteIncome(string entryId, string SiteID)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Delete from tblIncome where entryId = '" + entryId + "' and siteID= '" + SiteID + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper(strSql, true, "1");
                objCon.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function
        #endregion

        #region tblDailyExp CRUD
        public void GetDailyExp(out System.Data.DataSet dsRef, string expId, string siteID)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Select * from tblDailyExp where expId= '" + expId + "' and SiteID= '" + siteID + "'";


                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function\
        public void GetUserActIncome(out System.Data.DataSet dsRef, string userAc, string siteID)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Select * from tblIncome where userAc= '" + userAc + "' and SiteID= '" + siteID + "'";


                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function\
        public void DeleteDailyExp(string expId, string SiteID)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Delete from tblDailyExp where expId = '" + expId + "' and siteID= '" + SiteID + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper(strSql, true, "1");
                objCon.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function
        #endregion

        #region tblEmployee CRUD
        public void GetEmployeeByCode(string strEmpCode, string siteID,out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = @"Select * from tblEmployeeMaster";
                if (strEmpCode.Trim() != "")
                {
                    strSql += " where EmployeeCode = '" + strEmpCode.Trim() + "' and SiteId = '" + siteID + "'";
                }

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function
        public void GetEmployeeByEntryID(string strEntryID, string siteID, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = @"Select * from tblEmployeeMaster";
                if (strEntryID.Trim() != "")
                {
                    strSql += " where EmpEntryID = '" + strEntryID.Trim() + "' and SiteId = '" + siteID + "'";
                }

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function
        public void DeleteEmployeeByCode(string entryID, string SiteID)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Delete from tblEmployeeMaster where EmpEntryID = '" + entryID + "' and siteId= '" + SiteID + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper(strSql, true, "1");
                objCon.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function
        public void GetEmployeeEmail(string strEmail, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = @"Select * from tblEmployeeMaster where EmailAddress = '" + strEmail.Trim() + "'";
                
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End Function
        public void UpdateEmpImageName(string entryID,string imgName)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();

                strSQl = @"update tblEmployeeMaster set EmployeeImageName ='"+ imgName + "' Where EmpEntryID = '" + entryID + "'";

                objCon.ExecuteNonQueryWrapper(strSQl, true, "1");

                objCon.CommitTransaction();

            }
            catch (Exception ex)
            {
                objCon.RollBack();
                throw (ex);
            }
            finally
            {
                objCon.CloseConnection();
                objCon = null;
            }
        }//EOF
        #endregion

        #region SqlQuery Tran
        public void GetSQLOutput(string sqlQuery, out DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(sqlQuery, out dsRef, false, false, "", "1");
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
        #endregion


    }
}
