using Microsoft.AspNetCore.Mvc;

namespace Tests.Extensions
{
    public static class ControllerTestExtension
    {
        public static T GetValue<T>(this ActionResult<T> action)
        {
            return action switch
            {
                _ when action.Value is not null => action.Value,
                _ when action.Result is ObjectResult objectResult && objectResult.Value is T value => value,
                _ => default,
            };
        }
    }
}
