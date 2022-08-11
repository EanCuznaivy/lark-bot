using System.Text;
using System.Text.Json;
using Ean.LarkBot.QingYunKe.Modules;
using RestSharp;
using Volo.Abp.DependencyInjection;

namespace Ean.LarkBot.QingYunKe;

public class QingYunKeService : IQingYunKeService, ITransientDependency
{
    public async Task<string> ChatAsync(string message)
    {
        var restClient = new RestClient($"{QingYunKeConstants.Host}");
        var restRequest = new RestRequest();
        restRequest.AddQueryParameter("key", "free");
        restRequest.AddQueryParameter("appid", "0");
        restRequest.AddQueryParameter("msg", message);
        var response = await restClient.ExecuteAsync(restRequest);
        var responseContent = response.Content;
        var content = JsonSerializer.Deserialize<QingYunKeContent>(responseContent!);
        var text = JsonSerializer.SerializeToUtf8Bytes(new TextDto
        {
            Text = content!.Content
        });

        return Encoding.UTF8.GetString(text);
    }
}