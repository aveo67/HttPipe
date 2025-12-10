using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	internal class StringSerializationStep : IRequestStep<String>
	{
		public Task ProcessAsync(string payload, HttpRequestMessage request, CancellationToken token = default)
		{
			request.Content = new StringContent(payload, Encoding.UTF8);

			return Task.CompletedTask;
		}
	}
}
