using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	public interface IDeserializationBuildingStage<TResult, TBuilder>
	{
		TBuilder WithDeserializationStep(IResponseStep<TResult> deserializationStep);

		TBuilder WithDeserializationStep(Func<TResult, HttpRequestMessage, HttpResponseMessage, CancellationToken, Task<TResult>> deserializationAction);

		TBuilder WithoutDeserialization();
	}
}
