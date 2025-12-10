using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	internal class RequestStepAction : StepActionBase<Func<HttpRequestMessage, CancellationToken, Task>>, IRequestStep
	{
		public RequestStepAction(Func<HttpRequestMessage, CancellationToken, Task> action) : base(action) { }

		public async Task ProcessAsync(HttpRequestMessage request, CancellationToken token)
		{
			await _action.Invoke(request, token);
		}
	}

	internal class RequestStepAction<TPayload> : StepActionBase<Func<TPayload, HttpRequestMessage, CancellationToken, Task>>, IRequestStep<TPayload>
	{
		public RequestStepAction(Func<TPayload, HttpRequestMessage, CancellationToken, Task> action) : base(action) { }

		public async Task ProcessAsync(TPayload payload, HttpRequestMessage request, CancellationToken token)
		{
			await _action.Invoke(payload, request, token);
		}
	}

	internal class RequestStepAction<TQueryModel, TPayload> : StepActionBase<Func<TQueryModel, TPayload, HttpRequestMessage, CancellationToken, Task>>, IRequestStep<TQueryModel, TPayload>
	{
		public RequestStepAction(Func<TQueryModel, TPayload, HttpRequestMessage, CancellationToken, Task> action) : base(action) { }

		public async Task ProcessAsync(TQueryModel model, TPayload payload, HttpRequestMessage request, CancellationToken token)
		{
			await _action.Invoke(model, payload, request, token);
		}
	}
}
