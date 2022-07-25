using System.Text;
using System.Text.Json;
using AElf.Client.Token;
using AElf.Contracts.MultiToken;
using AElf.Types;
using Ean.LarkBot.Core;
using Ean.LarkBot.Core.Models;
using Ean.LarkBot.WebApi.Extensions;

namespace Ean.LarkBot.WebApi.ContentProviders;

public class TransferContentProvider : IReplyContentProvider
{
    private readonly ITokenService _tokenService;
    public List<string> KeyWords => new() { "ELF_" };

    public TransferContentProvider(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<string> GetTextAsync(EventDto eventDto)
    {
        var content = eventDto.Event.Message.Content;
        var parameters = JsonSerializer.Deserialize<TextDto>(content)!.Text.Split(' ');
        var formattedAddress = parameters.First();
        if (!formattedAddress.TryParseTransferInfo(out var address, out var chainType)
            || !decimal.TryParse(parameters[1], out var amount))
        {
            var bytes = JsonSerializer.SerializeToUtf8Bytes(new TextDto { Text = "格式有、问题" });
            return Encoding.UTF8.GetString(bytes);
        }

        if (chainType.ToLower() != "tdvw")
        {
            var bytes = JsonSerializer.SerializeToUtf8Bytes(new TextDto { Text = $"暂不支持{chainType}链" });
            return Encoding.UTF8.GetString(bytes);
        }

        //TODO: Better check TokenInfo to get decimals.
        var transferAmount = (long)(amount * 1_00000000);

        var balance = await _tokenService.GetTokenBalanceAsync("ELF",
            Address.FromBase58("2AiXjNszZwUMdonm2RYb3GsB3aLUU3hkD1fxoazMwqPAamerLQ"));
        if (balance.Balance <= transferAmount)
        {
            var bytes = JsonSerializer.SerializeToUtf8Bytes(new TextDto
            {
                Text = $"钱不够了，只剩{(decimal)balance.Balance / 1_00000000}个ELF了"
            });
            return Encoding.UTF8.GetString(bytes);
        }

        var sendTransactionResult = await _tokenService.TransferAsync(new TransferInput
        {
            To = address,
            Amount = transferAmount,
            Symbol = "ELF"
        });

        var response = JsonSerializer.SerializeToUtf8Bytes(new TextDto
        {
            Text =
                $"已为{address.ToBase58()}转了{amount}个ELF，交易链接: https://explorer-test-side02.aelf.io/tx/{sendTransactionResult.TransactionResult.TransactionId.ToHex()}"
        });

        return Encoding.UTF8.GetString(response);
    }
}