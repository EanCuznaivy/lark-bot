namespace Ean.LarkBot.Core;

public interface IDecryptService
{
    string Decrypt(string encrypt);
    string CalculateSignature(string timestamp, string nonce, string encryptKey, string body);
}