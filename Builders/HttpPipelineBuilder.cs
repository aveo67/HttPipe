using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	internal abstract class HttpPipelineBuilder<TResult, TAfterAuthenticationBuildingStage, TAfterDeserializationBuildingStage>
		: IAuthenticationBuildingStage<TAfterAuthenticationBuildingStage>, IDeserializationBuildingStage<TResult, TAfterDeserializationBuildingStage>, ICompletionBuildingStageCommon<TResult, TAfterDeserializationBuildingStage>, IBuilderRoot, ICustomHttpPipelineBuilderCommon<TAfterAuthenticationBuildingStage>
	{
		private readonly Dictionary<string, string> _headers = new Dictionary<string, string>();

		private HttpMethod _method;

		private TimeSpan _timeout;

		private Uri _uri;

		private Action<HttpClient> _clientSetupAction;

		private IRequestStep _authenticationStep;

		private IResponseStep<TResult> _deserializationStep;

		private bool _isCorrect = false;

		private readonly LinkedList<IRequestStep> _requestSteps = new LinkedList<IRequestStep>();

		private readonly LinkedList<IResponseStep<TResult>> _responseSteps = new LinkedList<IResponseStep<TResult>>();

		protected abstract TAfterAuthenticationBuildingStage AfterAuthenticationBuildingStage { get; }

		protected abstract TAfterDeserializationBuildingStage AfterDeserializationBuildingStage { get; }



		protected void AssertStepNotNull(IHttpPipelineStep step)
		{
			if (step == null)
				throw new ArgumentNullException("Step must not be null");
		}

		protected void AssertBuildingDone()
		{
			if (!_isCorrect)
				throw new HttpPipelineBuilderException("Building was not completed");
		}

		protected T AttachStepTo<TStep, T>(TStep step, LinkedList<TStep> destination, T result)
			where TStep : IHttpPipelineStep
		{
			AssertStepNotNull(step);
			destination.AddLast(step);

			return result;
		}

		protected T SetStepTo<TStep, T>(TStep step, ref TStep destination, T result)
			where TStep : IHttpPipelineStep
		{
			AssertStepNotNull(step);
			destination = step;

			return result;
		}

		public IBuilderRoot SetMethod(HttpMethod method)
		{
			_method = method ?? throw new ArgumentNullException("Method must not be null");

			return this;
		}

		public IBuilderRoot SetTimeout(TimeSpan timeout)
		{
			_timeout = timeout;

			return this;
		}

		public IBuilderRoot SetUrl(string uri)
		{
			if (String.IsNullOrEmpty(uri))
				throw new ArgumentNullException("The uri is incorrect");

			_uri = new Uri(uri);

			return this;
		}

		public IBuilderRoot SetUrl(Uri uri)
		{
			_uri = uri ?? throw new ArgumentNullException("The uri is incorrect");

			return this;
		}

		public TAfterDeserializationBuildingStage ConfigureHttpClient(Action<HttpClient> action)
		{
			_clientSetupAction = action ?? throw new ArgumentNullException("Http client action is null");

			return AfterDeserializationBuildingStage;
		}

		public TAfterDeserializationBuildingStage AddHeader(string name, string value)
		{
			if (String.IsNullOrEmpty(name))
				throw new ArgumentNullException("The name is incorrect");

			_headers.Add(name, value);

			return AfterDeserializationBuildingStage;
		}

		public TAfterDeserializationBuildingStage RemoveHeader(string name)
		{
			_headers.Remove(name);

			return AfterDeserializationBuildingStage;
		}

		public TAfterDeserializationBuildingStage AttachRequestStep(IRequestStep step)
			=> AttachStepTo(step, _requestSteps, AfterDeserializationBuildingStage);

		public TAfterDeserializationBuildingStage AttachRequestStep(Func<HttpRequestMessage, CancellationToken, Task> stepAction)
			 => AttachRequestStep(new RequestStepAction(stepAction));

		public TAfterDeserializationBuildingStage AttachResponseStep(IResponseStep<TResult> step)
			=> AttachStepTo(step, _responseSteps, AfterDeserializationBuildingStage);

		public TAfterDeserializationBuildingStage AttachResponseStep(Func<TResult, HttpRequestMessage, HttpResponseMessage, CancellationToken, Task<TResult>> action)
			=> AttachResponseStep(new ResponseStepAction<TResult>(action));

		public TAfterAuthenticationBuildingStage WithAuthenticationStep(IRequestStep authenticationStep)
			=> SetStepTo(authenticationStep, ref _authenticationStep, AfterAuthenticationBuildingStage);

		public TAfterAuthenticationBuildingStage WithAuthenticationStep(Func<HttpRequestMessage, CancellationToken, Task> authenticationAction)
			=> WithAuthenticationStep(new RequestStepAction(authenticationAction));

		public TAfterDeserializationBuildingStage WithDeserializationStep(IResponseStep<TResult> deserializationStep)
		{
			_isCorrect = true;

			return SetStepTo(deserializationStep, ref _deserializationStep, AfterDeserializationBuildingStage);
		}
			

		public TAfterDeserializationBuildingStage WithDeserializationStep(Func<TResult, HttpRequestMessage, HttpResponseMessage, CancellationToken, Task<TResult>> deserializationAction)
			=> WithDeserializationStep(new ResponseStepAction<TResult>(deserializationAction));

		public TAfterAuthenticationBuildingStage WithoutAuthentication()
			=> SetStepTo(new EmptyRequestStep(), ref _authenticationStep, AfterAuthenticationBuildingStage);

		public TAfterDeserializationBuildingStage WithoutDeserialization()
			=> SetStepTo(new EmptyResponseStep<TResult>(), ref _deserializationStep, AfterDeserializationBuildingStage);

		public IAuthenticationBuildingStage<TAfterAuthenticationBuildingStage> Configure(Uri uri, HttpMethod method, TimeSpan timeout = default)
		{
			SetUrl(uri)
			.SetMethod(method)
			.SetTimeout(timeout);

			return this;
		}

		public IAuthenticationBuildingStage<TAfterAuthenticationBuildingStage> Configure(string uri, HttpMethod method, TimeSpan timeout = default)
		{
			SetUrl(uri)
			.SetMethod(method)
			.SetTimeout(timeout);

			return this;
		}

		protected HttpRequestFactory CreateHttpRequestFactory()
			=> new HttpRequestFactory(_uri, _method, _headers);

		protected HttpClient CreateHttpClient()
		{
			var client = new HttpClient();

			if (_timeout != default)
				client.Timeout = _timeout;

			_clientSetupAction?.Invoke(client);

			return client;
		}

		protected IRequestStep GetRequestAuthenticationStep()
		{
			if (_authenticationStep == null)
				return new EmptyRequestStep();

			return _authenticationStep;
		}

		protected IResponseStep<TResult> GetDeserializationStep()
			=> _deserializationStep;

		protected IRequestStep[] GetRequestStepsFirstRank()
			=> _requestSteps.ToArray();

		protected IResponseStep<TResult>[] GetResponseStepsFirstRank()
			=> _responseSteps.ToArray();

		protected HttpPipeline<TResult> CreatePipeline()
		{
			AssertBuildingDone();

			return new HttpPipeline<TResult>(
				CreateHttpClient(),
				CreateHttpRequestFactory(),
				GetRequestAuthenticationStep(),
				GetDeserializationStep(),
				GetRequestStepsFirstRank(),
				GetResponseStepsFirstRank());
		}
	}

	internal abstract class HttpPipelineBuilder<TPayload, TResult, TAfterAuthenticationBuildingStage, TAfterDeserializationBuildingStage> : HttpPipelineBuilder<TResult, TAfterAuthenticationBuildingStage, TAfterDeserializationBuildingStage>, ICompletionBuildingStageCommon<TPayload, TResult, TAfterDeserializationBuildingStage>, ISerializationBuildingStageCommon<TPayload, TResult, TAfterDeserializationBuildingStage>, IQueryBuildingStage<TPayload, IDeserializationBuildingStage<TResult, TAfterDeserializationBuildingStage>>
	{
		private readonly LinkedList<IRequestStep<TPayload>> _requestSteps = new LinkedList<IRequestStep<TPayload>>();

		private readonly LinkedList<IResponseStep<TPayload, TResult>> _responseSteps = new LinkedList<IResponseStep<TPayload, TResult>>();

		private IRequestStep<TPayload> _serializationStep;

		public TAfterDeserializationBuildingStage AttachRequestStep(IRequestStep<TPayload> step)
			=> AttachStepTo(step, _requestSteps, AfterDeserializationBuildingStage);

		public TAfterDeserializationBuildingStage AttachRequestStep(Func<TPayload, HttpRequestMessage, CancellationToken, Task> stepAction)
			=> AttachRequestStep(new RequestStepAction<TPayload>(stepAction));

		public TAfterDeserializationBuildingStage AttachResponseStep(IResponseStep<TPayload, TResult> step)
			=> AttachStepTo(step, _responseSteps, AfterDeserializationBuildingStage);

		public TAfterDeserializationBuildingStage AttachResponseStep(Func<TPayload, TResult, HttpRequestMessage, HttpResponseMessage, CancellationToken, Task<TResult>> action)
			=> AttachResponseStep(new ResponseStepAction<TPayload, TResult>(action));

		public IDeserializationBuildingStage<TResult, TAfterDeserializationBuildingStage> WithoutQuery()
			=> WithQueryConstructionStep(new EmptyQueryConstructionStep<TPayload>());

		public IDeserializationBuildingStage<TResult, TAfterDeserializationBuildingStage> WithoutSerialization()
			=> WithSerializationStep(new EmptyRequestStep<TPayload>());

		public IDeserializationBuildingStage<TResult, TAfterDeserializationBuildingStage> WithQueryConstructionStep(IQueryConstructionStep<TPayload> queryConstructionStep)
			=> WithSerializationStep(queryConstructionStep);

		public IDeserializationBuildingStage<TResult, TAfterDeserializationBuildingStage> WithQueryConstructionStep(Func<TPayload, HttpRequestMessage, CancellationToken, Task> queryConstructionAction)
			=> WithSerializationStep(queryConstructionAction);

		public IDeserializationBuildingStage<TResult, TAfterDeserializationBuildingStage> WithSerializationStep(IRequestStep<TPayload> serializationStep)
			=> SetStepTo(serializationStep, ref _serializationStep, this);

		public IDeserializationBuildingStage<TResult, TAfterDeserializationBuildingStage> WithSerializationStep(Func<TPayload, HttpRequestMessage, CancellationToken, Task> serializationAction)
			=> WithSerializationStep(new RequestStepAction<TPayload>(serializationAction));

		protected IRequestStep<TPayload> GetSerializationStep()
		{
			if (_serializationStep == null)
				return new EmptyRequestStep<TPayload>();

			return _serializationStep;
		}

		protected IRequestStep<TPayload>[] GetRequestStepsSecondRank()
			=> _requestSteps.ToArray();

		protected IResponseStep<TPayload, TResult>[] GetResponseStepsSecondRank()
			=> _responseSteps.ToArray();

		protected new HttpPipeline<TPayload, TResult> CreatePipeline()
		{
			AssertBuildingDone();

			return new HttpPipeline<TPayload, TResult>(
				CreateHttpClient(),
				CreateHttpRequestFactory(),
				GetRequestAuthenticationStep(),
				GetDeserializationStep(),
				GetRequestStepsFirstRank(),
				GetResponseStepsFirstRank(),
				GetSerializationStep(),
				GetRequestStepsSecondRank(),
				GetResponseStepsSecondRank());
		}
	}

	internal abstract class HttpPipelineBuilder<TQueryModel, TPayload, TResult, TAfterAuthenticationBuildingStage, TAfterDeserializationBuildingStage, TAfterQueryBuildingStage> : HttpPipelineBuilder<TPayload, TResult, TAfterAuthenticationBuildingStage, TAfterDeserializationBuildingStage>, ICompletionBuildingStageCommon<TQueryModel, TPayload, TResult, TAfterDeserializationBuildingStage>, IQueryBuildingStage<TQueryModel, TAfterQueryBuildingStage>
	{
		private readonly LinkedList<IRequestStep<TQueryModel, TPayload>> _requestSteps = new LinkedList<IRequestStep<TQueryModel, TPayload>>();

		private readonly LinkedList<IResponseStep<TQueryModel, TPayload, TResult>> _responseSteps = new LinkedList<IResponseStep<TQueryModel, TPayload, TResult>>();

		private IQueryConstructionStep<TQueryModel> _queryConstructionStep;

		protected abstract TAfterQueryBuildingStage AfterQueryBuildingStage { get; }

		public TAfterDeserializationBuildingStage AttachRequestStep(IRequestStep<TQueryModel, TPayload> step)
			=> AttachStepTo(step, _requestSteps, AfterDeserializationBuildingStage);

		public TAfterDeserializationBuildingStage AttachRequestStep(Func<TQueryModel, TPayload, HttpRequestMessage, CancellationToken, Task> stepAction)
			=> AttachRequestStep(new RequestStepAction<TQueryModel, TPayload>(stepAction));

		public TAfterDeserializationBuildingStage AttachResponseStep(IResponseStep<TQueryModel, TPayload, TResult> step)
			=> AttachStepTo(step, _responseSteps, AfterDeserializationBuildingStage);

		public TAfterDeserializationBuildingStage AttachResponseStep(Func<TQueryModel, TPayload, TResult, HttpRequestMessage, HttpResponseMessage, CancellationToken, Task<TResult>> action)
			=> AttachResponseStep(new ResponseStepAction<TQueryModel, TPayload, TResult>(action));

		public new TAfterQueryBuildingStage WithoutQuery()
			=> WithQueryConstructionStep(new EmptyQueryConstructionStep<TQueryModel>());

		public TAfterQueryBuildingStage WithQueryConstructionStep(IQueryConstructionStep<TQueryModel> queryConstructionStep)
			=> SetStepTo(queryConstructionStep, ref _queryConstructionStep, AfterQueryBuildingStage);

		public TAfterQueryBuildingStage WithQueryConstructionStep(Func<TQueryModel, HttpRequestMessage, CancellationToken, Task> queryConstructionAction)
			=> WithQueryConstructionStep(new QueryConstructionStepAction<TQueryModel>(queryConstructionAction));

		protected IQueryConstructionStep<TQueryModel> GetQueryConstructionStep()
		{
			if (_queryConstructionStep == null)
				return new EmptyQueryConstructionStep<TQueryModel>();

			return _queryConstructionStep;
		}

		protected IRequestStep<TQueryModel, TPayload>[] GetRequestStepsThirdRank()
			=> _requestSteps.ToArray();

		protected IResponseStep<TQueryModel, TPayload, TResult>[] GetResponseStepsThirdRank()
			=> _responseSteps.ToArray();

		protected new HttpPipeline<TQueryModel, TPayload, TResult> CreatePipeline()
		{
			AssertBuildingDone();

			return new HttpPipeline<TQueryModel, TPayload, TResult>(
				CreateHttpClient(),
				CreateHttpRequestFactory(),
				GetRequestAuthenticationStep(),
				GetDeserializationStep(),
				GetRequestStepsFirstRank(),
				GetResponseStepsFirstRank(),
				GetSerializationStep(),
				GetRequestStepsSecondRank(),
				GetResponseStepsSecondRank(),
				GetQueryConstructionStep(),
				GetRequestStepsThirdRank(),
				GetResponseStepsThirdRank());
		}
	}
}
