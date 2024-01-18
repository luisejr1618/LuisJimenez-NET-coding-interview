using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecureFlight.Api.Models;
using SecureFlight.Api.Utils;
using SecureFlight.Core;

namespace SecureFlight.Api.Controllers;

public class SecureFlightBaseController : ControllerBase
{
    private readonly IMapper _mapper;

    public SecureFlightBaseController(IMapper mapper)
    {
        _mapper = mapper;
    }
    protected IActionResult GetResult<TResult, TDataTransferObject>(OperationResult<TResult> result)
    {
        if (!result.Succeeded)
        {
            return new ErrorResponseActionResult
            {
                Result = new ErrorResponse
                {
                    Error = new Error
                    {
                        Code = result.Error.Code,
                        Message = result.Error.Message
                    }
                }
            };
        }

        return Ok(_mapper.Map<TResult, TDataTransferObject>(result));
    }
}