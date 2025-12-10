using System;
using System.IO;

namespace HttPipe
{
	public static class HttpPipelineBuilderExtensions
	{
		public static IDeserializationBuildingStage<TResult, TBuilder> WithPayloadAsQuery<TPayload, TResult, TBuilder>(this ISerializationBuildingStageCommon<TPayload, TResult, TBuilder> context)
			=> context.WithSerializationStep(new QueryConstructionStep<TPayload>());

		public static IDeserializationBuildingStage<TResult, TBuilder> WithIdAsQuery<TResult, TBuilder>(this ISerializationBuildingStageCommon<Guid, TResult, TBuilder> context)
			=> context.WithSerializationStep(new GuidAsQueryStep());

		public static IDeserializationBuildingStage<TResult, TBuilder> WithPayloadAsJson<TPayload, TResult, TBuilder>(this ISerializationBuildingStage<TPayload, TResult, TBuilder> context)
			=> context.WithSerializationStep(new JsonSerializationStep<TPayload>());

		public static IDeserializationBuildingStage<TResult, TBuilder> WithStringAsContent<TResult, TBuilder>(this ISerializationBuildingStage<String, TResult, TBuilder> context)
			=> context.WithSerializationStep(new StringSerializationStep());

		public static IDeserializationBuildingStage<TResult, TBuilder> WithStreamAsContent<TResult, TBuilder>(this ISerializationBuildingStage<Stream, TResult, TBuilder> context)
			=> context.WithSerializationStep(new StreamSerializationStep());

		public static IDeserializationBuildingStage<TResult, TBuilder> WithByteArrayAsContent<TResult, TBuilder>(this ISerializationBuildingStage<byte[], TResult, TBuilder> context)
			=> context.WithSerializationStep(new ByteArraySerializationStep());

		public static TBuilder WithQuery<TQueryModel, TBuilder>(this IQueryBuildingStage<TQueryModel, TBuilder> context)
			=> context.WithQueryConstructionStep(new QueryConstructionStep<TQueryModel>());

		public static TBuilder WithIdAsQuery<TBuilder>(this IQueryBuildingStage<Guid, TBuilder> context)
			=> context.WithQueryConstructionStep(new GuidAsQueryStep());

		public static TBuilder WithJsonDeserialization<TResult, TBuilder>(this IDeserializationBuildingStage<TResult, TBuilder> context)
			=> context.WithDeserializationStep(new JsonDeserializationStep<TResult>());

		public static TBuilder WithStringAsResult<TBuilder>(this IDeserializationBuildingStage<String, TBuilder> context)
			=> context.WithDeserializationStep(new StringResultExcludingStep());

		public static TBuilder WithStreamAsResult<TBuilder>(this IDeserializationBuildingStage<Stream, TBuilder> context)
			=> context.WithDeserializationStep(new StreamResultExcludingStep());

		public static TBuilder WithBiteArrayAsResult<TBuilder>(this IDeserializationBuildingStage<byte[], TBuilder> context)
			=> context.WithDeserializationStep(new ByteArrayResultExcludingStep());
	}
}
