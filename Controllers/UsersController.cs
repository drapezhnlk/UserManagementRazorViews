using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UserManagementRazorViews.Entities;
using UserManagementRazorViews.Interfaces;
using UserManagementRazorViews.Models;

namespace UserManagementRazorViews.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ITitleRepository _titleRepository;
        private readonly IContentService _contentService;

        public UsersController(
            IUserRepository userRepository, 
            ICompanyRepository companyRepository,
            ITitleRepository titleRepository, 
            IContentService contentService,
            ILogHandler logHandler) : base(logHandler)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _titleRepository = titleRepository;
            _contentService = contentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = _userRepository.GetAll().ToList();
            var userRecords = new List<UsersListItemViewModel>();
                
            foreach (var user in users)
            {
                //According to task statement the photo resizing should be performed for each request
                var resizedPhoto = await _contentService.ResizeOriginalImageAsync(user.Photo);
                var resizedPhotoBase64 = resizedPhoto == null ? string.Empty : Convert.ToBase64String(resizedPhoto);
                
                var listItem = new UsersListItemViewModel
                {
                    UserId = user.Id,
                    UserName = user.Name,
                    UserSurname = user.Surname,
                    UserTitles = user.UsersTitles?.Select(t => t.Title.Name).ToList(),
                    BirthDate = user.BirthDate,
                    CompanyName = user.Company?.Name,
                    Photo = resizedPhotoBase64
                };
                
                userRecords.Add(listItem);
            }

            return View(new UsersListViewModel { UserRecords = userRecords });
        }

        [HttpGet("create-user")]
        public IActionResult ManageUser()
        {
            var viewModel = new ManageUserViewModel
            {
                AllCompanies = GetCompaniesSelectList(),
                IsCreatePage = true
            };

            return View(viewModel);
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> Create(ManageUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.AllCompanies = GetCompaniesSelectList();
                viewModel.IsCreatePage = true;
                return View("ManageUser", viewModel);
            }
            
            var user = await MapUser(viewModel);
            _userRepository.Create(user);
            
            return RedirectToAction("Index");
        }

        [HttpGet("edit-user")]
        public IActionResult Edit(int id)
        {
            var userToEdit = _userRepository.Get(id);
            if (userToEdit == null)
            {
                return RedirectToAction("Index");
            }
            
            var viewModel = MapUser(userToEdit);
            viewModel.IsCreatePage = false;

            return View("ManageUser", viewModel);
        }

        [HttpPost("edit-user")]
        public async Task<IActionResult> Edit(ManageUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("ManageUser", viewModel);
            }
            
            var userToUpdate = await MapUser(viewModel);
            _userRepository.Update(userToUpdate);
            return RedirectToAction("Index");
        }

        [HttpPost("delete-user")]
        public IActionResult Delete(int id)
        {
            _userRepository.Delete(id);
            return RedirectToAction("Index");
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<User> MapUser(ManageUserViewModel viewModel)
        {
            var user = _userRepository.Get(viewModel.UserId) ?? new User();
            
            CreateAbsentTitles(viewModel.UserTitles);
            var userTitles = _titleRepository.Get(viewModel.UserTitles);
            var userTitlesMappings = userTitles.Select(t => new UserTitle {Title = t, User = user}).ToList();

            var userCompany = _companyRepository.Get(viewModel.SelectedCompanyId);
            var userPhotoName = await _contentService.SaveUserPhotoAsync(viewModel.Photo);
            
            user.Id = viewModel.UserId;
            user.Name = viewModel.UserName;
            user.Surname = viewModel.UserSurname;
            user.BirthDate = viewModel.UserBirthDate;
            user.Email = viewModel.UserEmail;
            user.Company = userCompany;        
            user.UsersTitles = userTitlesMappings;

            if (!string.IsNullOrEmpty(userPhotoName))
            {
                user.Photo = userPhotoName;   
            }
            
            return user;
        }

        private ManageUserViewModel MapUser(User domainModel)
        {
            var userTitlesNames = domainModel.UsersTitles.Select(ut => ut.Title.Name).ToList();

            return new ManageUserViewModel
            {
                UserId = domainModel.Id,
                UserName = domainModel.Name,
                UserSurname = domainModel.Surname,
                UserBirthDate = domainModel.BirthDate,
                UserEmail = domainModel.Email,
                UserTitles = userTitlesNames,
                SelectedCompanyId = domainModel.Company.Id,
                AllCompanies = GetCompaniesSelectList(),
            };
        }

        private void CreateAbsentTitles(IEnumerable<string> postedUserTitlesNames)
        {
            var allTitlesNames = _titleRepository.GetAll().Select(t => t.Name);

            var newTitles = postedUserTitlesNames
                .Where(tn => !allTitlesNames.Contains(tn))
                .Select(tn => new Title { Name = tn });

            _titleRepository.Create(newTitles);
        }

        private SelectList GetCompaniesSelectList()
        {
            var allCompanies = _companyRepository.GetAll();

            return new SelectList(allCompanies, "Id", "Name");
        }
    }
}
