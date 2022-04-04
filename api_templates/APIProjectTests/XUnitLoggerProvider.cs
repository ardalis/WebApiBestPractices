﻿using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace APIProjectTests;

internal sealed class XUnitLoggerProvider : ILoggerProvider
{
	private readonly ITestOutputHelper _testOutputHelper;
	private readonly LoggerExternalScopeProvider _scopeProvider = new LoggerExternalScopeProvider();

	public XUnitLoggerProvider(ITestOutputHelper testOutputHelper)
	{
		_testOutputHelper = testOutputHelper;
	}

	public ILogger CreateLogger(string categoryName)
	{
		return new XUnitLogger(_testOutputHelper, _scopeProvider, categoryName);
	}

	public void Dispose()
	{
	}
}
