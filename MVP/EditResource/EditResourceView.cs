using Furmanov.Dal.Dto;

namespace Furmanov.MVP.EditResource
{
	public interface IEditResourceView : IView
	{
		SalaryPay ResOp { get; }
		string ResourceName { get; }
	}
}