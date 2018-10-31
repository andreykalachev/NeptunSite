using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neptun.Models.ViewModels
{
    public class NewsIndexViewModel
    {
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public List<News> News { get; set; }

        public NewsIndexViewModel()
        {

        }
        public NewsIndexViewModel(int pageCount, int currentPage)
        {
            PageCount = pageCount;
            CurrentPage = currentPage;
        }
    }
}