using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	internal class QueryConstructionStepAction<TQueryModel> : RequestStepAction<TQueryModel>, IQueryConstructionStep<TQueryModel>
	{
		public QueryConstructionStepAction(Func<TQueryModel, HttpRequestMessage, CancellationToken, Task> action) : base(action)
		{
		}
	}
}
