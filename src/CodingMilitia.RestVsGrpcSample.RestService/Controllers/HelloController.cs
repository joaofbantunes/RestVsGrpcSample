using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace CodingMilitia.RestVsGrpcSample.RestService.Controllers
{
    [ApiController]
    [Route("hello")]
    public class HelloController : ControllerBase
    {
        [HttpPost]
        public IActionResult Hello(HelloRequest request)
        {
            return Ok(new HelloResponse{ Hello = $"Hello {request.Name}!" });
        }
        
        [HttpPost("complex")]
        public IActionResult ComplexHello(ComplexHelloRequest request)
        {
            var response = new ComplexHelloResponse
            {
                Hello = $"Hello {request.Name}!",
                SimpleHellos = request.SimpleHellos.Select(r => new HelloResponse{ Hello = $"Hello {r.Name}!"}),
                SomeRandomNumbers = request.SomeRandomNumbers
            };
            return Ok(response);
        }

        public class HelloRequest
        {
            public string Name { get; set; }
        }
        
        public class HelloResponse
        {
            public string Hello { get; set; }
        }

        public class ComplexHelloRequest
        {
            public string Name { get; set; }
            public IEnumerable<HelloRequest> SimpleHellos { get; set; }
            public IEnumerable<int> SomeRandomNumbers { get; set; }
        }
        
        public class ComplexHelloResponse
        {
            public string Hello { get; set; }
            public IEnumerable<HelloResponse> SimpleHellos { get; set; }
            public IEnumerable<int> SomeRandomNumbers { get; set; }
        }
    }
}