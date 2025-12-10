using System;
using System.Net.Http;

namespace HttPipe
{
	internal class PostHttpPipelineBuilder<TPayload, TResult> : HttpPipelineBuilder<TPayload, TResult, ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TPayload, TResult>>, ICompletionBuildingStage<TPayload, TResult>>, ICompletionBuildingStage<TPayload, TResult>, IPostHttpPipelineBuilder<TPayload, TResult>
	{
		protected override ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TPayload, TResult>> AfterAuthenticationBuildingStage => this;

		protected override ICompletionBuildingStage<TPayload, TResult> AfterDeserializationBuildingStage => this;

		public IAuthenticationBuildingStage<ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TPayload, TResult>>> Configure(string url, TimeSpan timeout = default)
			=> Configure(url, HttpMethod.Post, timeout);

		public IAuthenticationBuildingStage<ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TPayload, TResult>>> Configure(Uri uri, TimeSpan timeout = default)
			=> Configure(uri, HttpMethod.Post, timeout);

		public IPostHttpPipeline<TPayload, TResult> Create()
		{
			return CreatePipeline();
		}
	}

	internal class PostHttpPipelineBuilder<TQueryModel, TPayload, TResult> : HttpPipelineBuilder<TQueryModel, TPayload, TResult, IQueryBuildingStage<TQueryModel, ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TQueryModel, TPayload, TResult>>>, ICompletionBuildingStage<TQueryModel, TPayload, TResult>, ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TQueryModel, TPayload, TResult>>>, ICompletionBuildingStage<TQueryModel, TPayload, TResult>, IPostHttpPipelineBuilder<TQueryModel, TPayload, TResult>
	{
		protected override ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TQueryModel, TPayload, TResult>> AfterQueryBuildingStage => this;

		protected override IQueryBuildingStage<TQueryModel, ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TQueryModel, TPayload, TResult>>> AfterAuthenticationBuildingStage => this;

		protected override ICompletionBuildingStage<TQueryModel, TPayload, TResult> AfterDeserializationBuildingStage => this;

		public IAuthenticationBuildingStage<IQueryBuildingStage<TQueryModel, ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TQueryModel, TPayload, TResult>>>> Configure(string url, TimeSpan timeout = default)
			=> Configure(url, HttpMethod.Post, timeout);

		public IAuthenticationBuildingStage<IQueryBuildingStage<TQueryModel, ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TQueryModel, TPayload, TResult>>>> Configure(Uri uri, TimeSpan timeout = default)
			=> Configure(uri, HttpMethod.Post, timeout);

		public IPostHttpPipeline<TQueryModel, TPayload, TResult> Create()
		{
			return CreatePipeline();
		}
	}
}
