namespace HttPipe
{
	public interface IGetHttpPipelineBuilder<TResult> : IHttpPipelineBuilder<IDeserializationBuildingStage<TResult, ICompletionBuildingStage<TResult>>>, IHttpPipelineFactory<IGetHttpPipeline<TResult>> { }

	public interface IGetHttpPipelineBuilder<TQueryModel, TResult> : IHttpPipelineBuilder<IQueryBuildingStage<TQueryModel, IDeserializationBuildingStage<TResult, ICompletionBuildingStage<TQueryModel, TResult>>>>, IHttpPipelineFactory<IGetHttpPipeline<TQueryModel, TResult>> { }
}
