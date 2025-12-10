using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttPipe
{
	internal class GuidAsQueryStep : IQueryConstructionStep<Guid>
	{
		public Task ProcessAsync(Guid model, HttpRequestMessage request, CancellationToken token = default)
		{
			request.RequestUri = new Uri(request.RequestUri, model.ToString());

			return Task.CompletedTask;
		}
	}
}
