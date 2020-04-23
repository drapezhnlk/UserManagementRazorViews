using System.IO;
using System.Linq;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using UserManagementRazorViews.Interfaces;
using UserManagementRazorViews.Models;

namespace UserManagementRazorViews.Validators
{
    //Need to add data annotation based server validation
    public class UserValidator : AbstractValidator<ManageUserViewModel>
    {
        public UserValidator(IConfiguration configuration, IUserRepository userRepository)
        {
            // Email is required;
            RuleFor(u => u.UserEmail)
                .NotEmpty()
                .WithMessage("User Email should not be empty");

            // Email is correct
            RuleFor(u => u.UserEmail)
                .EmailAddress()
                .WithMessage("User Email should be in email-like format");

            // Name is required
            RuleFor(u => u.UserName)
                .NotEmpty()
                .WithMessage("Name should not be empty");

            // Surname is required
            RuleFor(u => u.UserSurname)
                .NotEmpty()
                .WithMessage("Surname should not be empty");;

            // The maximum number of users for one company is N (Configured value).
            RuleFor(u => u.SelectedCompanyId).Must(selectedCompanyId =>
            {
                var maxCompanyPeopleCount = int.Parse(configuration.GetSection("ValidationRules")["MaxCompanyPeopleCount"]);
                var companyUsers = userRepository.GetByCompanyId(selectedCompanyId).ToList();
                
                return companyUsers == null ? true : companyUsers.Count() < maxCompanyPeopleCount;
            });

            RuleFor(u => u.Photo)
                .Must(file =>
                {
                    if (file == null) return true;

                    var allowedExtensions = new[] {".jpeg", ".jpg", ".png"};
                    var fileExtension = Path.GetExtension(file.FileName);

                    return allowedExtensions.Contains(fileExtension);
                })
                .WithMessage("Uploaded file has invalid format. Please, upload .jpeg, .jpg or .png");
        }
    }
}