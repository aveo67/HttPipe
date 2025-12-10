using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	internal class JsonDeserializationStep<TResult> : IResponseStep<TResult>
	{
		public async Task<TResult> ProcessAsync(HttpRequestMessage request, HttpResponseMessage response, TResult result, CancellationToken token = default)
		{
			var raw = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<TResult>(raw);
		}
	}
}
