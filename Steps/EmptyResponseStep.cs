using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	public class EmptyResponseStep<TResult> : IResponseStep<TResult>
	{
		public async Task<TResult> ProcessAsync(HttpRequestMessage request, HttpResponseMessage response, TResult result, CancellationToken token = default)
		{
			return await Task.FromResult(default(TResult));
		}
	}
	public class EmptyResponseStep<TPayload, TResult> : IResponseStep<TPayload, TResult>
	{
		public async Task<TResult> ProcessAsync(TPayload payload, HttpRequestMessage request, HttpResponseMessage response, TResult result, CancellationToken token = default)
		{
			return await Task.FromResult(default(TResult));
		}
	}
	public class EmptyResponseStep<TQueryModel, TPayload, TResult> : IResponseStep<TQueryModel, TPayload, TResult>
	{
		public async Task<TResult> ProcessAsync(TQueryModel model, TPayload payload, HttpRequestMessage request, HttpResponseMessage response, TResult result, CancellationToken token = default)
		{
			return await Task.FromResult(default(TResult));
		}
	}
}
