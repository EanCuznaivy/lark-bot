using System.Runtime.InteropServices;
using System.Text;

namespace Ean.Tapd.Extensions;

public static class StringExtensions
{
    public static string ToBase64(this string str)
    {
        var bytes = Encoding.ASCII.GetBytes(str);
        return MemoryMarshal.TryGetArray(bytes, out ArraySegment<byte> segment)
            ? Convert.ToBase64String(segment.Array!, segment.Offset, segment.Count)
            : Convert.ToBase64String(bytes.ToArray());
    }

    public static string FromBase64(this string str)
    {
        return str == string.Empty ? string.Empty : Encoding.ASCII.GetString(Convert.FromBase64String(str));
    }
}