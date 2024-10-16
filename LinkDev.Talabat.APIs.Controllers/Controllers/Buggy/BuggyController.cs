using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Errors;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Buggy
{
    public class BuggyController : ApiControllerBase
    {
        [HttpGet("notfound")]  // GET: /api/Buggy/notfound
        public IActionResult GetNotFoundResponse() 
        {
            // throw new NotFoundException();
            return NotFound( new ApiResponse(404) );     // 404
        }

        [HttpGet("servererror")]  // GET: /api/Buggy/servererror
        public IActionResult GetServerError()
        {
            throw new Exception(); // 500

            // Handling Exception At the level of endpoint
            /// try
            /// {
            ///     throw new Exception(); // 500
            /// }
            /// catch (Exception ex)
            /// {
            ///     var response = new ApiResponse(500);
            /// 
            ///     Response.WriteAsync(JsonSerializer.Serialize(response));
            /// 
            ///     return StatusCode(500);
            /// }
        }

        [HttpGet("badrequest")]  // GET: /api/Buggy/badrequest
        public IActionResult GetBadRequest()
        {
            return BadRequest( new ApiResponse(400) );   // 400
        }

        [HttpGet("badrequest/{id}")]  // GET: /api/Buggy/badrequest/five
        public IActionResult GetValidationError(int id)   // => 400
        {
            // if SuppressModelStateInvalidFilter = true
            /// if (!ModelState.IsValid) 
            /// {
            ///     var errors = ModelState.Where(P => P.Value.Errors.Count > 0)
            ///                            .SelectMany(P => P.Value.Errors)
            ///                            .Select(P => P.ErrorMessage);
            /// 
            /// 
            ///     return BadRequest(new ApiValidationErrorResponse() 
            ///     { 
            ///         Errors = errors
            ///     });
            /// }

            return Ok();
        }

        [HttpGet("unauthorized")]  // GET: /api/Buggy/unauthorized
        public IActionResult GetUnauthorizedError()
        {
            return Unauthorized( new ApiResponse(401) );   // 401
        }

        [HttpGet("forbidden")]  // GET: /api/Buggy/forbidden
        public IActionResult GetForbiddenRequest()
        {
            return Forbid();   // 403
        }

        //[Authorize]
        //[HttpGet("authorized")]  // GET: /api/Buggy/authorized
        //public IActionResult GetAuthorizedRequest()
        //{
        //    return Ok();
        //}
    }
}
