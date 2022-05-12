using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ArchitecturePortalBackend.API.Controllers;

/// <summary>
/// ErrorsController
/// </summary>
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    /// <summary>
    /// HandleError
    /// </summary>
    /// <returns></returns>
    [Route("/error")]
    public IActionResult HandleError() => Problem();

    /// <summary>
    /// HandleErrorDevelopment
    /// </summary>
    /// <param name="hostEnvironment"></param>
    /// <returns></returns>
    [Route("/error-development")]
    public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsDevelopment())
        {
            return NotFound();
        }

        var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

        var index = 0;
        var stackTraceDictionary = exceptionHandlerFeature.Error.StackTrace?.Split("\n").ToDictionary(s => index++, s => s.Trim().Trim('\r'));

        return Problem(
            detail: string.Join("\n", stackTraceDictionary!.Select(x => x.Key + "=" + x.Value).ToArray()),
            //detail: exceptionHandlerFeature.Error.StackTrace,
            title: exceptionHandlerFeature.Error.Message);
    }
}