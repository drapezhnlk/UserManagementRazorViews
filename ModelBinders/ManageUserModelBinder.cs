using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using UserManagementRazorViews.Models;

namespace UserManagementRazorViews.ModelBinders
{
    public class ManageUserModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var idValue = bindingContext.ValueProvider.GetValue("UserId").FirstValue;
            var nameValue = bindingContext.ValueProvider.GetValue("UserName").FirstValue;
            var surnameValue = bindingContext.ValueProvider.GetValue("UserSurname").FirstValue;
            var emailValue = bindingContext.ValueProvider.GetValue("UserEmail").FirstValue;
            var birthdateValue = bindingContext.ValueProvider.GetValue("UserBirthDate").FirstValue;
            var selectedCompanyIdValue = bindingContext.ValueProvider.GetValue("SelectedCompanyId").FirstValue;

            var requestForm = bindingContext.ActionContext.HttpContext.Request.Form;
            var formFile = requestForm.Files.Any() ? requestForm.Files.First() : null;
            var userTitlesValues = requestForm.Keys
                .Where(k => k.Contains("UserTitles"))
                .Where(k => !string.IsNullOrEmpty(bindingContext.ValueProvider.GetValue(k).FirstValue))
                .Select(k => bindingContext.ValueProvider.GetValue(k).FirstValue.Trim().ToLower())
                .ToList();

            bindingContext.Result = ModelBindingResult.Success(
                new ManageUserViewModel
                {
                    UserId = int.Parse(idValue),
                    UserName = nameValue,
                    UserSurname = surnameValue,
                    UserEmail = emailValue,
                    UserTitles = userTitlesValues,
                    UserBirthDate = DateTime.Parse(birthdateValue),
                    Photo = formFile,
                    SelectedCompanyId = selectedCompanyIdValue == null ? 0 : int.Parse(selectedCompanyIdValue)
                });

            return Task.CompletedTask;
        }
    }
}