using System;
using Newtonsoft.Json.Linq;
using SimpleCrm.WebApi.Auth;

namespace SimpleCrm.WebApi.Auth
{
    public class GoogleAuthVerifier<T>
    {
        private readonly GoogleAuthSettings _googleAuthSettings;
        private readonly string _host;
        private readonly ILogger<T> _logger;

        public GoogleAuthVerifier(GoogleAuthSettings googleAuthSettings, string host, ILogger<T> logger)
        {
            _googleAuthSettings = googleAuthSettings;
            _host = host;
            _logger = logger;
        }

        public async Task<GoogleUserProfile> AcquireUser(string token)
        {
            try
            {
                var client = new HttpClient();

                var tokenRequestParameters = new Dictionary<string, string>()
            {
                { "client_id", _googleAuthSettings.ClientId },
                { "client_secret", _googleAuthSettings.ClientSecret },
                { "redirect_uri", _host + "signin-google" },
                { "code", token },
                { "grant_type", "authorization_code" }
            };

                var requestContent = new FormUrlEncodedContent(tokenRequestParameters);

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://oauth2.googleapis.com/token");
                requestMessage.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                requestMessage.Content = requestContent;

                var response = await client.SendAsync(requestMessage);
                var payloadStr = await response.Content.ReadAsStringAsync();
                var payload = JObject.Parse(payloadStr);

                if (payload["error"] != null)
                {
                    var err = payload["error"];
                    _logger.LogWarning("Google toke error response: {0}", payloadStr);
                    return new GoogleUserProfile
                    {
                        IsSuccessful = false,
                        Error = new OAuthError
                        {
                            Code = payload.Value<string>("error"),
                            Message = payload.Value<string>("error_description")
                        }
                    };
                }

                _logger.LogInformation("Payload details: {0}", payloadStr);

                var accessToken = payload.Value<string>("access_token");
                var graphMessage = new HttpRequestMessage(HttpMethod.Get, "https://www.googleapis.com/oauth2/v2/userinfo");
                graphMessage.Headers.Add("Authorization", "Bearer " + accessToken);

                var graphResponse = await client.SendAsync(graphMessage);
                var graphPayloadStr = await graphResponse.Content.ReadAsStringAsync();
                var graphPayload = JObject.Parse(graphPayloadStr);

                if (graphPayload["error"] != null)
                {
                    var err = graphPayload["error"];
                    _logger.LogWarning("Google graph error response: {0}", graphPayloadStr);
                    return new GoogleUserProfile
                    {
                        IsSuccessful = false,
                        Error = new OAuthError
                        {
                            Code = err.Value<string>("code"),
                            Message = err.Value<string>("message")
                        }
                    };
                }

                var profile = new GoogleUserProfile
                {
                    IsSuccessful = true,
                    VerifiedEmail = graphPayload.Value<bool>("verified_email"),
                    Context = graphPayload.Value<string>("@odata.context"),
                    Id = graphPayload.Value<string>("id"),
                    DisplayName = graphPayload.Value<string>("name"),
                    GivenName = graphPayload.Value<string>("given_name"),
                    Hd = graphPayload.Value<string>("hd"),
                    Mail = graphPayload.Value<string>("email"),
                    Gender = graphPayload.Value<string>("gender"),
                    Link = graphPayload.Value<string>("link"),
                    PreferredLanguage = graphPayload.Value<string>("locale"),
                    Surname = graphPayload.Value<string>("family_name"),
                    PictureUrl = graphPayload.Value<string>("picture")
                };

                return profile;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: {0} Details: {1}", ex, ex.StackTrace);
                throw;
            }
        }
    }

    public interface IOAuthUserProfile
    {
        bool IsSuccessful { get; set; }
        OAuthError Error { get; set; }
        string Id { get; set; }
        string Mail { get; set; }
        string JobTitle { get; set; }
        string DisplayName { get; set; }
        string MobilePhone { get; set; }
    }

    public class GoogleUserProfile : IOAuthUserProfile
    {
    
        public bool IsSuccessful { get; set; }
        public bool VerifiedEmail { get; set; }
        public OAuthError Error { get; set; }
        public string Context { get; set; }
        public string Id { get; set; }
        public string[] BusinessPhones { get; set; }
        public string DisplayName { get; set; }
        public string GivenName { get; set; }
        public string Hd { get; set; }
        public string Mail { get; set; }
        public string Gender { get; set; }
        public string Link { get; set; }
        public string PreferredLanguage { get; set; }
        public string Surname { get; set; }
        public string PictureUrl { get; set; }
        public string JobTitle { get; set; }
        public string MobilePhone { get; set; }
    }

    public class OAuthError
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}

