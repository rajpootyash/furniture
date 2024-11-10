using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sgnfurniture.Models
{
    public class BussinessLayer
    {
        public SelectListItem AddDefoultlstDropdwon()
        {
           return new SelectListItem() { Text = "--Select--", Value = "0" };
        }
    }
}