using Ean.Tapd.Extensions;

namespace Ean.LarkBot.Core.Tests;

public class BasicTests
{
    [Fact]
    public async Task Base64Test()
    {
        var foo = "AElf".ToBase64();
        var bar = foo.FromBase64();
        Assert.Equal("AElf", bar);
    }
}