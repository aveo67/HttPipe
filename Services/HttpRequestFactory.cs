using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace HttPipe
{
	internal class HttpRequestFactory
	{
		private readonly Uri _uri;

		private readonly HttpMethod _method;

		private readonly string[] _headerKeys;

		private readonly string[] _headerValues;

		public HttpRequestFactory(Uri uri, HttpMethod method, Dictionary<string, string> headers)
		{
			_uri = uri;
			_method = method;
			_headerKeys = headers.Keys.ToArray();
			_headerValues = headers.Values.ToArray();
		}

		public HttpRequestMessage CreateRequest()
		{
			var request = new HttpRequestMessage(_method, _uri);

			for (int i = 0; i < _headerKeys.Length; i++)
			{
				request.Headers.Add(_headerKeys[i], _headerValues[i]);
			}

			return request;
		}
	}
}
