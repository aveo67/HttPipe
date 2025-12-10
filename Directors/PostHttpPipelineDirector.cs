namespace HttPipe
{
	public abstract class PostHttpPipelineDirector<TPayload, TResult> : HttpPipelineDirector<IPostHttpPipelineBuilder<TPayload, TResult>, IPostHttpPipeline<TPayload, TResult>>
	{
		public sealed override IPostHttpPipeline<TPayload, TResult> Create()
			=> HttPipe.CreatePost<TPayload, TResult>(Configure);
	}

	public abstract class PostHttpPipelineDirector<TQueryModel, TPayload, TResult> : HttpPipelineDirector<IPostHttpPipelineBuilder<TQueryModel, TPayload, TResult>, IPostHttpPipeline<TQueryModel, TPayload, TResult>>
	{
		public sealed override IPostHttpPipeline<TQueryModel, TPayload, TResult> Create()
			=> HttPipe.CreatePost<TQueryModel, TPayload, TResult>(Configure);
	}
}
