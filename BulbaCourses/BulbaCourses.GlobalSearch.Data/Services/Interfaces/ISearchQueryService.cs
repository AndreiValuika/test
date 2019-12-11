﻿using BulbaCourses.GlobalSearch.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulbaCourses.GlobalSearch.Data.Services.Interfaces
{
    public interface ISearchQueryService
    {
        IEnumerable<SearchQueryDB> GetAll();
        SearchQueryDB GetById(string id);
        SearchQueryDB Add(SearchQueryDB query);
        void RemoveById(string id);
        void RemoveAll();
    }
}