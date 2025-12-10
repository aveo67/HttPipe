namespace HttPipe
{
	public abstract class PutHttpPipelineDirector<TPayload, TResult> : HttpPipelineDirector<IPutHttpPipelineBuilder<TPayload, TResult>, IPutHttpPipeline<TPayload, TResult>>
	{
		public sealed override IPutHttpPipeline<TPayload, TResult> Create()
			=> HttpPipelineFactory.CreatePut<TPayload, TResult>(Configure);
	}

	public abstract class PutHttpPipelineDirector<TQueryModel, TPayload, TResult> : HttpPipelineDirector<IPutHttpPipelineBuilder<TQueryModel, TPayload, TResult>, IPutHttpPipeline<TQueryModel, TPayload, TResult>>
	{
		public sealed override IPutHttpPipeline<TQueryModel, TPayload, TResult> Create()
			=> HttpPipelineFactory.CreatePut<TQueryModel, TPayload, TResult>(Configure);
	}
}
