﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.ViewModels
{
    public class NewsCategoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int NewsCount { get; set; }
    }
}