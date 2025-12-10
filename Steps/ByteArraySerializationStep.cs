using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	internal class ByteArraySerializationStep : IRequestStep<byte[]>
	{
		public Task ProcessAsync(byte[] payload, HttpRequestMessage request, CancellationToken token = default)
		{
			request.Content = new ByteArrayContent(payload);

			return Task.CompletedTask;
		}
	}
}
