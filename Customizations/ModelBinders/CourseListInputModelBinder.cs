using System.Threading.Tasks;
using Corso10157.Models.Options;
using Corso10157.Models.Services.ADO.NET.InputModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace Corso10157.Customizations.ModelBinders
{
    public class CourseListInputModelBinder : IModelBinder
    {
        private readonly IOptionsMonitor<CoursesOptions> courseOptions;

        public CourseListInputModelBinder(IOptionsMonitor<CoursesOptions> courseOptions)
        {
            this.courseOptions = courseOptions;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string search = bindingContext.ValueProvider.GetValue("Search").FirstValue;
            string orderby = bindingContext.ValueProvider.GetValue("OrderBy").FirstValue;
            int.TryParse(bindingContext.ValueProvider.GetValue("Page").FirstValue, out int page);
            bool.TryParse(bindingContext.ValueProvider.GetValue("Ascending").FirstValue, out bool ascending);

            var inputModel = new CourseListInputModel(search, page, orderby, ascending, courseOptions.CurrentValue);
            bindingContext.Result = ModelBindingResult.Success(inputModel);
            return Task.CompletedTask; 

        }
    }
}