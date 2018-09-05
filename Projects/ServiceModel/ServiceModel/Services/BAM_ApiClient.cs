using Newtonsoft.Json;
using ServiceModel.Connection;
using ServiceModel.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServiceModel.Services
{
    public class BAM_ApiClient
    {
        public HttpClientHandler _handler;
        public HttpClient _client;
        public Uri _clientUri;
        public AuthorizationModel _authLogin;

        public async Task Setup()
        {
            if (_client?.DefaultRequestHeaders?.Authorization != null)
            {
                var authToken = _client?.DefaultRequestHeaders?.Authorization;
                if (string.IsNullOrEmpty(authToken.Parameter))
                    return;
            }
            _handler = new HttpClientHandler();
            _handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            _handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            _client = new HttpClient(_handler);

            var connection = new ApiConnection();
            var model = connection.CreateConnection();
            //_clientUri = new Uri(@"https://labesteemapi.bamnuttall.co.uk/");
            _clientUri = new Uri(model.Server);
            _client.BaseAddress = _clientUri;

            _authLogin = model.AuthorizationModel;
            //_authLogin = new AuthorizationModel()
            //{
            //    UserName = "SCSM_Esteem_API",
            //    Password = "Sn0wDrag0n77*",
            //    LanguageCode = "ENU"
            //};
            var content = new StringContent(JsonConvert.SerializeObject(_authLogin), Encoding.UTF8, "application/json");

            // Act
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var tokenResult = await _client.PostAsync("api/V3/Authorization/GetToken", content);
            var token = tokenResult.Content.ReadAsStringAsync().Result;
            Regex regex = new Regex("\"(.*)\"");
            Match match = regex.Match(token);
            _authLogin.AuthToken = match.Groups[1].Value; // regex.Replace(token, "");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _authLogin.AuthToken);
        }

    }
}
