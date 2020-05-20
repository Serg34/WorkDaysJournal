namespace Furmanov.Data.Data
{
	public interface IHasId { int ID { get; set; } }
	public interface IHasName { string Name { get; set; } }
	public interface IViewModel { string ViewModelId { get; set; } }
}
