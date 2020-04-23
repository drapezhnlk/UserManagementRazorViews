using Microsoft.AspNetCore.Mvc.ModelBinding;
using UserManagementRazorViews.Models;

namespace UserManagementRazorViews.ModelBinders
{
    public class ManageUserModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(ManageUserViewModel) 
                    ? new ManageUserModelBinder() 
                    : null;
        }
    }
}