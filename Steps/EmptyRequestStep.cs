using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	public class EmptyRequestStep : IRequestStep
	{
		public Task ProcessAsync(HttpRequestMessage target, CancellationToken token = default)
		{
			return Task.CompletedTask;
		}
	}

	public class EmptyRequestStep<TPayload> : IRequestStep<TPayload>
	{
		public Task ProcessAsync(TPayload payload, HttpRequestMessage target, CancellationToken token = default)
		{
			return Task.CompletedTask;
		}
	}

	public class EmptyRequestStep<TQueryModel, TPayload> : IRequestStep<TQueryModel,TPayload>
	{
		public Task ProcessAsync(TQueryModel model, TPayload payload, HttpRequestMessage target, CancellationToken token = default)
		{
			return Task.CompletedTask;
		}
	}
}
