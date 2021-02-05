using Newtonsoft.Json;
using ProyectoBaseXF.Infrastructure.ApiModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ProyectoBaseXF.Infrastructure.Services
{
    public class ApiServiceBase
    {
        protected HttpClient client { get; set; }

        public ApiServiceBase()
        {
            client = new HttpClient() { MaxResponseContentBufferSize = 2560000 };
        }

        public async Task HandleUnauthorized()
        {
            //await UserService.CerrarSession();
            throw new UnauthorizedAccessException("Su sesión es inválida o ha expirado");
        }

        public async Task<TokenRequest> GetToken(string username, string password)
        {
            string endpoint = "Token";

            var dict = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "Username", username },
                { "Password", password }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, endpoint) { Content = new FormUrlEncodedContent(dict) };
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var parsedResponse = JsonConvert.DeserializeObject<TokenRequest>(json);
                return parsedResponse;
            }
            else
            {
                throw (new Exception("Ocurrio un error al realizar la consulta. Verifique las credenciales"));
            }
        }

        public async Task<T> PostAsync<T>(T data, string endpoint)
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                if (!string.IsNullOrEmpty(oauthToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oauthToken);
                }

                var uri = new Uri(endpoint);

                var body = JsonConvert.SerializeObject(data);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage
                {
                    RequestUri = uri,
                    Content = content,
                    Method = HttpMethod.Post,
                    Headers =
                    {
                        {"X-Version", $"{VersionTracking.CurrentVersion} ({VersionTracking.CurrentBuild})" },
                    }
                };
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var parsedResponse = JsonConvert.DeserializeObject<T>(json);
                    return parsedResponse;
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        await HandleUnauthorized();
                        return default;
                    }
                    else
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var parserResponse = JsonConvert.DeserializeObject<BadRequest>(json);
                        throw new Exception(parserResponse.Message);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Error al realizar la consulta: \n\n{0}", e.Message), e); ;
            }
        }


        public async Task<U> PostAsync<T, U>(T data, string endpoint)
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                if (!string.IsNullOrEmpty(oauthToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oauthToken);
                }

                var uri = new Uri(endpoint);


                var body = JsonConvert.SerializeObject(data);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage
                {
                    RequestUri = uri,
                    Content = content,
                    Method = HttpMethod.Post,
                    Headers =
                    {
                        {"X-Version", $"{VersionTracking.CurrentVersion} ({VersionTracking.CurrentBuild})" },
                    }
                };
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var parsedResponse = JsonConvert.DeserializeObject<U>(json);
                    return parsedResponse;
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        await HandleUnauthorized();
                        return default;
                    }
                    else
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var parserResponse = JsonConvert.DeserializeObject<BadRequest>(json);
                        throw new Exception(parserResponse.Message);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Error al realizar la consulta: \n\n{0}", e.Message), e); ;
            }
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                if (!string.IsNullOrEmpty(oauthToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oauthToken);
                }

                var uri = new Uri(endpoint);

                var request = new HttpRequestMessage(HttpMethod.Get, uri);
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var parserResponse = JsonConvert.DeserializeObject<T>(json);
                    return parserResponse;
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        await HandleUnauthorized();
                        return default;
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var parserResponse = JsonConvert.DeserializeObject<BadRequest>(json);
                        throw new Exception(parserResponse.Message);
                    }
                    else
                    {
                        throw new Exception($"No es posible conectarse al DMS, verifique su conexión y estado de RED. \n\nRazon: {response.StatusCode}");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Error al realizar la consulta: \n\n{0}", e.Message), e); ;
            }
        }

    }
}
