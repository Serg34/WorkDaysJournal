using System;

namespace Furmanov.MVP.CreateResource
{
	public interface ICreateResourceView : IView
	{
		event EventHandler<CreateResourceViewModel> Creating;
	}
}