using System;
using System.Net;

namespace Fn.Users.Exceptions
{
	public sealed class UsersException : Exception
	{
        public int StatusCode { get; private set; }

        public UsersException (
			string message,
			int statusCode = 200
		): base(message)
		{
			StatusCode = statusCode;
		}

		private static UsersException _GetInstance(string message, int statusCode=200)
		{
			return new UsersException(message, statusCode);
		}

		public static void ThrowEmptyValue(string message)
		{
			throw _GetInstance(
				message,
				_GetHttpStatusCode(HttpStatusCode.BadRequest)
			);
		}

		private static int _GetHttpStatusCode(HttpStatusCode statusCode)
		{
			return (int)statusCode;
		}
	}
}

