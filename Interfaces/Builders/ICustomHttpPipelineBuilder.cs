namespace HttPipe
{
	public interface ICustomHttpPipelineBuilder<TResult>
		: ICustomHttpPipelineBuilderCommon<IDeserializationBuildingStage<TResult, ICompletionBuildingStage<TResult>>>
		, IHttpPipelineBuilder
	{ }

	public interface ICustomHttpPipelineBuilder<TPayload, TResult>
		: ICustomHttpPipelineBuilderCommon<ISerializationBuildingStageCommon<TPayload, TResult, ICompletionBuildingStage<TPayload, TResult>>>,
		IHttpPipelineBuilder
	{ }

	public interface ICustomHttpPipelineBuilder<TQueryModel, TPayload, TResult>
		: ICustomHttpPipelineBuilderCommon<IQueryBuildingStage<TQueryModel, ISerializationBuildingStage<TPayload, TResult, ICompletionBuildingStage<TQueryModel, TPayload, TResult>>>>,
		IHttpPipelineBuilder
	{ }
}
