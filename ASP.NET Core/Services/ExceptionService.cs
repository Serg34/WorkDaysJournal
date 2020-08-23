using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Furmanov.Services
{
	public class ExceptionService
	{
		private static readonly object LockObject = new object();
		private Data<Exception> _data = new Data<Exception>();

		/// <summary>Константа для чтения и записи ключа запроса в куки</summary>
		public const string RequestKey = "RequestId";

		private ExceptionService() { }

		private static ExceptionService _instance;
		private static ExceptionService Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (LockObject)
					{
						_instance = new ExceptionService();
					}
				}
				return _instance;
			}
		}

		public static void AddCookies(HttpContext context, string requestId)
		{
			if (!context.Request.Cookies.ContainsKey(RequestKey))
			{
				context.Response.Cookies.Append(RequestKey, requestId);
			}
		}
		public static void AddException(HttpRequest request, Exception ex) => Instance.Add(request, ex);
		public static Exception GetLastException(HttpRequest request) => Instance.Get(request);

		private void Add(HttpRequest request, Exception ex)
		{
			var requestId = request.Cookies[RequestKey];
			_data.Add(requestId, ex);
		}

		private Exception Get(HttpRequest request)
		{
			var requestId = request.Cookies[RequestKey];
			var ex = _data.GetLast(requestId);
			return ex;
		}

		private class Data<T> where T : Exception
		{
			private readonly Dictionary<string, T> _data = new Dictionary<string, T>();

			public void Add(string key, T ex)
			{
				lock (LockObject)
				{
					if (_data.ContainsKey(key)) _data[key] = ex;
					else _data.Add(key, ex);
				}
			}

			public T GetLast(string key)
			{
				lock (LockObject)
				{
					if (_data.ContainsKey(key)) return _data[key];
					else return null;
				}
			}
		}
	}
}
