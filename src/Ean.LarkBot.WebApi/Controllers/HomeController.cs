using System.Text;
using System.Text.Json;
using Ean.LarkBot.Core;
using Ean.LarkBot.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Ean.LarkBot.WebApi.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    private readonly IDecryptService _decryptService;
    private readonly ILarkService _larkService;
    private readonly LarkBotOptions _larkBotOptions;

    public HomeController(IDecryptService decryptService, ILarkService larkService,
        IOptionsSnapshot<LarkBotOptions> larkBotOptions)
    {
        _decryptService = decryptService;
        _larkService = larkService;
        _larkBotOptions = larkBotOptions.Value;
    }

    [HttpPost]
    public async Task<ActionResult> Home()
    {
        using var reader = new StreamReader(Request.Body, Encoding.UTF8, true, 1024, true);
        var encryptedBody = await reader.ReadToEndAsync();
        var encrypt = JsonSerializer.Deserialize<EncryptedBody>(encryptedBody)!.Encrypt;
        var body = _decryptService.Decrypt(encrypt);

        if (!Request.Headers.TryGetValue("X-Lark-Request-Timestamp", out var timestamp))
        {
            var verify = JsonSerializer.Deserialize<VerifyDto>(body);
            if (verify!.Token == _larkBotOptions.VerificationToken)
            {
                return Ok(new { challenge = verify.Challenge });
            }
        }
        else
        {
            Request.Headers.TryGetValue("X-Lark-Request-Nonce", out var nonce);
            Request.Headers.TryGetValue("X-Lark-Signature", out var signature);
            var calculatedSignature =
                _decryptService.CalculateSignature(timestamp, nonce, _larkBotOptions.EncryptKey, encryptedBody);
            if (!string.Equals(signature.ToString(), calculatedSignature, StringComparison.CurrentCultureIgnoreCase))
                return Problem();
            var eventDto = JsonSerializer.Deserialize<EventDto>(body)!;
            await _larkService.ProcessEvent(eventDto);
            return Ok();
        }

        return Problem();
    }
}