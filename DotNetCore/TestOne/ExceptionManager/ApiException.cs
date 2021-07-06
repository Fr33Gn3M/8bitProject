using Commons;
using System;

namespace ExceptionManager
{
	public class ApiException : Exception
	{
		public int Code { get; set; }

		private string FMessage = string.Empty;

		public ApiException(ResultCode code)
		{
			Code = (int)code;
			FMessage = EnumHelper.GetDescriptionByEnum(code);
		}

		public ApiException(string message)
		{
			Code = (int)ResultCode.CUSTOMEXCEPTION;
			FMessage = message;
		}

		public ApiException(int code, string message)
		{
			Code = code;
			FMessage = message;
		}

		public override string Message
		{
			get
			{
				return FMessage;
			}
		}
	}
}
