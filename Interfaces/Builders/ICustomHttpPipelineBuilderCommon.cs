using System;
using System.Net.Http;

namespace HttPipe
{
	public interface ICustomHttpPipelineBuilderCommon<TBuilder>
	{
		IAuthenticationBuildingStage<TBuilder> Configure(string url, HttpMethod method, TimeSpan timeout = default);

		IAuthenticationBuildingStage<TBuilder> Configure(Uri uri, HttpMethod method, TimeSpan timeout = default);
	}
}
