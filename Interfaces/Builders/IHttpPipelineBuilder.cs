using System;

namespace HttPipe
{
	public interface IHttpPipelineBuilder<TBuilder>
	{
		IAuthenticationBuildingStage<TBuilder> Configure(string url, TimeSpan timeout = default);

		IAuthenticationBuildingStage<TBuilder> Configure(Uri uri, TimeSpan timeout = default);
	}
}
