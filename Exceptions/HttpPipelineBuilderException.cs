using System;

namespace HttPipe
{
	internal class HttpPipelineBuilderException : Exception
	{
		public HttpPipelineBuilderException()
		{
		}

		public HttpPipelineBuilderException(string message) : base(message)
		{
		}
	}
}
