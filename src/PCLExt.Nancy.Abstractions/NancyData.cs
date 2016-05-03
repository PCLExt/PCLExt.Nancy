using System;
using System.Collections.Generic;

namespace PCLExt.Nancy
{
    public class NancyData
    {
        public class PageAction
        {
            public string Page { get; }
            public Func<dynamic, dynamic> Action { get; }

            public PageAction(string page, Func<dynamic, dynamic> action) { Page = page; Action = action; }
        }
        public List<PageAction> List { get; } = new List<PageAction>();

        public void Add(string page, Func<dynamic, dynamic> action) { List.Add(new PageAction(page, action)); }
    }
}