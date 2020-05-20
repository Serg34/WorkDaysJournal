using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using SC.Common.Services;

namespace Furmanov.Services.Repositories
{
	public interface IRepository<TType>
	{
		TType Load();
		void Save(TType item);
	}
	public class XmlRepository<TType> : IRepository<TType> where TType : new()
	{
		private readonly string _fileName;

		public XmlRepository(string fileName = null)
		{
			if (fileName.IsEmpty())
			{
				var type = typeof(TType);
				var typeName = type.IsGenericType ? type.GenericTypeArguments.First().Name + "s"
								: typeof(TType).Name.Replace("[]", "s");

				fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"{typeName}.xml");
			}

			_fileName = fileName;
		}

		public TType Load()
		{
			try
			{
				if (File.Exists(_fileName))
				{
					var serializer = new XmlSerializer(typeof(TType));
					using (TextReader textReader = new StreamReader(_fileName))
						return (TType)serializer.Deserialize(textReader);
				}
				return new TType();
			}
			catch
			{
				return new TType();
			}
		}

		public void Save(TType item)
		{
			var di = new DirectoryInfo(Path.GetDirectoryName(_fileName) ?? throw new InvalidOperationException());
			if (!di.Exists) di.Create();

			var serializer = new XmlSerializer(typeof(TType));
			using (TextWriter textWriter = new StreamWriter(_fileName))
				serializer.Serialize(textWriter, item);
		}
	}
}
