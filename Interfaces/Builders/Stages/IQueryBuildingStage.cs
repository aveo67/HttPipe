using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	public interface IQueryBuildingStage<TQueryModel, TBuilder>
	{
		TBuilder WithQueryConstructionStep(IQueryConstructionStep<TQueryModel> queryConstructionStep);

		TBuilder WithQueryConstructionStep(Func<TQueryModel, HttpRequestMessage, CancellationToken, Task> queryConstructionAction);

		TBuilder WithoutQuery();
	}
}
