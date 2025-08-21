using BHSNCJW.BO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace BHSNCJW
{
    public class mngJWT
    {

        public modJWTOutput ReadJWT(string JWTToRead)
        {
            modJWTOutput JWTOutput = null;
            try
            {
                JWTOutput = new modJWTOutput();

                //Assume the input is in a control called txtJwtIn,
                //and the output will be placed in a control called txtJwtOut
                var jwtHandler = new JwtSecurityTokenHandler();

                //Check if readable token (string is in a JWT format)
                var readableToken = jwtHandler.CanReadToken(JWTToRead);

                if (readableToken != true)
                {
                    System.Exception ex = new System.Exception("The token doesn't seem to be in a proper JWT format.");
                    
                }

                if (readableToken == true)
                {
                    var token = jwtHandler.ReadJwtToken(JWTToRead);

                    //Extract the headers of the JWT
                    var headers = token.Header;
                    var jwtHeader = "{";
                    foreach (var h in headers)
                    {
                        jwtHeader += '"' + h.Key + "\":\"" + h.Value + "\",";
                    }
                    jwtHeader += "}";

                    //JWTOutput.JWTHeader = "Header:\r\n" + JToken.Parse(jwtHeader).ToString(Formatting.Indented);
                    JWTOutput.JWTHeader = JToken.Parse(jwtHeader).ToString(Formatting.Indented);

                    //Extract the payload of the JWT
                    var claims = token.Claims;
                    var jwtPayload = "{";
                    foreach (Claim c in claims)
                    {
                        jwtPayload += '"' + c.Type + "\":\"" + c.Value + "\",";
                    }
                    jwtPayload += "}";
                    //JWTOutput.JWTClaim = "\r\nClaims:\r\n" + JToken.Parse(jwtPayload).ToString(Formatting.Indented);
                    JWTOutput.JWTClaim = JToken.Parse(jwtPayload).ToString(Formatting.Indented);
                }

                return JWTOutput;

            }
            catch(System.Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
        }//eof

        public bool GetUserIDAndCheckAccess(string jwtToken, string moduleName,out string userID)
        {
            clsApplication objApp= null;
            try
            {
                objApp = new clsApplication();
                modJWTOutput jwtObj = ReadJWT(jwtToken);
                modDinitJWTClaim modJWTClaim = JsonConvert.DeserializeObject<modDinitJWTClaim>(jwtObj.JWTClaim);
                userID = modJWTClaim.unique_name;
                if (objApp.GetFMUserAccessCheck(modJWTClaim.unique_name, moduleName))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            
        }

    }


    public class modJWTOutput
    {
        public string JWTHeader { get; set; }
        public string JWTClaim { get; set; }
    }//eof    
    public class modDinitJWTClaim
    {
       public string unique_name {  get; set; }
    }
}