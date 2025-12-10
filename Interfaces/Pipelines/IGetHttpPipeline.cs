using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	public interface IGetHttpPipeline<TResult>
	{
		Task<TResult> GetAsync(CancellationToken token = default);
	}

	public interface IGetHttpPipeline<TQueryModel, TResult>
	{
		Task<TResult> GetAsync(TQueryModel model, CancellationToken token = default);
	}
}
