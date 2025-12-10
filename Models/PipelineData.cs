namespace HttPipe
{
	internal class PipelineData<TResult>
	{
		public TResult Result { get; set; }
	}

	internal class PipelineData<TPayload, TResult> : PipelineData<TResult>
	{
		public TPayload Payload { get; set; }
	}

	internal class PipelineData<TQueryModel, TPayload, TResult> : PipelineData<TPayload, TResult>
	{
		public TQueryModel QueryModel { get; set; }
	}
}
