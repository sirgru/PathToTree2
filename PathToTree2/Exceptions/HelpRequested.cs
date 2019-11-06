using System;
using System.Runtime.Serialization;

namespace PathToTree2
{
	[Serializable]
	public class HelpRequested : Exception
	{
		public HelpRequested()
		{
		}

		public HelpRequested(string message) : base(message)
		{
		}

		public HelpRequested(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected HelpRequested(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
