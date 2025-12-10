using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	public interface ICompletionBuildingStageCommon<TResult, TBuilder>
	{
		TBuilder AddHeader(string name, string value);

		TBuilder RemoveHeader(string name);

		TBuilder ConfigureHttpClient(Action<HttpClient> action);

		TBuilder AttachRequestStep(IRequestStep step);

		TBuilder AttachRequestStep(Func<HttpRequestMessage, CancellationToken, Task> stepAction);

		TBuilder AttachResponseStep(IResponseStep<TResult> step);

		TBuilder AttachResponseStep(Func<TResult, HttpRequestMessage, HttpResponseMessage, CancellationToken, Task<TResult>> action);
	}

	public interface ICompletionBuildingStageCommon<TPayload, TResult, TBuilder>
		: ICompletionBuildingStageCommon<TResult, TBuilder>
	{
		TBuilder AttachRequestStep(IRequestStep<TPayload> step);

		TBuilder AttachRequestStep(Func<TPayload, HttpRequestMessage, CancellationToken, Task> stepAction);

		TBuilder AttachResponseStep(IResponseStep<TPayload, TResult> step);

		TBuilder AttachResponseStep(Func<TPayload, TResult, HttpRequestMessage, HttpResponseMessage, CancellationToken, Task<TResult>> action);
	}

	public interface ICompletionBuildingStageCommon<TQueryModel, TPayload, TResult, TBuilder>
		: ICompletionBuildingStageCommon<TPayload, TResult, TBuilder>
	{
		TBuilder AttachRequestStep(IRequestStep<TQueryModel, TPayload> step);

		TBuilder AttachRequestStep(Func<TQueryModel, TPayload, HttpRequestMessage, CancellationToken, Task> stepAction);

		TBuilder AttachResponseStep(IResponseStep<TQueryModel, TPayload, TResult> step);

		TBuilder AttachResponseStep(Func<TQueryModel, TPayload, TResult, HttpRequestMessage, HttpResponseMessage, CancellationToken, Task<TResult>> action);
	}
}
