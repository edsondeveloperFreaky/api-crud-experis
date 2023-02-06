using FluentValidation.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "OK";
        public int Code { get; set; } = 200;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}
