using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	public interface IPostHttpPipeline<TPayload, TResult>
	{
		Task<TResult> PostAsync(TPayload payload, CancellationToken token = default);
	}

	public interface IPostHttpPipeline<TQueryModel, TPayload, TResult>
	{
		Task<TResult> PostAsync(TQueryModel model, TPayload payload, CancellationToken token = default);
	}
}
