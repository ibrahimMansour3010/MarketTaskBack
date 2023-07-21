using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Dtos.Common
{
    public class APIResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
    }

    public class APIResponse<T> : APIResponse where T : class
    {
        public T Data { get; set; }
    }
}
