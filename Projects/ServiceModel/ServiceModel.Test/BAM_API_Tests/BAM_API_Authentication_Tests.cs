using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using ServiceModel.Models;

namespace ServiceModel.Test.BAM_API_Tests
{
    [TestClass]
    public class BAM_API_Authentication_Tests 
    {
        //https://labesteemapi.bamnuttall.co.uk/api/V3/Authorization/GetToken?UserName=SCSM_Esteem_API&Password=Sn0wDrag0n77*&LanguageCode=en
        //https://labesteemapi.bamnuttall.co.uk/api/V3/Authorization/GetToken?UserName={UserName}&Password={Password}&LanguageCode={LanguageCode}
        [TestMethod]
        public async Task Auth_GetToken_Test()
        {
            using (HttpClientHandler _handler = new HttpClientHandler())
            {
                _handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                _handler.ServerCertificateCustomValidationCallback =
                    (httpRequestMessage, cert, cetChain, policyErrors) =>
                    {
                        return true;
                    };
                using (HttpClient _client = new HttpClient(_handler))
                {
                    var _clientUri = new Uri(@"https://labesteemapi.bamnuttall.co.uk/");
                    _client.BaseAddress = _clientUri;
                    var authLogin = new AuthorizationModel()
                    {
                        UserName = "SCSM_Esteem_API",
                        Password = "Sn0wDrag0n77*",
                        LanguageCode = "ENU"
                    };
                    var content = new StringContent(JsonConvert.SerializeObject(authLogin), Encoding.UTF8, "application/json");

                    // Act
                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var tokenResult = await _client.PostAsync("api/V3/Authorization/GetToken", content);

                    authLogin.AuthToken = tokenResult.Content.ReadAsStringAsync().Result;

                    //// Assert
                    Assert.IsTrue(tokenResult.IsSuccessStatusCode, "Request status SUCCESS: " + tokenResult.StatusCode.ToString());
                    Assert.IsNotNull(authLogin.AuthToken);
                    Assert.IsFalse(authLogin.AuthToken.Contains("html"), "Failed to Authenticate");
                }
            }
        }

        //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
    }
}
