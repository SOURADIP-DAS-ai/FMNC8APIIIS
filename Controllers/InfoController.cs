using BHSAPIBaseNC8.DBConnection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BHSNCJW.Controllers
{
    public class InfoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public InfoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet]
        [ActionName("About")]
        [Route("BHSNCJW/Info/About")]
        public IActionResult About()
        {
            string ss = " Update on : 25FEB2025; Add new endpoint For new table entity type by site id. Module : FMNC8; (Version 8.0.0)(Build No : 02252025.01)";
            String ms = "This is publish on iis server and Under push request and merged by admin";
            try
            {
                return Ok(ss+ms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                //
            }
        }//EOF

        [AllowAnonymous]
        [HttpPost]
        [ActionName("InfoAboutHost")]
        [Route("BHSNCJW/Info/InfoAboutHost")]
        public IActionResult InfoAboutHost([FromBody] HostInfo dataKey)
        {
            Response returnRes = null;
            bool DATA_OK = false;
            string strMsg = "";
            
            try
            {
                returnRes = new Response();
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }
                if (DATA_OK == false)
                {
                    if (dataKey.Sys_Config_PIN !="999999")
                    {
                        strMsg += "Incorrect PIN";
                    }
                    if (strMsg == "")
                    {
                        DATA_OK = true;
                    }
                }
                if (DATA_OK == true)
                {
                    string scheme=HttpContext.Request.Scheme;
                    string host = HttpContext.Request.Host.Value.ToString();
                    string DBHostName = "DBName:"+_configuration.GetSection("CMHelperSupprt:DATABASE_NAME").Value.ToString() +", Host:"+ scheme+"://"+host+" JWT TOKEN Expire in 2 hours from login time.";
                    returnRes.MessageCode = "1002";
                    returnRes.MessageText = "API Execution successful. "+DBHostName;
                }
                else
                {
                    returnRes.MessageCode = "1001";
                    returnRes.MessageText = "API Execution failed." + strMsg;
                }
                return Ok(returnRes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                //
            }
        }//EOF 
    }
}
