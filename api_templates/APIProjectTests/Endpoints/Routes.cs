using System;

namespace APIProjectTests;

public static class Routes
{
	private const string BaseRoute = "";

	public static class Authors
	{
		private const string BaseAuthorsRoute = Routes.BaseRoute + "/authors";

		public const string Create = BaseAuthorsRoute;

		public const string Update = BaseAuthorsRoute;

		public static string List() => BaseAuthorsRoute;

		public static string List(int perPage, int page) => $"{BaseAuthorsRoute}?perPage={perPage}&page={page}";

		public static string Get(int id) => $"{BaseAuthorsRoute}/{id}";

		public static string Delete(int id) => $"{BaseAuthorsRoute}/{id}";
	}

	public static class Account
	{
		private const string BaseAccountRoute = Routes.BaseRoute + "/account";

		public static string Register() => BaseAccountRoute + "/register";

		internal static string? Login() => BaseAccountRoute + "/login";
	}
}
