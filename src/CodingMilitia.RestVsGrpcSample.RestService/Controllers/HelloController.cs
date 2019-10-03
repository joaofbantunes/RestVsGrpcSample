using Microsoft.AspNetCore.Mvc;

namespace CodingMilitia.RestVsGrpcSample.RestService.Controllers
{
    [ApiController]
    [Route("/hello")]
    public class HelloController : ControllerBase
    {
        [HttpPost]
        public IActionResult Hello(HelloRequest request)
        {
            return Ok(new HelloResponse{ Hello = $"Hello {request.Name}!" });
        }

        public class HelloRequest
        {
            public string Name { get; set; }
        }
        
        public class HelloResponse
        {
            public string Hello { get; set; }
        }
    }
}