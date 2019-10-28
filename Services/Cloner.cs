using Force.DeepCloner;

namespace Furmanov.Services
{
	public static class Cloner
	{
		public static T DeepCopy<T>(T obj)
		{
			return obj.DeepClone();
		}
	}
}