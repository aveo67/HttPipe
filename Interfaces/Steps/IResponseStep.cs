using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	public interface IResponseStep<TResult> : IHttpPipelineStep
	{
		Task<TResult> ProcessAsync(HttpRequestMessage request, HttpResponseMessage response, TResult result, CancellationToken token = default);
	}

	public interface IResponseStep<TPayload, TResult> : IHttpPipelineStep
	{
		Task<TResult> ProcessAsync(TPayload payload, HttpRequestMessage request, HttpResponseMessage response, TResult result, CancellationToken token = default);
	}

	public interface IResponseStep<TQueryModel, TPayload, TResult> : IHttpPipelineStep
	{
		Task<TResult> ProcessAsync(TQueryModel model, TPayload payload, HttpRequestMessage request, HttpResponseMessage response, TResult result, CancellationToken token = default);
	}
}
