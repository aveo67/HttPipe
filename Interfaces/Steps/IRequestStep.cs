using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	public interface IRequestStep : IHttpPipelineStep
	{
		Task ProcessAsync(HttpRequestMessage request, CancellationToken token = default);
	}

	public interface IRequestStep<TPayload> : IHttpPipelineStep
	{
		Task ProcessAsync(TPayload payload, HttpRequestMessage request, CancellationToken token = default);
	}

	public interface IRequestStep<TQueryModel, TPayload> : IHttpPipelineStep
	{
		Task ProcessAsync(TQueryModel model, TPayload payload, HttpRequestMessage request, CancellationToken token = default);
	}
}
