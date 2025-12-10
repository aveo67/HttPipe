namespace HttPipe
{
	public interface ICompletionBuildingStage<TResult>
		: ICompletionBuildingStageCommon<TResult, ICompletionBuildingStage<TResult>>
	{ }

	public interface ICompletionBuildingStage<TPayload, TResult>
		: ICompletionBuildingStageCommon<TPayload, TResult, ICompletionBuildingStage<TPayload, TResult>>
	{ }

	public interface ICompletionBuildingStage<TQueryModel, TPayload, TResult>
		: ICompletionBuildingStageCommon<TQueryModel, TPayload, TResult, ICompletionBuildingStage<TQueryModel, TPayload, TResult>>
	{ }
}
