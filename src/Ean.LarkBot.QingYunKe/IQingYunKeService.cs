namespace Ean.LarkBot.QingYunKe;

public interface IQingYunKeService
{
    Task<string> ChatAsync(string message);
}