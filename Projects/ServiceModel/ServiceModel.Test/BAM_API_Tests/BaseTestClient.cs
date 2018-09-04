using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ServiceModel.Models;
using ServiceModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServiceModel.Test.BAM_API_Tests
{
    [TestClass]
    public class BaseTestClient
    {
        public HttpClientHandler _handler;
        public HttpClient _client;
        public Uri _clientUri;
        public AuthorizationModel _authLogin;
        public BAM_ApiClient _bamClient;

        [TestInitialize]
        public async Task Setup()
        {
            _bamClient = new BAM_ApiClient();
            Task.Run(() => _bamClient.Setup()).Wait();
        }

        //[TestInitialize]
        //public async Task TestSetup()
        //{
        //    _handler = new HttpClientHandler
        //    {
        //        ClientCertificateOptions = ClientCertificateOption.Manual,
        //        ServerCertificateCustomValidationCallback =
        //        (httpRequestMessage, cert, cetChain, policyErrors) =>
        //        {
        //            return true;
        //        }
        //    };

        //    _client = new HttpClient(_handler);
        //    _clientUri = new Uri(@"https://lab.esteemapi.bamnuttall.co.uk/api/V3/");
        //    _client.BaseAddress = _clientUri;

        //    _authLogin = new AuthorizationModel()
        //    {
        //        UserName = "SCSM_Esteem_API",
        //        Password = "Sn0wDrag0n77*",
        //        LanguageCode = "ENU"
        //    };
        //    var content = new StringContent(JsonConvert.SerializeObject(_authLogin), Encoding.UTF8, "application/json");

        //    // Act
        //    _client.DefaultRequestHeaders.Accept.Clear();
        //    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    var tokenResult = await _client.PostAsync("Authorization/GetToken", content);
        //    var token = tokenResult.Content.ReadAsStringAsync().Result;
        //    Regex regex = new Regex("\"(.*)\"");
        //    Match match = regex.Match(token);
        //    _authLogin.AuthToken = match.Groups[1].Value; // regex.Replace(token, "");

        //    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _authLogin.AuthToken);
        //}

        [TestCleanup]
        public void TestCleanup()
        {
            //_handler.Dispose();
            //_client.Dispose();
            //_handler = null;
            //_client = null;
            _bamClient._client.Dispose();
            _bamClient = null;
        }
    }
}
