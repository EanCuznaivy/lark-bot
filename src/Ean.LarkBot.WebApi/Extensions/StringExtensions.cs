using AElf.Types;

namespace Ean.LarkBot.WebApi.Extensions;

public static class StringExtensions
{
    public static bool TryParseTransferInfo(this string formattedAddress, out Address address, out string chainType)
    {
        address = new Address();
        chainType = string.Empty;
        var list = formattedAddress.Split('_');
        if (list.Length != 3)
        {
            return false;
        }

        address = Address.FromBase58(list[1]);
        chainType = list.Last();
        return true;
    }
}