using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace Ean.LarkBot.Core;

public class DecryptService : IDecryptService, ITransientDependency
{
    private const int BlockSize = 16;
    private readonly byte[] _encryptKey;

    public DecryptService(IOptionsSnapshot<LarkBotOptions> larkBotOptions)
    {
        _encryptKey = Sha256Hash(larkBotOptions.Value.EncryptKey);
    }

    public string Decrypt(string encrypt)
    {
        var encBytes = Convert.FromBase64String(encrypt);
        var rijndaelManaged = Aes.Create();
        rijndaelManaged.Key = _encryptKey;
        rijndaelManaged.Mode = CipherMode.CBC;
        rijndaelManaged.IV = encBytes.Take(BlockSize).ToArray();
        var transform = rijndaelManaged.CreateDecryptor();
        var blockBytes = transform.TransformFinalBlock(encBytes, BlockSize, encBytes.Length - BlockSize);
        return Encoding.UTF8.GetString(blockBytes);
    }

    public string CalculateSignature(string timestamp, string nonce, string encryptKey, string body)
    {
        var content = new StringBuilder();
        content.Append(timestamp);
        content.Append(nonce);
        content.Append(encryptKey);
        content.Append(body);
        var sha256 = SHA256.Create();
        var result = BitConverter.ToString(sha256.ComputeHash(Encoding.Default.GetBytes(content.ToString())));
        result = result.Replace("-", "");
        return result;
    }

    private static byte[] Sha256Hash(string str)
    {
        var bytes = Encoding.UTF8.GetBytes(str);
        var sha256 = SHA256.Create();
        return sha256.ComputeHash(bytes);
    }
}