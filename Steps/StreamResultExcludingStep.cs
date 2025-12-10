using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	internal class StreamResultExcludingStep : IResponseStep<Stream>
	{
		public async Task<Stream> ProcessAsync(HttpRequestMessage request, HttpResponseMessage response, Stream result, CancellationToken token = default)
		{
			return await response.Content.ReadAsStreamAsync();
		}
	}
}
