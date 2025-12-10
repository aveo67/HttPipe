using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	public interface ISerializationBuildingStage<TPayload, TResult, TBuilder>
	{
		IDeserializationBuildingStage<TResult, TBuilder> WithSerializationStep(IRequestStep<TPayload> serializationStep);

		IDeserializationBuildingStage<TResult, TBuilder> WithSerializationStep(Func<TPayload, HttpRequestMessage, CancellationToken, Task> serializationAction);

		IDeserializationBuildingStage<TResult, TBuilder> WithoutSerialization();
	}
}
