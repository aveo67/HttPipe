namespace HttPipe
{
	public abstract class GetHttpPipelineDirector<TResult> : HttpPipelineDirector<IGetHttpPipelineBuilder<TResult>, IGetHttpPipeline<TResult>>
	{
		public sealed override IGetHttpPipeline<TResult> Create()
			=> HttPipe.CreateGet<TResult>(Configure);
	}

	public abstract class GetHttpPipelineDirector<TQueryModel, TResult> : HttpPipelineDirector<IGetHttpPipelineBuilder<TQueryModel, TResult>, IGetHttpPipeline<TQueryModel, TResult>>
	{
		public sealed override IGetHttpPipeline<TQueryModel, TResult> Create()
			=> HttPipe.CreateGet<TQueryModel, TResult>(Configure);
	}
}
