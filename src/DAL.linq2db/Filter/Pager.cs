using System;

namespace DAL.linq2db.Filter
{
    public class Pager : IPager
    {
        private int _totalItems;
        private int _pageCurrent;
        private int _pageSize;
        private int _pageTotal;
        private int _pageStart;
        private int _pageEnd;

        public Pager(int? pageCurrent, int pageSize = DalParams.DefaultPageSize)
        {
            _pageCurrent = pageCurrent ?? 1;
            _pageSize = pageSize;
        }

        public Pager(int totalItems, int? pageCurrent, int pageSize = DalParams.DefaultPageSize) : this(pageCurrent, pageSize)
        {
            _totalItems = totalItems;
            RecalcPages();
        }

        public int TotalItems
        {
            get { return _totalItems; }
        }
        public int PageCurrent
        {
            get { return _pageCurrent; }
        }
        public int PageSize
        {
            get { return _pageSize; }
        }
        public int PageTotal
        {
            get { return _pageTotal; }
        }
        public int PageStart
        {
            get { return _pageStart; }
        }
        public int PageEnd
        {
            get { return _pageEnd; }
        }

        public void RecalcPages(int totalItems = -1)
        {
            if (totalItems > -1)
            {
                _totalItems = totalItems;
            }

            _pageTotal = (int)Math.Ceiling((decimal)_totalItems / (decimal)_pageSize);
            _pageStart = _pageCurrent - 5;
            _pageEnd = _pageCurrent + 4;

            if (_pageStart <= 0)
            {
                _pageEnd = _pageStart - 1;
                _pageStart = 1;
            }

            if (_pageEnd > _pageTotal)
            {
                _pageEnd = _pageTotal;
            }

            if (_pageEnd > 10)
            {
                _pageStart = _pageEnd - 9;
            }
        }
    }
}