using System;
using System.Threading.Tasks;
using DatingAppApi.Extensions;
using DatingAppApi.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace DatingAppApi.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userId = resultContext.HttpContext.User.GetUserID();

            var repo = resultContext.HttpContext.RequestServices.GetService<IUnitOfWork>();
            var user = await repo.UserRepository.GetUserById(userId);

            user.LastActive = DateTime.UtcNow;
            await repo.Complete();
        }
    }
}