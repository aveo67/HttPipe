using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	internal class JsonSerializationStep<TPayload> : IRequestStep<TPayload>
	{
		private readonly JsonSerializerSettings _settings;

		public JsonSerializationStep(JsonSerializerSettings settings = null)
		{
			_settings = settings;
		}

		public Task ProcessAsync(TPayload payload, HttpRequestMessage request, CancellationToken token)
		{
			request.Content = JsonContent.Create(payload, _settings);
			
			return Task.CompletedTask;
		}
	}
}
