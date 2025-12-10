using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	internal class ResponseStepAction<TResult> : StepActionBase<Func<TResult, HttpRequestMessage, HttpResponseMessage, CancellationToken, Task<TResult>>>, IResponseStep<TResult>
	{
		public ResponseStepAction(Func<TResult, HttpRequestMessage, HttpResponseMessage, CancellationToken, Task<TResult>> action) : base(action) { }

		public async Task<TResult> ProcessAsync(HttpRequestMessage request, HttpResponseMessage response, TResult result, CancellationToken token)
		{
			return await _action.Invoke(result, request, response, token);
		}
	}

	internal class ResponseStepAction<TPayload, TResult> : StepActionBase<Func<TPayload, TResult, HttpRequestMessage, HttpResponseMessage, CancellationToken, Task<TResult>>>, IResponseStep<TPayload, TResult>
	{
		public ResponseStepAction(Func<TPayload, TResult, HttpRequestMessage, HttpResponseMessage, CancellationToken, Task<TResult>> action) : base(action)
		{
		}

		public async Task<TResult> ProcessAsync(TPayload payload, HttpRequestMessage request, HttpResponseMessage response, TResult result, CancellationToken token)
		{
			return await _action.Invoke(payload, result, request, response, token);
		}
	}

	internal class ResponseStepAction<TQueryModel, TPayload, TResult> : StepActionBase<Func<TQueryModel, TPayload, TResult, HttpRequestMessage, HttpResponseMessage, CancellationToken, Task<TResult>>>, IResponseStep<TQueryModel, TPayload, TResult>
	{
		public ResponseStepAction(Func<TQueryModel, TPayload, TResult, HttpRequestMessage, HttpResponseMessage, CancellationToken, Task<TResult>> action) : base(action)
		{
		}

		public async Task<TResult> ProcessAsync(TQueryModel model, TPayload payload, HttpRequestMessage request, HttpResponseMessage response, TResult result, CancellationToken token)
		{
			return await _action.Invoke(model, payload, result, request, response, token);
		}
	}
}
