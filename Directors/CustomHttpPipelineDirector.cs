namespace HttPipe
{
	public abstract class CustomHttpPipelineDirector<TResult> : HttpPipelineDirector<ICustomHttpPipelineBuilder<TResult>, IHttpPipeline<TResult>>
	{
		public sealed override IHttpPipeline<TResult> Create()
			=> HttpPipelineFactory.Create<TResult>(Configure);
	}

	public abstract class CustomHttpPipelineDirector<TPayload, TResult> : HttpPipelineDirector<ICustomHttpPipelineBuilder<TPayload, TResult> ,IHttpPipeline<TPayload, TResult>>
	{
		public sealed override IHttpPipeline<TPayload, TResult> Create()
			=> HttpPipelineFactory.Create<TPayload, TResult>(Configure);
	}

	public abstract class CustomHttpPipelineDirector<TQueryModel, TPayload, TResult> : HttpPipelineDirector<ICustomHttpPipelineBuilder<TQueryModel, TPayload, TResult>, IHttpPipeline<TQueryModel, TPayload, TResult>>
	{
		public sealed override IHttpPipeline<TQueryModel, TPayload, TResult> Create()
			=> HttpPipelineFactory.Create<TQueryModel, TPayload, TResult>(Configure);
	}
}
