using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http;
using System.Text;

namespace HttPipe
{
	internal class JsonContent : StringContent
	{
		private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			Culture = CultureInfo.InvariantCulture,
			Formatting = Formatting.Indented,
		};

		private readonly string _content;

		private JsonContent(string content) : base(content, Encoding.UTF8, "application/json")
		{
			_content = content;
		}

		public static JsonContent Create(object model, JsonSerializerSettings settings = null)
		{
			settings ??= Settings;

			var content = JsonConvert.SerializeObject(model, settings);

			return new JsonContent(content);
		}

		public override string ToString() => _content;
	}
}

