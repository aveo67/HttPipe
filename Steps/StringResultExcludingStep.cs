using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	internal class StringResultExcludingStep : IResponseStep<String>
	{
		public async Task<string> ProcessAsync(HttpRequestMessage request, HttpResponseMessage response, string result, CancellationToken token = default)
		{
			return await response.Content.ReadAsStringAsync();
		}
	}
}
