using System.Collections.Generic;

namespace CodingMilitia.RestVsGrpcSample.Benchmark
{
    public class JsonHelloRequest
    {
        public string Name { get; set; }
    }
        
    public class JsonHelloResponse
    {
        public string Hello { get; set; }
    }

    public class JsonComplexHelloRequest
    {
        public string Name { get; set; }
        public IEnumerable<JsonHelloRequest> SimpleHellos { get; set; }
        public IEnumerable<int> SomeRandomNumbers { get; set; }
    }
        
    public class JsonComplexHelloResponse
    {
        public string Hello { get; set; }
        public IEnumerable<JsonHelloResponse> SimpleHellos { get; set; }
        public IEnumerable<int> SomeRandomNumbers { get; set; }
    }
}