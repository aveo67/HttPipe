using System;

namespace HttPipe
{
	internal class HttpPipelineException : Exception
	{
		public HttpPipelineException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
