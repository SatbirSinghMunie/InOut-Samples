using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIResource.Models
{
    public class BaseResponse<T>
    {
        public T Data { get; set; } = default(T);

        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public PageInfo Paging { get; set; } = null;
    }

    public class PageInfo
    {
        public int ItemsPerPage { get; set; }
        public int ItemsReturned { get; set; }
        public int NumberOfPages { get; set; }
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public string ContinuationToken { get; set; }
    }
}
