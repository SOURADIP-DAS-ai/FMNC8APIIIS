using BHSAPIBaseNC8.JWTToken;
using BHSNCJW;
using bplib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHSAPIBaseNC8.Controllers
{
    public class JWTTokenController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [ActionName("VerifyMe")]
        [Route("BHSNCJW/JWTToken/VerifyMe")]
        public IActionResult VerifyMe([FromBody] modAuthTokenRequest dataKey)
        {
            JWTResponse objResponce = null;
            try
            {
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                objResponce = new JWTResponse();

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }

                //OEM_Partner_Id should be checked by database
                if (CheckUserIdAndPassword(dataKey.UserID, dataKey.Password) == true)
                {
                    objResponce.MessageCode = "1002";
                    objResponce.MessageText = "Welcome... you are verified. A JWT token has issued.  You need to use same other end point access.";
                    objResponce.JWTToken = JwtManager.GenerateToken(dataKey.UserID);
                }
                else
                {
                    objResponce.MessageCode = "1001";
                    objResponce.MessageText = "User ID Verification failed";
                }

                return Ok(objResponce);
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


        //Login auth with site id

        [AllowAnonymous]
        [HttpPost]
        [ActionName("Login")]
        [Route("BHSNCJW/JWTToken/Login")]
        public IActionResult Login([FromBody] modAuthTokenRequestWithSiteID dataKey)
        {
            JWTResponse objResponce = null;
            Response response = null;
            try
            {
                dataKey = clsWebLib.CheckDataKeyValue(dataKey);
                objResponce = new JWTResponse();
                response = new Response();
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }
                response = CheckUserIdAndPasswordWithSiteID(dataKey.UserID, dataKey.Password, dataKey.SITEID);
                //OEM_Partner_Id should be checked by database
                if (response.MessageCode == "1")
                {
                    objResponce.MessageCode = "1002";
                    objResponce.MessageText = "Welcome... you are verified. A JWT token has issued.  You need to use same other end point access.";
                    objResponce.JWTToken = JwtManager.GenerateToken(dataKey.UserID);
                }
                else
                {
                    objResponce.MessageCode = "1001";
                    objResponce.MessageText = response.MessageText;
                }

                return Ok(objResponce);
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

        public bool CheckUserIdAndPassword(string username, string password)
        {
            var objBO = new BHSAPIBaseNC8.BO.clsBO();
            try
            {
                // should check in the database               

                return objBO.CheckUserID(username, password);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objBO = null;
            }

        }//EOF
        public Response CheckUserIdAndPasswordWithSiteID(string username, string password,string siteID)
        {
            var objBO = new BHSAPIBaseNC8.BO.clsBO();
            try
            {
                // should check in the database               
                return objBO.CheckUserIDWithSiteID(username, password, siteID);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objBO = null;
            }

        }//EOF
    }
}
