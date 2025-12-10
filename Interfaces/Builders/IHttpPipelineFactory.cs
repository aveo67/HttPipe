namespace HttPipe
{
	public interface IHttpPipelineFactory<TPipeline>
	{
		TPipeline Create();
	}
}
