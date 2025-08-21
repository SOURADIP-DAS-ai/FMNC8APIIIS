using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace BHSAPIBaseNC8.DBConnection
{
    public class ConnectionManagerHelper
    {
        public ConnectionManagerHelper()
        {
            // Constructor;
            LoginApplication();
        }//eof

        static string _user = "bpfin08mgr";
        static string _pass = "@bpfin08mgr";
        public static void LoginApplication()
        {
            try
            {
                Root root = DeSerializeAppsettingsJasonFile();

                LoginApplication(_user, _pass, "1", "NA", root.CMHelperSupprt.DATABASE_NAME, root.CMHelperSupprt.CONCODE, root.CMHelperSupprt.CONNAME, root.CMHelperSupprt.SERVER_NAME, root.CMHelperSupprt.SERVERTYPE);
            }
            catch (Exception e1)
            {
                throw (e1);

            }
            finally
            {
                //
            }
        }

        /// <summary>
        /// This function reading the set config file ... 
        /// </summary>
        /// <returns></returns>
        private static Root DeSerializeAppsettingsJasonFile()
        {
            try
            {
                Root Items = null;
                //var Path = System.AppDomain.CurrentDomain.BaseDirectory;
                //Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                //Path = appPathMatcher.Match(Path).Value;

                //using (StreamReader Reader = new StreamReader(Path + "\\" + "appsettings.json"))
                using (StreamReader Reader = new StreamReader("appsettings.json"))
                {
                    string json = Reader.ReadToEnd();
                    Items = JsonConvert.DeserializeObject<Root>(json);

                }
                return Items;
            }
            catch (Exception e1)
            {
                throw (e1);

            }
            finally
            {
                //
            }
        }//eof 


        /// <summary>
        /// using - CMHelper.json file // secondary option
        /// </summary>
        public static void LoginApplicationUsingCustomJsonFile()
        {
            try
            {
                conComponent conComponent = DeSerializeExternalComponent();

                LoginApplication(_user, _pass, "1", "NA", conComponent.DATABASE_NAME, conComponent.CONCODE, conComponent.CONNAME, conComponent.SERVER_NAME, conComponent.SERVERTYPE);
            }
            catch (Exception e1)
            {
                throw (e1);

            }
            finally
            {
                //
            }
        }//eof


        /// <summary>
        /// Using CUSTOMIZED Jason file put at application path;
        /// CMHelper.json file stcture 
        /// {
        /// "DATABASE_NAME": "BHSEAMS",
        /// "CONCODE": "1",
        /// "CONNAME" : "1",
        /// "SERVER_NAME": "(LOCAL)",
        /// "SERVERTYPE" :"SQlClient"
        /// }
        /// </summary>
        /// <returns></returns>
        private static conComponent DeSerializeExternalComponent()
        {
            try
            {
                conComponent conComponent = null;

                using (StreamReader file = File.OpenText(@"CMHelper.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    conComponent = (conComponent)serializer.Deserialize(file, typeof(conComponent));
                }
                return conComponent;
            }
            catch (Exception e1)
            {
                throw (e1);

            }
            finally
            {
                //
            }
        } // eof 



        /// <summary>
        /// Core function to initiate the connection Manager
        /// </summary>
        /// <param name="strUser"></param>
        /// <param name="strPassword"></param>
        /// <param name="strDefaultConID"></param>
        /// <param name="strPath"></param>
        /// <param name="Catalog"></param>
        /// <param name="ConnectionCode"></param>
        /// <param name="ConnectionName"></param>
        /// <param name="DataSource"></param>
        /// <param name="ServerType"></param>

        private static void LoginApplication(string strUser, string strPassword, string strDefaultConID, string strPath, string Catalog, string ConnectionCode, string ConnectionName, string DataSource, string ServerType)
        {
            ConnectionManager.DAL.ConBuilder objConBuilder = new ConnectionManager.DAL.ConBuilder();

            ///<param name="ServerType">Oledb, SQlClient, ODBC, OracleClient, OledbMSSQL, OledbOracle, OledbMSAcess2000</param>
            objConBuilder.ConnectionInitiator(Catalog, ConnectionCode, ConnectionName, DataSource, ServerType);

            /// ----- Set command lines no need to chnage---------- 
            ConnectionManager.DAL.ConBuilder.SetDefaultConnectionCode(strDefaultConID);

            /// The follwoing two command Lines don't use in the web application 
            /// in Application Start Event. In Application start I am just  				/// inistiting the variables. 
            /// I am calling this following two command lines directly from clsWebUISequrityControl.cs in heckDefaultConnection
            /// During ASP.NET - at default.aspx I used these two commnad line called from there. 
            /// web api case or asp.net core we have call these 2 line from here. 

            ConnectionManager.DAL.ConManager objCon = new ConnectionManager.DAL.ConManager(strDefaultConID);
            //ConManager obj = new ConManager();
            objCon.LoginApplication(strUser, strPassword, strDefaultConID, strPath);
        }//eof 
    }

    internal class conComponent
    {
        public string DATABASE_NAME { get; set; }
        public string CONCODE { get; set; }
        public string CONNAME { get; set; }
        public string SERVER_NAME { get; set; }
        public string SERVERTYPE { get; set; }
    }//eof


    /// <summary>
    /// This below part of MODEL sets is for reading appSetting.jason file reading. 
    /// I have used CMHelperSupprt support Model for our config; Another model is just as example how mutiple model can set in same in file.
    /// </summary>
    internal class Root
    {
        public CMHelperSupprt CMHelperSupprt { get; set; }
        public AnotherConfigSetting AnotherConfigSetting { get; set; }
    }
    internal class CMHelperSupprt
    {
        public string DATABASE_NAME { get; set; }
        public string CONCODE { get; set; }
        public string CONNAME { get; set; }
        public string SERVER_NAME { get; set; }
        public string SERVERTYPE { get; set; }
    }
    internal class AnotherConfigSetting
    {
        public string Info { get; set; }
    }
}

