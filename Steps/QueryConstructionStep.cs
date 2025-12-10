using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Web;

namespace HttPipe
{
	internal class QueryConstructionStep<TQueryModel> : IQueryConstructionStep<TQueryModel>
	{
		public Task ProcessAsync(TQueryModel model, HttpRequestMessage request, CancellationToken token)
		{
			var s = WebSerializer.ToQueryString(model);

			request.RequestUri = new Uri(request.RequestUri, s);

			return Task.CompletedTask;
		}
	}
}
