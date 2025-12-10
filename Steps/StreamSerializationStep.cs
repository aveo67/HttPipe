using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	internal class StreamSerializationStep : IRequestStep<Stream>
	{
		public Task ProcessAsync(Stream payload, HttpRequestMessage request, CancellationToken token = default)
		{
			request.Content = new StreamContent(payload);

			return Task.CompletedTask;
		}
	}
}
