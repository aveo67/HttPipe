using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	public interface IAuthenticationBuildingStage<TBuilder>
	{
		TBuilder WithAuthenticationStep(IRequestStep authenticationStep);

		TBuilder WithAuthenticationStep(Func<HttpRequestMessage, CancellationToken, Task> authenticationAction);

		TBuilder WithoutAuthentication();
	}
}
