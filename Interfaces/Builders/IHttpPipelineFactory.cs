namespace HttPipe
{
	internal interface IHttpPipelineFactory<TPipeline>
	{
		TPipeline Create();
	}
}
