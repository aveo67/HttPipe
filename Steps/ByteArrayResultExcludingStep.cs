using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	internal class ByteArrayResultExcludingStep : IResponseStep<byte[]>
	{
		public async Task<byte[]> ProcessAsync(HttpRequestMessage request, HttpResponseMessage response, byte[] result, CancellationToken token = default)
		{
			return await response.Content.ReadAsByteArrayAsync();
		}
	}
}
