using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectARTEMIS.Controllers
{
    [Route("api/v1/schools")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly SchoolService _schoolService;

        public SchoolController(SchoolService schoolService) => _schoolService = schoolService;
        
        [HttpGet]
        public async Task<IActionResult> GetSchools()
        {
            var result = await _schoolService.GetSchoolsAsync();

            return Ok(result);
        }
    }
}
