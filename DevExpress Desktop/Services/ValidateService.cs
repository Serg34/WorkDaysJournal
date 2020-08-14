using FluentValidation;
using Furmanov.Services;
using System;

namespace Furmanov.UI.Services
{
	public static class ValidateService
	{
		public static void Validate<TViewModel>(
			DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e,
			AbstractValidator<TViewModel> validator,
			TViewModel focusedVm,
			string fieldName)
		{
			var prop = focusedVm.GetType().GetProperty(fieldName);
			if (prop == null) return;
			var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
			var val = e.Value == null || string.IsNullOrEmpty(e.Value.ToString()) ? null
				: Convert.ChangeType(e.Value, type);

			var vmToValidate = Cloner.DeepCopy(focusedVm);
			prop.SetValue(vmToValidate, val);

			var res = validator.Validate(vmToValidate, fieldName);
			if (!res?.IsValid ?? false)
			{
				e.ErrorText = string.Join("\n", res.Errors);
				e.Valid = false;
			}
			else
			{
				prop.SetValue(focusedVm, val);
			}
		}
	}
}
