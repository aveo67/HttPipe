using System;
using System.Net.Http;

namespace HttPipe
{
	internal class GetHttpPipelineBuilder<TResult> : CustomHttpPipelineBuilder<TResult>, IGetHttpPipelineBuilder<TResult>, IHttpPipelineFactory<IGetHttpPipeline<TResult>>
	{
		public IAuthenticationBuildingStage<IDeserializationBuildingStage<TResult, ICompletionBuildingStage<TResult>>> Configure(string url, TimeSpan timeout = default)
			=> Configure(url, HttpMethod.Get, timeout);

		public IAuthenticationBuildingStage<IDeserializationBuildingStage<TResult, ICompletionBuildingStage<TResult>>> Configure(Uri uri, TimeSpan timeout = default)
			=> Configure(uri, HttpMethod.Get, timeout);

		public new IGetHttpPipeline<TResult> Create()
		{
			return CreatePipeline();
		}
	}

	internal class GetHttpPipelineBuilder<TQueryModel, TResult> : HttpPipelineBuilder<TQueryModel, TResult, IQueryBuildingStage<TQueryModel, IDeserializationBuildingStage<TResult, ICompletionBuildingStage<TQueryModel, TResult>>>, ICompletionBuildingStage<TQueryModel, TResult>>, ICompletionBuildingStage<TQueryModel, TResult>, IGetHttpPipelineBuilder<TQueryModel, TResult>, IHttpPipelineFactory<IGetHttpPipeline<TQueryModel, TResult>>
	{
		protected override IQueryBuildingStage<TQueryModel, IDeserializationBuildingStage<TResult, ICompletionBuildingStage<TQueryModel, TResult>>> AfterAuthenticationBuildingStage => this;

		protected override ICompletionBuildingStage<TQueryModel, TResult> AfterDeserializationBuildingStage => this;

		public IAuthenticationBuildingStage<IQueryBuildingStage<TQueryModel, IDeserializationBuildingStage<TResult, ICompletionBuildingStage<TQueryModel, TResult>>>> Configure(string url, TimeSpan timeout = default)
			=> Configure(url, HttpMethod.Get, timeout);

		public IAuthenticationBuildingStage<IQueryBuildingStage<TQueryModel, IDeserializationBuildingStage<TResult, ICompletionBuildingStage<TQueryModel, TResult>>>> Configure(Uri uri, TimeSpan timeout = default)
			=> Configure(uri, HttpMethod.Get, timeout);

		public IGetHttpPipeline<TQueryModel, TResult> Create()
		{
			return CreatePipeline();
		}
	}
}
