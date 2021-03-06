﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaniDomu.ViewModels
{
    public class SumCategoryViewModel
    {
        public string CategoryName { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal TotalAmount { get; set; }
    }
}