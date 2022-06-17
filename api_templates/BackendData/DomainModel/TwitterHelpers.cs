using System.Text.RegularExpressions;
using Ardalis.Result;

namespace BackendData;

public static class TwitterHelpers
{
	public static Result<string> ToAtPrefixFormat(string twitterAlias)
	{
		if (Regex.IsMatch(twitterAlias, "^@(\\w){ 1,15}$")) return twitterAlias;
		if (Regex.IsMatch(twitterAlias, "^(\\w){ 1,15}$")) return "@" + twitterAlias;
		if (twitterAlias.StartsWith("http://twitter.com") ||
			twitterAlias.StartsWith("https://twitter.com"))
		{
			// grab the last part of the URL
			string lastPart = twitterAlias.Split('/').Last();
			return "@" + lastPart;
		}
		return Result.Error("Input was not in a recognized format.");
	}
}
