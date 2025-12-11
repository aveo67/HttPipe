namespace HttPipe
{
	public interface IPutHttpPipelineBuilder<TPayload, TResult> : IHttpPipelineBuilder<ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TPayload, TResult>>>, IHttpPipelineBuilder { }

	public interface IPutHttpPipelineBuilder<TQueryModel, TPayload, TResult> : IHttpPipelineBuilder<IQueryBuildingStage<TQueryModel, ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TQueryModel, TPayload, TResult>>>>, IHttpPipelineBuilder { }
}
