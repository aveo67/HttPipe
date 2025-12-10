namespace HttPipe
{
	public interface IPostHttpPipelineBuilder<TPayload, TResult> : IHttpPipelineBuilder<ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TPayload, TResult>>>, IHttpPipelineFactory<IPostHttpPipeline<TPayload, TResult>> { }

	public interface IPostHttpPipelineBuilder<TQueryModel, TPayload, TResult> : IHttpPipelineBuilder<IQueryBuildingStage<TQueryModel, ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TQueryModel, TPayload, TResult>>>>, IHttpPipelineFactory<IPostHttpPipeline<TQueryModel, TPayload, TResult>> { }
}
