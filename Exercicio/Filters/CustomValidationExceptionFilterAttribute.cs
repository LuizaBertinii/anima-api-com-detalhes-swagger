#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Linq;
using Exercicio.ModelViews;

namespace Exercicio.Filters
{
    public class CustomValidationExceptionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors
                            .Select(e => e.ErrorMessage ?? e.Exception?.Message)
                            .ToArray()
                    );

                var result = new ObjectResult(new ApiError
                {
                    Message = JsonConvert.SerializeObject(errors),
                    StatusCode = 400,
                })
                {
                    StatusCode = 400
                };

                context.Result = result;
            }
        }
    }
}
