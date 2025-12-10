namespace HttPipe
{
	public abstract class PutHttpPipelineDirector<TPayload, TResult> : HttpPipelineDirector<IPutHttpPipelineBuilder<TPayload, TResult>, IPutHttpPipeline<TPayload, TResult>>
	{
		public sealed override IPutHttpPipeline<TPayload, TResult> Create()
			=> HttPipe.CreatePut<TPayload, TResult>(Configure);
	}

	public abstract class PutHttpPipelineDirector<TQueryModel, TPayload, TResult> : HttpPipelineDirector<IPutHttpPipelineBuilder<TQueryModel, TPayload, TResult>, IPutHttpPipeline<TQueryModel, TPayload, TResult>>
	{
		public sealed override IPutHttpPipeline<TQueryModel, TPayload, TResult> Create()
			=> HttPipe.CreatePut<TQueryModel, TPayload, TResult>(Configure);
	}
}
