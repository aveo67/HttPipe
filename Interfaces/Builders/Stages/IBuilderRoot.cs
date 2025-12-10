using System;
using System.Net.Http;

namespace HttPipe
{
	internal interface IBuilderRoot
	{
		IBuilderRoot SetUrl(string uri);

		IBuilderRoot SetUrl(Uri uri);

		IBuilderRoot SetMethod(HttpMethod method);

		IBuilderRoot SetTimeout(TimeSpan timeout);
	}
}
