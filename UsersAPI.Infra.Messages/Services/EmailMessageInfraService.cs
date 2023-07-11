using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using UsersAPI.Infra.Messages.Models;
using UsersAPI.Infra.Messages.Settings;

namespace UsersAPI.Infra.Messages.Services
{
    public class EmailMessageInfraService
    {
        private readonly EmailMessageSettings? _emailMessageSettings;

        public EmailMessageInfraService(IOptions<EmailMessageSettings> emailMessageSettings)
        {
            _emailMessageSettings = emailMessageSettings.Value;
        }

        public async Task SendMessageAsync(MessageRequestModel messagesRequestModel)
        {
            using (var httpClient = new HttpClient())
            {

                var authResponseModel = await AuthenticateAsync();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponseModel.Token);

                var messagesRequestContent = new StringContent(JsonConvert.SerializeObject(messagesRequestModel),
                    Encoding.UTF8, "application/json");

                await httpClient.PostAsync($"{_emailMessageSettings?.BaseUrl}/messages", messagesRequestContent);
            }
        }

        private async Task<AuthResponseModel> AuthenticateAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var authRequestModel = new AuthRequestModel
                {
                    Key = _emailMessageSettings?.User,
                    Pass = _emailMessageSettings?.Password
                };

                var authRequestContent = new StringContent(JsonConvert.SerializeObject(authRequestModel),
                    Encoding.UTF8, "application/json");

                var authResponse = await httpClient
                    .PostAsync($"{_emailMessageSettings?.BaseUrl}/auth", authRequestContent);

                var authResponseModel = ReadResponse<AuthResponseModel>(authResponse);

                return authResponseModel;
            }
        }

        private T ReadResponse<T>(HttpResponseMessage response)
        {
            var builder = new StringBuilder();

            using (var item = response.Content)
            {
                var task = item.ReadAsStringAsync();
                builder.Append(task.Result);
            }

            return JsonConvert.DeserializeObject<T>(builder.ToString());
        }
    }
}
