using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	internal abstract class HttpPipelineBase<TData> : IDisposable
	{
		protected readonly HttpClient _httpClient;

		protected readonly HttpRequestFactory _requestFactory;

		protected readonly IRequestStep _authenticationStep;



		public HttpPipelineBase(
			HttpClient httpClient,
			HttpRequestFactory requestFactory,
			IRequestStep authenticationStep)
		{
			_httpClient = httpClient;
			_requestFactory = requestFactory;
			_authenticationStep = authenticationStep;
		}

		public void Dispose()
		{
			_httpClient.Dispose();
		}

		protected abstract Task PrepareRequestAsync(TData data, HttpRequestMessage request, CancellationToken token = default);

		protected abstract Task<TData> ProcessResponseAsync(TData data, HttpRequestMessage request, HttpResponseMessage response, CancellationToken token = default);
	}

	internal abstract class HttpPipelineBase<TData, TResult> : HttpPipelineBase<TData>
			where TData : PipelineData<TResult>
	{
		private readonly IRequestStep[] _requestSteps;

		private readonly IResponseStep<TResult>[] _responseSteps;

		private readonly IResponseStep<TResult> _deserializationStep;

		public HttpPipelineBase(
			HttpClient httpClient,
			HttpRequestFactory requestFactory,
			IRequestStep authenticationStep,
			IResponseStep<TResult> deserializationStep,
			IRequestStep[] requestSteps,
			IResponseStep<TResult>[] responseSteps)
			: base(httpClient, requestFactory, authenticationStep)
		{
			_deserializationStep = deserializationStep;
			_requestSteps = requestSteps;
			_responseSteps = responseSteps;
		}

		protected async Task<TResult> SendAsync(TData data, CancellationToken token = default)
		{
			HttpRequestMessage request = null;

			HttpResponseMessage response = null;

			try
			{
				request = _requestFactory.CreateRequest();

				await _authenticationStep.ProcessAsync(request, token);

				await PrepareRequestAsync(data, request, token);

				response = await _httpClient.SendAsync(request, token);

				var result = await ProcessResponseAsync(data, request, response, token);

				return result.Result;
			}

			catch (Exception ex)
			{
				throw new HttpPipelineException($"Network error! Uri: {request?.RequestUri.ToString() ?? "UNKNOWN Uri"}\n", ex);
			}

			finally
			{
				request?.Dispose();

				response?.Dispose();
			}
		}

		protected override async Task PrepareRequestAsync(TData data, HttpRequestMessage request, CancellationToken token = default)
		{
			for (int i = 0; i < _requestSteps.Length; i++)
			{
				var step = _requestSteps[i];

				await step.ProcessAsync(request, token);
			}
		}

		protected override async Task<TData> ProcessResponseAsync(TData data, HttpRequestMessage request, HttpResponseMessage response, CancellationToken token = default)
		{
			data.Result = default;

			data.Result = await _deserializationStep.ProcessAsync(request, response, data.Result, token);

			for (int i = 0; i < _responseSteps.Length; ++i)
			{
				var step = _responseSteps[i];

				data.Result = await step.ProcessAsync(request, response, data.Result, token);
			}

			return data;
		}
	}

	internal abstract class HttpPipelineBase<TData, TPayload, TResult> : HttpPipelineBase<TData, TResult>
			where TData : PipelineData<TPayload, TResult>
	{
		private readonly IRequestStep<TPayload>[] _requestSteps;

		private readonly IResponseStep<TPayload, TResult>[] _responseSteps;

		private readonly IRequestStep<TPayload> _serializationStep;

		public HttpPipelineBase(
			HttpClient httpClient,
			HttpRequestFactory requestFactory,
			IRequestStep authenticationStep,
			IResponseStep<TResult> deserializationStep,
			IRequestStep[] requestSteps,
			IResponseStep<TResult>[] responseSteps,
			IRequestStep<TPayload> serializationStep,
			IRequestStep<TPayload>[] requestStepsP,
			IResponseStep<TPayload, TResult>[] responseStepsP) : base(httpClient, requestFactory, authenticationStep, deserializationStep, requestSteps, responseSteps)
		{
			_serializationStep = serializationStep;
			_requestSteps = requestStepsP;
			_responseSteps = responseStepsP;
		}

		protected override async Task PrepareRequestAsync(TData data, HttpRequestMessage request, CancellationToken token = default)
		{
			await _serializationStep.ProcessAsync(data.Payload, request, token);

			await base.PrepareRequestAsync(data, request, token);

			for (int i = 0; i < _requestSteps.Length; i++)
			{
				var step = _requestSteps[i];

				await step.ProcessAsync(data.Payload, request, token);
			}
		}

		protected override async Task<TData> ProcessResponseAsync(TData data, HttpRequestMessage request, HttpResponseMessage response, CancellationToken token = default)
		{
			data = await base.ProcessResponseAsync(data, request, response, token);

			for (int i = 0; i < _responseSteps.Length; ++i)
			{
				var step = _responseSteps[i];

				data.Result = await step.ProcessAsync(data.Payload, request, response, data.Result, token);
			}

			return data;
		}
	}

	internal abstract class HttpPipelineBase<TData, TQueryModel, TPayload, TResult> : HttpPipelineBase<TData, TPayload, TResult>
			where TData : PipelineData<TQueryModel, TPayload, TResult>
	{
		private readonly IRequestStep<TQueryModel, TPayload>[] _requestSteps;

		private readonly IResponseStep<TQueryModel, TPayload, TResult>[] _responseSteps;

		private readonly IQueryConstructionStep<TQueryModel> _queryConstructionStep;

		protected HttpPipelineBase(
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
			: base(httpClient, requestFactory, authenticationStep, deserializationStep, requestSteps, responseSteps, serializationStep, requestStepsP, responseStepsP)
		{
			_queryConstructionStep = queryConstructionStep;
			_requestSteps = requestStepsQP;
			_responseSteps = responseStepsQP;
		}

		protected override async Task PrepareRequestAsync(TData data, HttpRequestMessage request, CancellationToken token = default)
		{
			await _queryConstructionStep.ProcessAsync(data.QueryModel, request, token);

			await base.PrepareRequestAsync(data, request, token);

			for (int i = 0; i < _requestSteps.Length; i++)
			{
				var step = _requestSteps[i];

				await step.ProcessAsync(data.QueryModel, data.Payload, request, token);
			}
		}

		protected override async Task<TData> ProcessResponseAsync(TData data, HttpRequestMessage request, HttpResponseMessage response, CancellationToken token = default)
		{
			data = await base.ProcessResponseAsync(data, request, response, token);

			for (int i = 0; i < _responseSteps.Length; ++i)
			{
				var step = _responseSteps[i];

				data.Result = await step.ProcessAsync(data.QueryModel, data.Payload, request, response, data.Result, token);
			}

			return data;
		}
	}
}
