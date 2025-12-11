using System;

namespace HttPipe
{
	public static class HttPipe
	{
		private static TPipeline Create<TPipeline, TBuilder, TBuilderInterface>(TBuilder builder, Action<TBuilderInterface> action)
			where TBuilder : IHttpPipelineFactory<TPipeline>, TBuilderInterface
			where TBuilderInterface : IHttpPipelineBuilder
		{
			action(builder);

			return builder.Create();
		}

		public static IHttpPipeline<TResult> Create<TResult>(Action<ICustomHttpPipelineBuilder<TResult>> buildAction)
			=> Create<IHttpPipeline<TResult>, CustomHttpPipelineBuilder<TResult>, ICustomHttpPipelineBuilder<TResult>>(new CustomHttpPipelineBuilder<TResult>(), buildAction);

		public static IHttpPipeline<TPayload, TResult> Create<TPayload, TResult>(Action<ICustomHttpPipelineBuilder<TPayload, TResult>> buildAction)
			=> Create<IHttpPipeline<TPayload, TResult>, CustomHttpPipelineBuilder<TPayload, TResult>, ICustomHttpPipelineBuilder<TPayload, TResult>>(new CustomHttpPipelineBuilder<TPayload, TResult>(), buildAction);

		public static IHttpPipeline<TQueryModel, TPayload, TResult> Create<TQueryModel, TPayload, TResult>(Action<ICustomHttpPipelineBuilder<TQueryModel, TPayload, TResult>> buildAction)
			=> Create<IHttpPipeline<TQueryModel, TPayload, TResult>, CustomHttpPipelineBuilder<TQueryModel, TPayload, TResult>, ICustomHttpPipelineBuilder<TQueryModel, TPayload, TResult>>(new CustomHttpPipelineBuilder<TQueryModel, TPayload, TResult>(), buildAction);

		public static IGetHttpPipeline<TResult> CreateGet<TResult>(Action<IGetHttpPipelineBuilder<TResult>> buildAction)
			=> Create<IGetHttpPipeline<TResult>, GetHttpPipelineBuilder<TResult>, IGetHttpPipelineBuilder<TResult>>(new GetHttpPipelineBuilder<TResult>(), buildAction);

		public static IGetHttpPipeline<TQueryModel, TResult> CreateGet<TQueryModel, TResult>(Action<IGetHttpPipelineBuilder<TQueryModel, TResult>> buildAction)
			=> Create<IGetHttpPipeline<TQueryModel, TResult>, GetHttpPipelineBuilder<TQueryModel, TResult>, IGetHttpPipelineBuilder<TQueryModel, TResult>>(new GetHttpPipelineBuilder<TQueryModel, TResult>(), buildAction);

		public static IPostHttpPipeline<TPayload, TResult> CreatePost<TPayload, TResult>(Action<IPostHttpPipelineBuilder<TPayload, TResult>> buildAction)
			=> Create<IPostHttpPipeline<TPayload, TResult>, PostHttpPipelineBuilder<TPayload, TResult>, IPostHttpPipelineBuilder<TPayload, TResult>>(new PostHttpPipelineBuilder<TPayload, TResult>(), buildAction);

		public static IPostHttpPipeline<TQueryModel, TPayload, TResult> CreatePost<TQueryModel, TPayload, TResult>(Action<IPostHttpPipelineBuilder<TQueryModel, TPayload, TResult>> buildAction)
			=> Create<IPostHttpPipeline<TQueryModel, TPayload, TResult>, PostHttpPipelineBuilder<TQueryModel, TPayload, TResult>, IPostHttpPipelineBuilder<TQueryModel, TPayload, TResult>>(new PostHttpPipelineBuilder<TQueryModel, TPayload, TResult>(), buildAction);

		public static IPutHttpPipeline<TPayload, TResult> CreatePut<TPayload, TResult>(Action<IPutHttpPipelineBuilder<TPayload, TResult>> buildAction)
			=> Create<IPutHttpPipeline<TPayload, TResult>, PutHttpPipelineBuilder<TPayload, TResult>, IPutHttpPipelineBuilder<TPayload, TResult>>(new PutHttpPipelineBuilder<TPayload, TResult>(), buildAction);

		public static IPutHttpPipeline<TQueryModel, TPayload, TResult> CreatePut<TQueryModel, TPayload, TResult>(Action<IPutHttpPipelineBuilder<TQueryModel, TPayload, TResult>> buildAction)
			=> Create<IPutHttpPipeline<TQueryModel, TPayload, TResult>, PutHttpPipelineBuilder<TQueryModel, TPayload, TResult>, IPutHttpPipelineBuilder<TQueryModel, TPayload, TResult>>(new PutHttpPipelineBuilder<TQueryModel, TPayload, TResult>(), buildAction);
	}
}
