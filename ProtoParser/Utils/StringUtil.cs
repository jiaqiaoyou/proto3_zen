using System.Text;

public static class StringUtil
{
    public static string ReplicateString(string org, int replicateCount)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < replicateCount; ++i)
        {
            sb.Append(org);
        }
        return sb.ToString();
    }
}
