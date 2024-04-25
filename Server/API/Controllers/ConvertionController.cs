using API.Services;
using API.Validation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConvertionController : ControllerBase
    {
        private readonly IConversionService Service;

        public ConvertionController(IConversionService service)
        {
            Service = service;
        }

        [HttpGet("{value}")]
        public Ok<string> Get(
            [PrecisionValidation]
            [Range(0, 999_999_999.99)] decimal value
        )
        {
            var result = Service.Convert(value);
            return TypedResults.Ok<string>(result);
        }
    }
}