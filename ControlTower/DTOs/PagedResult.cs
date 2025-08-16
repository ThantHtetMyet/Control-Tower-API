using System;
using System.Collections.Generic;

namespace ControlTower.DTOs
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int Page { get; set; }          // 1-based page index
        public int PageSize { get; set; }
        public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}