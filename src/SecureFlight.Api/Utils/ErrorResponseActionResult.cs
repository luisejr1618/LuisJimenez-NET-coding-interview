using Microsoft.AspNetCore.Mvc;
using SecureFlight.Api.Models;

namespace SecureFlight.Api.Utils;

public class ErrorResponseActionResult : ActionResult
{
    public ErrorResponse Result { get; set; }
}