using System;
using Newtonsoft.Json;

namespace DocumentDBTodo
{
	public class TodoItem
	{
		[JsonProperty (PropertyName = "id")]
		public string Id { get; set; }

		[JsonProperty (PropertyName = "text")]
		public string Text { get; set; }

		[JsonProperty (PropertyName = "complete")]
		public bool Complete { get; set; }
	}
}
