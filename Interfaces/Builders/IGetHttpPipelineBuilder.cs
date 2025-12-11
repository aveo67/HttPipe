namespace HttPipe
{
	public interface IGetHttpPipelineBuilder<TResult> : IHttpPipelineBuilder<IDeserializationBuildingStage<TResult, ICompletionBuildingStage<TResult>>>, IHttpPipelineBuilder { }

	public interface IGetHttpPipelineBuilder<TQueryModel, TResult> : IHttpPipelineBuilder<IQueryBuildingStage<TQueryModel, IDeserializationBuildingStage<TResult, ICompletionBuildingStage<TQueryModel, TResult>>>>, IHttpPipelineBuilder { }
}
