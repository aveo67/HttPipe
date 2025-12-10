using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	public interface IHttpPipeline<TResult>
	{
		Task<TResult> SendAsync(CancellationToken token = default);
	}

	public interface IHttpPipeline<TPayload, TResult>
	{
		Task<TResult> SendAsync(TPayload payload, CancellationToken token = default);
	}

	public interface IHttpPipeline<TQueryModel, TPayload, TResult>
	{
		Task<TResult> SendAsync(TQueryModel model, TPayload payload, CancellationToken token = default);
	}
}
