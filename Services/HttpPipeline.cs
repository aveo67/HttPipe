using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	internal class HttpPipeline<TResult> : HttpPipelineBase<PipelineData<TResult>, TResult>, IHttpPipeline<TResult>, IGetHttpPipeline<TResult>
	{
		public HttpPipeline(
			HttpClient httpClient,
			HttpRequestFactory requestFactory,
			IRequestStep authenticationStep,
			IResponseStep<TResult> deserializationStep,
			IRequestStep[] requestSteps,
			IResponseStep<TResult>[] responseSteps) : base(httpClient, requestFactory, authenticationStep, deserializationStep, requestSteps, responseSteps)
		{
			//
		}

		public async Task<TResult> GetAsync(CancellationToken token = default)
		{
			return await SendAsync(token);
		}

		public async Task<TResult> SendAsync(CancellationToken token = default)
		{
			var data = new PipelineData<TResult>();

			return await SendAsync(data, token);
		}
	}

	internal class HttpPipeline<TPayload, TResult> : HttpPipelineBase<PipelineData<TPayload, TResult>, TPayload, TResult>, IHttpPipeline<TPayload, TResult>, IGetHttpPipeline<TPayload, TResult>, IPostHttpPipeline<TPayload, TResult>, IPutHttpPipeline<TPayload, TResult>
	{
		public HttpPipeline(
			HttpClient httpClient,
			HttpRequestFactory requestFactory,
			IRequestStep authenticationStep,
			IResponseStep<TResult> deserializationStep,
			IRequestStep[] requestSteps,
			IResponseStep<TResult>[] responseSteps,
			IRequestStep<TPayload> serializationStep,
			IRequestStep<TPayload>[] requestStepsP,
			IResponseStep<TPayload, TResult>[] responseStepsP)
			: base(httpClient, requestFactory, authenticationStep, deserializationStep, requestSteps, responseSteps, serializationStep, requestStepsP, responseStepsP)
		{
			//
		}

		public async Task<TResult> GetAsync(TPayload payload, CancellationToken token = default)
		{
			return await SendAsync(payload, token);
		}

		public async Task<TResult> PostAsync(TPayload payload, CancellationToken token = default)
		{
			return await SendAsync(payload, token);
		}

		public async Task<TResult> PutAsync(TPayload payload, CancellationToken token = default)
		{
			return await SendAsync(payload, token);
		}

		public async Task<TResult> SendAsync(TPayload payload, CancellationToken token = default)
		{
			if (payload == null)
				throw new ArgumentNullException($"The payload of type {typeof(TPayload).Name} must not be null");

			var data = new PipelineData<TPayload, TResult>()
			{
				Payload = payload,
			};

			return await SendAsync(data, token);
		}
	}

	internal class HttpPipeline<TQueryModel, TPayload, TResult> : HttpPipelineBase<PipelineData<TQueryModel, TPayload, TResult>, TQueryModel, TPayload, TResult>, IHttpPipeline<TQueryModel, TPayload, TResult>, IPostHttpPipeline<TQueryModel, TPayload, TResult>, IPutHttpPipeline<TQueryModel, TPayload, TResult>
	{
		public HttpPipeline(
			HttpClient httpClient,
			HttpRequestFactory requestFactory,
			IRequestStep authenticationStep,
			IResponseStep<TResult> deserializationStep,
			IRequestStep[] requestSteps,
			IResponseStep<TResult>[] responseSteps,
			IRequestStep<TPayload> serializationStep,
			IRequestStep<TPayload>[] requestStepsP,
			IResponseStep<TPayload, TResult>[] responseStepsP,
			IQueryConstructionStep<TQueryModel> queryConstructionStep,
			IRequestStep<TQueryModel, TPayload>[] requestStepsQP,
			IResponseStep<TQueryModel, TPayload, TResult>[] responseStepsQP)
			: base(
				  httpClient,
				  requestFactory,
				  authenticationStep,
				  deserializationStep,
				  requestSteps,
				  responseSteps,
				  serializationStep,
				  requestStepsP,
				  responseStepsP,
				  queryConstructionStep,
				  requestStepsQP,
				  responseStepsQP)
		{
			//
		}

		public async Task<TResult> PostAsync(TQueryModel model, TPayload payload, CancellationToken token = default)
		{
			return await SendAsync(model, payload, token);
		}

		public async Task<TResult> PutAsync(TQueryModel model, TPayload payload, CancellationToken token = default)
		{
			return await SendAsync(model, payload, token);
		}

		public async Task<TResult> SendAsync(TQueryModel model, TPayload payload, CancellationToken token = default)
		{
			if (model == null)
				throw new ArgumentNullException($"The query model of type {typeof(TQueryModel).Name} must not be null");

			if (payload == null)
				throw new ArgumentNullException($"The payload of type {typeof(TPayload).Name} must not be null");

			var data = new PipelineData<TQueryModel, TPayload, TResult>()
			{
				QueryModel = model,
				Payload = payload,
			};

			return await SendAsync(data, token);
		}
	}
}
