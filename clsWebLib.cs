using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace bplib
{
    public class clsWebLib
    {
        public static readonly string DB_DATE_FORMAT = "MM/dd/yyyy";
        public static readonly string STD_DATE_FORMAT = "dddd,MMMM dd,yyyy";
        public static readonly string REPORT_DB_NAME = "BpNewReport.mdb";

        #region customized function
        public clsWebLib()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string BappsMultiLineStringBuilder(string str)
        {
            int i = 0;
            char[] param = { '\n' };
            char[] lineEnd = { '\r' };
            string ss = "";
            if (str.Trim() == "")
            {
                return ("");
            }
            string[] lines = str.Split(param);
            foreach (string s in lines)
            {
                lines[i++] = s.TrimEnd(lineEnd);
            }
            foreach (string line in lines)
            {
                ss = ss + line + "<br>";
            }

            return (ss);

        }//end of function

        public static object RetValidLen(string str, int How_Long_Should_It_Be)
        {

            string removechar = "";
            if (str.Trim() == "")
            {
                return (object)Convert.DBNull;
            }
            removechar = str.Trim();
            removechar = removechar.Replace("'", " ");
            if ((removechar.Trim()).Length > How_Long_Should_It_Be)
            {
                return (object)(removechar.Substring(1, How_Long_Should_It_Be));
            }
            else
            {
                return (object)removechar.Trim();
            }
        }//end of function
        #endregion

        #region App Date / time type data managment
        //sample use
        //txtModificationDate.Text = bplib.clsutilib.DateData_DBToApp(dsLocal.Tables[0].Rows[0]["ModificationDate"].ToString()).ToString("d");
        //drLocal["ModificationDate"] =bplib.clsutilib.DateData_AppToDB(System.DateTime.Now,clsStartUp.DB_DATE_FORMAT);
        private static bool DateOkCheck(string strdate)
        {
            try
            {
                System.DateTime myDt = System.Convert.ToDateTime(strdate);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            finally
            {
                //
            }
        }// end function
        public static object chk_NullDateData(object dateValue)
        {
            if (DateOkCheck("" + dateValue.ToString()) == false)
            {
                dateValue = "";
            }

            if (("" + dateValue.ToString()) == "")
            {
                System.DateTime dt = new System.DateTime(1901, 1, 1);
                dateValue = (object)dt;
            }
            return (object)dateValue;
        }
        public static System.DateTime AppDateConvert(object dateValue, string input_date_format, string output_date_format)
        {
            string strDate = null;
            dateValue = chk_NullDateData(dateValue);
            strDate = dateValue.ToString();
            if (strDate != "")
            {
                if (input_date_format.Trim() != "")
                {
                    if (output_date_format.Trim() != "")
                    {
                        System.Globalization.DateTimeFormatInfo InputFormat = new System.Globalization.DateTimeFormatInfo();
                        InputFormat.ShortDatePattern = input_date_format;
                        System.DateTime myDt = System.Convert.ToDateTime(strDate, InputFormat);
                        strDate = myDt.ToString(output_date_format);
                    }
                }
            }
            return System.Convert.ToDateTime(strDate);
        }// End of function


        public static System.DateTime DateData_AppToDB(object dateValue, string DB_Level_date_format)
        {
            string strDate = null;
            strDate = dateValue.ToString();
            if (DB_Level_date_format != "")
            {
                // Collecting the user terminal set format 
                System.Globalization.DateTimeFormatInfo USER_TERMINAL_DATE_FORMAT = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat;
                strDate = AppDateConvert(strDate, USER_TERMINAL_DATE_FORMAT.ShortDatePattern.ToString(), DB_Level_date_format).ToString();
            }
            return System.Convert.ToDateTime(strDate);
        }// End of function

        public static System.DateTime DateData_DBToApp(object dateValue)
        {
            string strDate = null;
            strDate = dateValue.ToString();

            System.Globalization.DateTimeFormatInfo myDBDateFormat = new System.Globalization.CultureInfo("en-US", false).DateTimeFormat;
            strDate = DateData_DBToApp(dateValue, myDBDateFormat.ShortDatePattern.ToString()).ToString();
            return System.Convert.ToDateTime(strDate);
        }// End function
        public static System.DateTime DateData_DBToApp(object dateValue, string DB_Level_date_format)
        {
            string strDate = null;
            strDate = dateValue.ToString();
            if (DB_Level_date_format != "")
            {
                // Collecting the user terminal set format 
                System.Globalization.DateTimeFormatInfo USER_TERMINAL_DATE_FORMAT = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat;
                strDate = AppDateConvert(strDate, DB_Level_date_format, USER_TERMINAL_DATE_FORMAT.ShortDatePattern.ToString()).ToString();
            }
            return System.Convert.ToDateTime(strDate);
        }// End of function

        public static String makeBaseBlank(object dateValue)
        {
            System.DateTime dt;
            dt = System.Convert.ToDateTime(dateValue.ToString());
            if (dt.Year == 1901)
            {
                return "";
            }
            else
            {
                return dateValue.ToString();
            }
        }// End of function

        public static string AppSysTimeFormat(object TimeValue)
        {
            string strTime = null;
            strTime = TimeValue.ToString();
            if (strTime != "")
            {
                System.Globalization.DateTimeFormatInfo AppTimeFormat = new System.Globalization.DateTimeFormatInfo();
                AppTimeFormat.ShortTimePattern = "HH:mm:ss";
                System.DateTime dt = System.Convert.ToDateTime(strTime, AppTimeFormat);
                strTime = dt.ToString();
            }
            return (string)strTime;
        } //End function
        public static string getUserDateFormat()
        {
            System.Globalization.DateTimeFormatInfo USER_TERMINAL_DATE_FORMAT = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat;
            return USER_TERMINAL_DATE_FORMAT.ShortDatePattern.ToString();
        }
        public static string getUserDateSeparator()
        {
            System.Globalization.DateTimeFormatInfo USER_TERMINAL_DATE_FORMAT = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat;
            return USER_TERMINAL_DATE_FORMAT.DateSeparator.ToString();
        }
        #endregion


        #region DataType Checking
        public static bool IsDateOK(string strdate)
        {
            try
            {
                if (strdate.Length != 11)
                {
                    return false;
                }
                if (strdate.Substring(2, 1) != "-" && strdate.Substring(6, 1) != "-")
                {
                    return false;
                }
                System.DateTime myDt = System.Convert.ToDateTime(strdate);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            finally
            {
                //
            }
        }// end function
        public static bool IsDateOK2(string inputDate)  //yyyy-MM-dd
        {
            DateTime date;
            if (DateTime.TryParseExact(inputDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out date))
            {
                // Check if the parsed date is not greater than today's date
                if (date.Date <= DateTime.Today)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsDateOK3(string inputDate)  //yyyy-MM-dd
        {
            DateTime date;
            DateTime year =Convert.ToDateTime("01-JAN-1901");
            if (DateTime.TryParseExact(inputDate, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.None, out date))
            {
                // Check if the parsed date is not greater than today's date
                if (date.Date.Year > year.Year )
                {
                    return true;
                }
            }
            return false;
        }
        public static bool CheckValidFromDateToDate(string fromDate, string toDate)
        {
            DateTime fromDateValue;
            DateTime toDateValue;

            // Attempt to parse the input dates
            if (DateTime.TryParseExact(fromDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out fromDateValue) &&
                DateTime.TryParseExact(toDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out toDateValue))
            {
                // Check if the fromDate is not greater than toDate
                if (fromDateValue.Date <= toDateValue.Date)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsNumeric(string strNumber)
        {
            Double d;
            System.Globalization.NumberFormatInfo n = new System.Globalization.NumberFormatInfo();
            if (strNumber.Length == 0)
            {
                return false;
            }
            return Double.TryParse(strNumber, System.Globalization.NumberStyles.Float, n, out d);
        } // End Function
        public static string GetNumData(string strNumber)
        {
            double d;
            System.Globalization.NumberFormatInfo n = new System.Globalization.NumberFormatInfo();
            if (strNumber.Trim() == "")
            { return "0"; }
            else if (System.Double.TryParse(strNumber, System.Globalization.NumberStyles.Float, n, out d) == true)
            {
                return strNumber;
            }
            else
            {
                return "0";
            }
        }// end function
        public static bool IsValidAbsoluteNumber(string strNumber)
        {
            string S1 = "+";
            string S2 = "-";

            if (strNumber.Contains(S1) == true)
            {
                return false;
            }
            if (strNumber.Contains(S2) == true)
            {
                return false;
            }

            return true;

        }// end function
        public static string ChkDBNull(ref String FldValue)
        {
            string s;
            if (Convert.IsDBNull(FldValue) == true)
            {
                s = "";
            }
            else
            {
                s = (string)FldValue;
            }
            return s;
        } // End Function
        
        #endregion

        #region Time Checking
        public static bool IsTimeOK(string strTime)
        {
            try
            {
                string format = "HH:mm:ss";

                if (DateTime.TryParseExact(strTime, format, null, DateTimeStyles.None, out DateTime parsedTime))
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
                return false;
            }
            finally
            {
                //
            }
        }// end function
        #endregion

        #region CheckNumeric_Lenchk
        public static bool ChkEntryNumeric(string str, char keyP)
        {
            int x = 0;
            int i;
            int KeyAssci;
            KeyAssci = Convert.ToInt32(keyP);
            if (KeyAssci == 8 || KeyAssci == 13)
            {
                return false;
            }
            if (KeyAssci == 46)
            {
                for (i = 1; i <= str.Length; ++i)
                {
                    x = String.CompareOrdinal(str, i, ".", 0, 1);
                    if (x == 0)
                    {
                        return true;
                    }
                }
            }
            if ((KeyAssci < 48 || KeyAssci > 57) && (KeyAssci != 46))
            {
                return true;
            }

            return false;


        }
        public static bool LenChk(String str, int lenStr, char KeyP)
        {
            if (Convert.ToInt32(KeyP) == 8)
            {
                return false;
            }
            if (str.Length >= lenStr)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Check email patern
        public static bool CheckEmailPatern(string strInputEmail)
        {
            try
            {
                string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

                if (Regex.IsMatch(strInputEmail.ToLower(), pattern) == true)
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
                return false;

            }
        }

        #endregion

        #region Check Zero start at phone number
        public static string RemoveLendingZero(string PhoneNumber)
        {
            string strPhoneNumber = "";

            if (PhoneNumber.Trim() == "")
            {
                //return strPhoneNumber;
            }
            else
            {
                strPhoneNumber = PhoneNumber.TrimStart(new Char[] { '0' });
            }

            return strPhoneNumber;
        }//eof

        #endregion

        #region Check (CN) Uni code char input 
        public static bool CheckCNCharInput(string strInputText)
        {
            var charArray = strInputText.ToCharArray();
            var isChineseTextPresent = false;

            foreach (var character in charArray)
            {
                var cat = char.GetUnicodeCategory(character);


                if (cat != System.Globalization.UnicodeCategory.OtherLetter)
                {
                    continue;
                }

                isChineseTextPresent = true;
                break;
            }
            return isChineseTextPresent;
        }
        #endregion

        #region Support function for Ultra

        //Sample use
        //		private void LoadStaticInfo()
        //		{
        //			
        //			string[] EntityTypes = {"CutNo","Pattern","Color","Size"};
        //			BS.clsStylePlan objPLan;
        //			try
        //			{
        //				objPLan = new BS.clsStylePlan();				
        //				ulCboOrder.DataSource = CreateNewTableForArray("Order by", EntityTypes );
        //				
        //				ulCboCritea.DataSource=CreateDataTableForCriteria();
        //			}
        //			catch(System.Exception ex)
        //			{
        //				MessageBox.Show(this, ex.ToString(),"System",MessageBoxButtons.OK, MessageBoxIcon.Error);
        //			}
        //		}//End function

        public static System.Data.DataTable CreateNewTableForArray(string ColumnName, string[] arr_Value)
        {
            System.Data.DataTable dtOut;
            try
            {
                System.Data.DataColumn col;
                dtOut = new System.Data.DataTable();
                col = new System.Data.DataColumn();
                col.DataType = typeof(string);
                col.ColumnName = ColumnName;
                dtOut.Columns.Add(col);

                System.Data.DataRow newrow;
                foreach (string strValue in arr_Value)
                {
                    newrow = dtOut.NewRow();
                    newrow[ColumnName] = strValue;
                    dtOut.Rows.Add(newrow);
                }
                return dtOut;
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                dtOut = null;
            }
        }//End function

        //ultraGridFinPlan.DataSource=BS.clsUltraUIManager.CreateNewVirtualTable( dsLocal.Tables[0],"FileEntryID","FileNo","Patern","CmbOrShade","Size","FoldeNo","QtyBook");

        public static System.Data.DataTable CreateNewVirtualTable(System.Data.DataTable dtINPut, params string[] ColumnNames)
        {
            System.Data.DataTable dtOut;
            try
            {
                System.Data.DataColumn col;
                dtOut = new System.Data.DataTable();

                foreach (string colname in ColumnNames)
                {
                    col = new System.Data.DataColumn();
                    col.DataType = dtINPut.Columns[colname].DataType;
                    col.ColumnName = dtINPut.Columns[colname].ColumnName;
                    col.AllowDBNull = dtINPut.Columns[colname].AllowDBNull;
                    dtOut.Columns.Add(col);
                    col = null;
                }

                System.Data.DataRow newrow;
                foreach (System.Data.DataRow row in dtINPut.Rows)
                {
                    newrow = dtOut.NewRow();
                    foreach (string colname in ColumnNames)
                    {
                        newrow[colname] = row[colname];
                    }
                    dtOut.Rows.Add(newrow);
                }

                return dtOut;
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                dtOut = null;
            }
        }//End function
        public static System.Data.DataTable CreateNewVirtualTable(params string[] ColumnNames)
        {
            System.Data.DataTable dtOut;
            int i = 0;
            try
            {
                System.Data.DataColumn col;
                dtOut = new System.Data.DataTable();

                foreach (string colname in ColumnNames)
                {
                    col = new System.Data.DataColumn();
                    col.DataType = typeof(System.String);
                    col.MaxLength = 50;
                    col.ColumnName = colname;
                    col.AllowDBNull = true;
                    dtOut.Columns.Add(col);
                    col = null;
                }
                i = dtOut.Columns.Count;
                return dtOut;
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                dtOut = null;
            }
        }//End function

        internal static object RetValidLenfromFirst(string str, int How_Long_Should_It_Be)
        {
            
            try
            {
                if (string.IsNullOrWhiteSpace(str.Trim()))
                {
                    return (object)Convert.DBNull;
                }
                else
                {
                    str = str.Replace("'", "");
                    if (string.IsNullOrWhiteSpace(str.Trim()))
                    {
                        return (object)Convert.DBNull;
                    }
                    else
                    {
                        if (str.Length> How_Long_Should_It_Be)
                        {
                            return (object)(str.Substring(0, (How_Long_Should_It_Be - 1)));
                        }
                        else
                        {
                            return (object)str.Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//eof
        public static string CheckNull(string str)
        {
            if (str == null)
            {
                return "";
            }
            else
            {
                return str;
            }

        }//eof
        #endregion

        #region Create Log       
        //Create Logs Files
        public static void FeedBackToLog(string strLog, string strFileName_With_Path)
        {
            FileInfo logFileInfo = new FileInfo(strFileName_With_Path);
            DirectoryInfo logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            using (FileStream fileStream = new FileStream(strFileName_With_Path, FileMode.Append))
            {
                using (StreamWriter log = new StreamWriter(fileStream))
                {
                    log.WriteLine(strLog);
                }
            }
        }//eof
        public static string MyLogFile(string ProcID, string strFilePath)
        {
            //string MyLogFile = strFilePath + @"\" + ProcID + "-" + System.DateTime.Today.ToString("MMddyyyy") + ".txt";
            string MyLogFile = strFilePath + @"\" + ProcID + ".txt";
            return MyLogFile;
        }//eof

        //public static void downloadFile(string strfilePath)
        //{
        //    string path;
        //    System.IO.FileInfo docFile;
        //    try
        //    {
        //        //path = getFilePath(dirName, fileName);
        //        docFile = new System.IO.FileInfo(strfilePath);
        //        if (docFile.Exists)
        //        {
        //            HttpContext.Current.Response.Clear();
        //            HttpContext.Current.Response.ClearContent();
        //            HttpContext.Current.Response.ClearHeaders();
        //            HttpContext.Current.Response.ContentType = "application/octet-stream";
        //            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + docFile.Name);
        //            HttpContext.Current.Response.WriteFile(strfilePath);
        //            HttpContext.Current.Response.End();

        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch (System.IO.IOException ex)
        //    {
        //        //
        //    }
        //    catch (Exception ex)
        //    {
        //        //
        //    }
        //    finally
        //    {
        //        docFile = null;
        //    }
        //}//eof

        #endregion

        #region Mail
        public static bool IsValidEmail(string email)
        {
            //https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }//eof
        public static bool isEmail(string inputEmail)
        {
            string[] strArray;
            bool isOk = false;
            try
            {
                if (inputEmail != "")
                {
                    //string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                    //      @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                    //      @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

                    string strRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

                    System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(strRegex);

                    if ((inputEmail.IndexOf(",")) > 0)
                    {
                        if (inputEmail.LastIndexOf(',') == (inputEmail.Length - 1))
                        {
                            inputEmail = inputEmail.Substring(0, inputEmail.LastIndexOf(','));
                        }
                        strArray = inputEmail.Split(',');
                        int i = strArray.Length; //for test
                        foreach (string mailAddr in strArray)
                        {
                            if (re.IsMatch(mailAddr))
                            {
                                isOk = true;
                            }
                            else
                            {
                                isOk = false;
                                break;
                            }
                        }
                    }
                    else
                    {

                        if (re.IsMatch(inputEmail))
                        {
                            isOk = true;
                        }
                        else
                        {
                            isOk = false;
                        }
                    }
                }
                else
                {
                    isOk = true;  //if email id is a mandatory field then it will be false
                }

                return isOk;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
        }//eof
        public static string genMCEntryId()
        {
            string TransactionID = Guid.NewGuid().ToString("N");
            return TransactionID;
        }//eof
        #endregion

        #region Input Check For Sql Injection
        public static T CheckDataKeyValue<T>(T dataKey)
        {
            string[] oddStrings = { " + ", " - ", "++", "--", "'", "''", "`", "``", ",", "\"", "\"\"", "/", "//", "\\", "\\\\", ";", "' or \"", "-- or #", "' OR '1", "' OR 1 -- -", "\" OR \"\" = \"", "\" OR 1 = 1 -- -", "' OR '' = '" };

            try
            {
                if (dataKey != null)
                {
                    var jsonString = JsonConvert.SerializeObject(dataKey);
                    var jsonObject = JObject.Parse(jsonString);
                    CheckJsonObject(jsonObject, oddStrings);
                    dataKey = JsonConvert.DeserializeObject<T>(jsonObject.ToString());
                }
                return dataKey;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Recursive function to check JSON object
        private static void CheckJsonObject(JObject jsonObject, string[] oddStrings)
        {
            try
            {
                foreach (var property in jsonObject.Properties().ToList())
                {
                    // Skip specific fields
                    if (property.Name.ToUpper() == "MASKEDPAN" || property.Name.ToUpper() == "RSAPUBLICKEY" || property.Name.ToUpper() == "TOKENID")
                    {
                        continue;
                    }

                    if (property.Value.Type == JTokenType.Object)
                    {
                        // If the property is a nested object, recursively check it
                        CheckJsonObject((JObject)property.Value, oddStrings);
                    }
                    else if (property.Value.Type == JTokenType.Array)
                    {
                        // If the property is an array, check each item in the array
                        CheckJsonArray((JArray)property.Value, oddStrings);
                    }
                    else if (property.Value.Type == JTokenType.String)
                    {
                        string value = property.Value.ToString();
                        if (oddStrings.Any(odd => value.Contains(odd)))
                        {
                            // Replace the value with '0Z' if it contains any odd string
                            property.Value = "0Z";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        // Recursive function to check JSON array
        private static void CheckJsonArray(JArray jsonArray, string[] oddStrings)
        {
            try
            {
                for (int i = 0; i < jsonArray.Count; i++)
                {
                    if (jsonArray[i].Type == JTokenType.Object)
                    {
                        // If the item is a nested object, recursively check it
                        CheckJsonObject((JObject)jsonArray[i], oddStrings);
                    }
                    else if (jsonArray[i].Type == JTokenType.Array)
                    {
                        // If the item is an array, recursively check it
                        CheckJsonArray((JArray)jsonArray[i], oddStrings);
                    }
                    else if (jsonArray[i].Type == JTokenType.String)
                    {
                        string value = jsonArray[i].ToString();
                        if (oddStrings.Any(odd => value.Contains(odd)))
                        {
                            // Replace the value with '0Z' if it contains any odd string
                            jsonArray[i] = "0Z";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Check MaskedPan Pattern
        public static bool IsValidMaskedPan(string pan)
        {
            // Regex to match a masked PAN with the first 6 and last 4 digits visible and 6 '*' characters in between
            string pattern = @"^\d{6}\*{6}\d{4}$";
            return Regex.IsMatch(pan, pattern);
        }

        #endregion

        #region Check Phone number
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            // Regular expression to validate phone numbers
            string pattern = @"^(\+?[1-9]\d{1,14}|[6-9]\d{9})$";

            /*
             * Explanation of the pattern:
             * - `^` and `$`: Ensure the pattern matches the entire string.
             * - `(\+?[1-9]\d{1,14})`: Matches international numbers in E.164 format (e.g., +1234567890).
             * - `|`: OR condition.
             * - `[6-9]\d{9}`: Matches 10-digit Indian mobile numbers (e.g., 9876543210).
             */

            return Regex.IsMatch(phoneNumber, pattern);
        }
        #endregion
        public static string ChkDBNull2(object obj)
        {
            if (string.IsNullOrEmpty((string)obj))
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }

        }//eof
    }
}