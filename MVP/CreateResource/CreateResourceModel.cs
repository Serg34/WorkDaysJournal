using Furmanov.MVP.Services.UI;
using System;

namespace Furmanov.MVP.CreateResource
{
	public interface ICreateResourceModel
	{
		event EventHandler<string> Error;
		event EventHandler Changed;
		void CreateResource(CreateResourceViewModel vm);
		void DeleteResource(CreateResourceViewModel vm);
	}

	public class CreateResourceModel : ICreateResourceModel
	{
		private readonly IdataAccessService _db;
		private readonly ResOPViewModel _resOpViewModel;

		public CreateResourceModel(IdataAccessService dataAccessService, ResOPViewModel resOpViewModel)
		{
			_db = dataAccessService;
			_resOpViewModel = resOpViewModel;
		}

		public event EventHandler<string> Error;
		public event EventHandler Changed;

		public void CreateResource(CreateResourceViewModel vm)
		{
			try
			{
				var (isError, error) = ValidateService.Validate(vm);
				if (isError)
				{
					Error?.Invoke(this, error);
					return;
				}

				var resource = new CResource
				{
					Name = vm.Name,
					Object_Id = _resOpViewModel.Object_Id,
					Manager_Id = vm.IsStaff ? _resOpViewModel.ManagerId : null,
					Card = vm.Card,
					Phone = vm.Phone,
					OfficialSalary = vm.OfficialSalary?.ToDecimal(),
				};

				if (_db.SaveResource(resource)) Changed?.Invoke(this, EventArgs.Empty);
			}
			catch (Exception ex) { Error?.Invoke(this, ex.ToString()); }
		}
		public void DeleteResource(CreateResourceViewModel vm)
		{
			try
			{
				var resource = new CResource
				{
					Name = vm.Name,
					Object_Id = _resOpViewModel.Object_Id,
					Manager_Id = vm.IsStaff ? _resOpViewModel.ManagerId : null,
					Card = vm.Card,
					Phone = vm.Phone,
					OfficialSalary = vm.OfficialSalary?.ToDecimal(),
				};

				if (_db.DeleteResource(resource)) Changed?.Invoke(this, EventArgs.Empty);
			}
			catch (Exception ex) { Error?.Invoke(this, ex.ToString()); }
		}
	}
}
