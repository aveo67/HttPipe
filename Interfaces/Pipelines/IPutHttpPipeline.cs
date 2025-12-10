using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	public interface IPutHttpPipeline<TPayload, TResult>
	{
		Task<TResult> PutAsync(TPayload payload, CancellationToken token = default);
	}

	public interface IPutHttpPipeline<TQueryModel, TPayload, TResult>
	{
		Task<TResult> PutAsync(TQueryModel model, TPayload payload, CancellationToken token = default);
	}
}
