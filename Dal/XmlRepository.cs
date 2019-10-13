using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Furmanov.Dal
{
	public interface IRepository<TType>
	{
		TType Load();
		void Save(TType item);
	}
	public class XmlDataContractRepository<TType> : IRepository<TType> where TType : new()
	{
		private readonly string _fileName;

		protected Type[] ExtraTypes = null;

		public XmlDataContractRepository(string fileName = null)
		{
			if (string.IsNullOrEmpty(fileName))
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
					using (var stream = File.OpenRead(_fileName))
					{
						var serializer = new DataContractSerializer(typeof(TType), ExtraTypes);
						return (TType)serializer.ReadObject(stream);
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
			var di = new DirectoryInfo(Path.GetDirectoryName(_fileName));
			if (!di.Exists) di.Create();

			var xmlSerializer = new DataContractSerializer(typeof(TType),
				new DataContractSerializerSettings { PreserveObjectReferences = true });
			using (var fileStream = new FileStream(_fileName, FileMode.Create))
			using (var writer = new XmlTextWriter(fileStream, Encoding.UTF8) { Formatting = Formatting.Indented })
			{
				xmlSerializer.WriteObject(writer, item);
				writer.Flush();
			}
		}
	}
}