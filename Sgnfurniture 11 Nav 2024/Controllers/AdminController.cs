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
            Query<MasterModel> query;
            MasterModel master = new MasterModel();
            query = await Task.Run(() => executer.select<MasterModel>(master, "CategorySaveEditDeletShow")).ConfigureAwait(false);
            master.lstMasterModel = query.resultData;
            return View(master);
        }
        [HttpPost]
        public async Task<ActionResult> Categories(MasterModel master)
        {
            Query<MasterModel> query;
            if (!string.IsNullOrEmpty(master.category_name)||master.mode == "Delete")
            {
                query = await Task.Run(() => executer.InsertAndGetIdentityAsync<MasterModel>(master, "CategorySaveEditDeletShow", true)).ConfigureAwait(false);
                if (query.isSuccess)
                {
                    query.Commit();
                }
                else
                {
                    query.RollBack();
                }
            }
            master.mode = "Data";
            query = await Task.Run(() => executer.select<MasterModel>(master, "CategorySaveEditDeletShow")).ConfigureAwait(false);
         MasterModel obj = new MasterModel();
            obj.lstMasterModel= query.resultData;
            return View(obj);
        }

        [HttpGet]
        public async Task<ActionResult> Subcategory()
        {
            Query<MasterModel> query;
            MasterModel master = new MasterModel();
            query = await Task.Run(() => executer.select<MasterModel>(master, "SubcategorySaveEditDeletShow")).ConfigureAwait(false);
            master.lstMasterModel = query.resultData;
          
            var lst =  await Task.Run(() => executer.select<SelectListItem>( "GetCategoryLst")).ConfigureAwait(false);
            lst.resultData.Add(bussinessLayer.AddDefoultlstDropdwon());
            lst.resultData.Reverse();
            ViewBag.LstCategories=lst.resultData;
            return View(master);
        }

        [HttpPost]
        public async Task<ActionResult> Subcategory(MasterModel master)
        {
            Query<MasterModel> query;
            if (!string.IsNullOrEmpty(master.subcategory_name) || master.mode == "Delete")
            {
                query = await Task.Run(() => executer.InsertAndGetIdentityAsync<MasterModel>(master, "SubcategorySaveEditDeletShow", true)).ConfigureAwait(false);
                if (query.isSuccess)
                {
                    query.Commit();
                }
                else
                {
                    query.RollBack();
                }
            }
            master.mode = "Data";
            query = await Task.Run(() => executer.select<MasterModel>(master, "SubcategorySaveEditDeletShow")).ConfigureAwait(false);
            MasterModel obj = new MasterModel();
            obj.lstMasterModel = query.resultData;

            var lst = await Task.Run(() => executer.select<SelectListItem>("GetCategoryLst")).ConfigureAwait(false);
            lst.resultData.Add(bussinessLayer.AddDefoultlstDropdwon());
            lst.resultData.Reverse();
            ViewBag.LstCategories = lst.resultData;
            return View(obj);
        }
        [HttpGet]
        public async Task<ActionResult> FurnitureType()
        {
            Query<MasterModel> query;
            MasterModel master = new MasterModel();
            query = await Task.Run(() => executer.select<MasterModel>(master, "Furniture_Type_SaveEditDeletShow")).ConfigureAwait(false);
            master.lstMasterModel = query.resultData;

            return View(master);
        }
        [HttpPost]
        public async Task<ActionResult> FurnitureType(MasterModel master)
        {
            Query<MasterModel> query;
            if (!string.IsNullOrEmpty(master.type_name) || master.mode == "Delete")
            {
                query = await Task.Run(() => executer.InsertAndGetIdentityAsync<MasterModel>(master, "Furniture_Type_SaveEditDeletShow", true)).ConfigureAwait(false);
                if (query.isSuccess)
                {
                    query.Commit();
                }
                else
                {
                    query.RollBack();
                }
            }
            master.mode = "Data";
            query = await Task.Run(() => executer.select<MasterModel>(master, "Furniture_Type_SaveEditDeletShow")).ConfigureAwait(false);
            MasterModel obj = new MasterModel();
            obj.lstMasterModel = query.resultData;

         
     
            return View(obj);
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