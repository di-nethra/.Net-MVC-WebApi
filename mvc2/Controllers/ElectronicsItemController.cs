using mvc2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace mvc2.Controllers
{
    public class ElectronicsItemController : Controller
    { 
        // GET: ElectronicsItem
        public ActionResult Index()
        {
            IEnumerable<MvcElectronicModel> ElectronicsList;
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("ElectronicItems").Result;
            ElectronicsList = response.Content.ReadAsAsync<IEnumerable<MvcElectronicModel>>().Result;

            return View(ElectronicsList);
        }


        public ActionResult Create(int id = 0)
        {
            if (id == 0)
            {///HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync("ElectronicItems", electronic).Result;
                return View();
            }
            else
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("ElectronicItems/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MvcElectronicModel>().Result);
            }
        }
        [HttpPost]

        public ActionResult Create(MvcElectronicModel electronic)
        {
            if (electronic.ID == 0)
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync("ElectronicItems", electronic).Result;
                return RedirectToAction("Index");
            }
            else
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.PutAsJsonAsync("ElectronicItems/"+electronic.ID , electronic).Result;
                return RedirectToAction("Index");
            }
          
        }
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.DeleteAsync("ElectronicItems/" +id.ToString()).Result;
            return RedirectToAction("Index");
        }
    }
}