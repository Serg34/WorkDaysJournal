using Force.DeepCloner;

namespace Services
{
	public static class Cloner
	{
		public static T DeepCopy<T>(T obj)
		{
			return obj.DeepClone();
		}
	}
}