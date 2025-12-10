namespace HttPipe
{
	internal class CustomHttpPipelineBuilder<TResult> : HttpPipelineBuilder<TResult, IDeserializationBuildingStage<TResult, ICompletionBuildingStage<TResult>>, ICompletionBuildingStage<TResult>>, ICompletionBuildingStage<TResult>, ICustomHttpPipelineBuilder<TResult>
	{
		protected override IDeserializationBuildingStage<TResult, ICompletionBuildingStage<TResult>> AfterAuthenticationBuildingStage => this;

		protected override ICompletionBuildingStage<TResult> AfterDeserializationBuildingStage => this;

		public IHttpPipeline<TResult> Create()
		{
			return CreatePipeline();
		}
	}

	internal class CustomHttpPipelineBuilder<TPayload, TResult> : HttpPipelineBuilder<TPayload, TResult, ISerializationBuildingStageCommon<TPayload, TResult, ICompletionBuildingStage<TPayload, TResult>>, ICompletionBuildingStage<TPayload, TResult>>, ICompletionBuildingStage<TPayload, TResult>, ICustomHttpPipelineBuilder<TPayload, TResult>
	{
		protected override ISerializationBuildingStageCommon<TPayload, TResult, ICompletionBuildingStage<TPayload, TResult>> AfterAuthenticationBuildingStage => this;

		protected override ICompletionBuildingStage<TPayload, TResult> AfterDeserializationBuildingStage => this;

		public IHttpPipeline<TPayload, TResult> Create()
		{
			return CreatePipeline();
		}
	}

	internal class CustomHttpPipelineBuilder<TQueryModel, TPayload, TResult> : HttpPipelineBuilder<TQueryModel, TPayload, TResult, IQueryBuildingStage<TQueryModel, ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TQueryModel, TPayload, TResult>>>, ICompletionBuildingStage<TQueryModel, TPayload, TResult>, ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TQueryModel, TPayload, TResult>>>, ICompletionBuildingStage<TQueryModel, TPayload, TResult>, ICustomHttpPipelineBuilder<TQueryModel, TPayload, TResult>
	{
		protected override ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TQueryModel, TPayload, TResult>> AfterQueryBuildingStage => this;

		protected override IQueryBuildingStage<TQueryModel, ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TQueryModel, TPayload, TResult>>> AfterAuthenticationBuildingStage => this;

		protected override ICompletionBuildingStage<TQueryModel, TPayload, TResult> AfterDeserializationBuildingStage => this;

		public IHttpPipeline<TQueryModel, TPayload, TResult> Create()
		{
			return CreatePipeline();
		}
	}
}
