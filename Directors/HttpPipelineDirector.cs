namespace HttPipe
{
	public abstract class HttpPipelineDirector<TBuilder, TPipeline>
	{
		protected abstract void Configure(TBuilder builder);

		public abstract TPipeline Create();
	}
}
