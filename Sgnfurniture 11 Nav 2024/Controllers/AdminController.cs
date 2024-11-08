using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using System.IO;
using Sgnfurniture.Models;
using System.Configuration;
namespace NewsApplication.Controllers
{
  
    public class AdminController : Controller
    {
        Executer executer = new Executer();
        BussinessLayer bussinessLayer = new BussinessLayer();
        DataLayer dataLayer = new DataLayer();
        JavaScriptSerializer js = new JavaScriptSerializer();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult>Categories()
        {
            MasterModel model = new MasterModel();  
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Categories(MasterModel master)
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Subcategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Subcategory(MasterModel master)
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> FurnitureType()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> FurnitureType(MasterModel master)
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> FurnitureMaterial()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> FurnitureMaterial(MasterModel master)
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> FurnitureShape()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> FurnitureShape(MasterModel master)
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> FurnitureColor()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> FurnitureColor(MasterModel master)
        {
            return View();
        }

        //public async Task<string> AddMasterCategorieSave(News news)
        //{
        //    Query<News> query;
        //    if (!string.IsNullOrEmpty(news.TabTitle))
        //    {
        //        query = await Task.Run(() => executer.InsertAndGetIdentityAsync<News>(news, "M_NewsCategoriesSave", true)).ConfigureAwait(false);
        //        if (query.isSuccess)
        //        {
        //            query.Commit();
        //        }
        //        else
        //        {
        //            query.RollBack();
        //        }
        //    }

        //    query = await Task.Run(() => executer.select<News>("GetM_NewsCategories")).ConfigureAwait(false);


        //    var data = js.Serialize(query.resultData);
        //    return data;
        //} 

    }
}