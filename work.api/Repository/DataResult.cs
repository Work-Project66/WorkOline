using System;
using System.Collections.Generic;
using System.Text;

namespace work.api.Repository
{
    public class PageEntity
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
    public class Result<T>
    {
        public List<T> List { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        private int _totalPage;
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            get
            {
                if (PageSize > 0)
                {
                    if (TotalCount % PageSize == 0)
                    {
                        _totalPage = TotalCount / PageSize;
                    }
                    else
                    {
                        _totalPage = TotalCount / PageSize + 1;
                    }
                }
                return _totalPage;
            }
        }
    }
}
