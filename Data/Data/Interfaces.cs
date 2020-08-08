namespace Furmanov.Data.Data
{
	public interface IHasName
	{
		string Name { get; set; }
	}

	public interface IViewModel
	{
		string ViewModelId { get; set; }
	}
}
