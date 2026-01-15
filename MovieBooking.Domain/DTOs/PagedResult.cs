using System;
using System.Collections.Generic;

namespace MovieBooking.Domain.DTOs
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();

        // [QUAN TRỌNG] Đổi tên thành TotalRecords để khớp với MovieDAO
        public int TotalRecords { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public PagedResult() { }

        // Constructor dùng cho việc gán nhanh
        public PagedResult(List<T> items, int count, int pageIndex, int pageSize)
        {
            Items = items;
            TotalRecords = count; // Gán vào biến TotalRecords
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}