namespace HttPipe
{
	public interface ICustomHttpPipelineBuilder<TResult>
		: ICustomHttpPipelineBuilderCommon<IDeserializationBuildingStage<TResult, ICompletionBuildingStage<TResult>>>,
		IHttpPipelineFactory<IHttpPipeline<TResult>>
	{ }

	public interface ICustomHttpPipelineBuilder<TPayload, TResult>
		: ICustomHttpPipelineBuilderCommon<ISerializationBuildingStageCommon<TPayload, TResult, ICompletionBuildingStage<TPayload, TResult>>>,
		IHttpPipelineFactory<IHttpPipeline<TPayload, TResult>>
	{ }

	public interface ICustomHttpPipelineBuilder<TQueryModel, TPayload, TResult>
		: ICustomHttpPipelineBuilderCommon<IQueryBuildingStage<TQueryModel, ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TQueryModel, TPayload, TResult>>>>,
		IHttpPipelineFactory<IHttpPipeline<TQueryModel, TPayload, TResult>>
	{ }
}
