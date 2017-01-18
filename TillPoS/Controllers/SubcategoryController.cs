using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Handlers;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TillPoS.Models;
using System.Text;

namespace TillPoS.Controllers
{
    public class SubcategoryController : Controller
    {
        string url = "http://webapi20170117015441.azurewebsites.net/api/SubCategory";
        // GET: Category
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage response = await client.GetAsync(url + "/get");
                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;

                    var SubCategory = JsonConvert.DeserializeObject<List<SubCategoryModel>>(responseData);
                    return View(SubCategory);
                }
            }
            return View("Error");
        }

        // GET: Category/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Category/Create
        public async Task<ActionResult> Create()
        {
            SubCategoryModel s = new SubCategoryModel();
            string url = "http://webapi20170117015441.azurewebsites.net/api/Category";
            using (var client = new HttpClient())
            {


                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage response = await client.GetAsync(url + "/get");
                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    List<Dropdownlist> l = new List<Dropdownlist>();
                    var Category = JsonConvert.DeserializeObject<List<CategoryModel>>(responseData);
                    // m.dep = Category;
                    for (int i = 0; i < Category.Count; i++)
                    {
                        Dropdownlist d = new Dropdownlist();

                        d.Categoryid = Category[i].Id;
                        d.Name = Category[i].Name;
                        l.Add(d);
                    }
                    // s.dep = l;
                    ViewBag.Drpdwn = new SelectList(l, "Categoryid", "Name"); ;
                }
            }
            return View(s);
        }

        //POST: Category/Create
        [HttpPost]
        public async Task<ActionResult> Create(SubCategoryModel collection)
        {
            try
            {
                HttpClient client = new HttpClient();
                var content = JsonConvert.SerializeObject(collection);

                HttpResponseMessage response = await client.PostAsJsonAsync(url + "/Post/", collection);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Error");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            SubCategoryModel s = new SubCategoryModel();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url1 = "http://webapi20170117015441.azurewebsites.net/api/Category";
                List<Dropdownlist> l = new List<Dropdownlist>();
                HttpResponseMessage response = await client.GetAsync(url1 + "/get");
                if (response.IsSuccessStatusCode)
                {
                    var responseData1 = response.Content.ReadAsStringAsync().Result;

                    var Category = JsonConvert.DeserializeObject<List<CategoryModel>>(responseData1);
                    // m.dep = Category;
                    for (int i = 0; i < Category.Count; i++)
                    {
                        Dropdownlist d = new Dropdownlist();

                        d.Categoryid = Category[i].Id;
                        d.Name = Category[i].Name;
                        l.Add(d);
                    }
                    // s.dep = l;
                    ViewBag.Drpdwn = new SelectList(l, "Categoryid", "Name"); ;
                }
                HttpResponseMessage responseMessage = await client.GetAsync(url + "/get/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    var SubCategory = JsonConvert.DeserializeObject<SubCategoryModel>(responseData);

                    return View(SubCategory);
                }
            }
            return View("Error");
        }

        //POST: Category/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(string id, SubCategoryModel m)
        {

            try
            { // TODO: Add update logic here
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/Put", m);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    return View("Error");
                }
            }
            catch
            {
                return View();
            }
            return View();
        }

        // GET: Category/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await client.GetAsync(url + "/get/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    var SubCategory = JsonConvert.DeserializeObject<SubCategoryModel>(responseData);
                    ViewBag.messag = Convert.ToString(TempData["message"]);
                    return View(SubCategory);
                }
            }
            return View("Error");
        }

        // POST: Category/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(string id, SubCategoryModel collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string url1 = "http://webapi20170117015441.azurewebsites.net/api/Product";
                    List<Dropdownlist1> l = new List<Dropdownlist1>();
                    HttpResponseMessage response = await client.GetAsync(url1 + "/get");
                    if (response.IsSuccessStatusCode)
                    {
                        var responseData1 = response.Content.ReadAsStringAsync().Result;
                        var Category = JsonConvert.DeserializeObject<List<ProductModel>>(responseData1);
                        for (int i = 0; i < Category.Count; i++)
                        {
                            if (Category[i].SubCategoryid == id)
                            {
                                TempData["message"] = "u can't delete";
                                return RedirectToAction("Delete", "SubCategory", id);
                            }
                        }
                    }
                    
                    HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/Delete/" + id);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else { View("Error"); }
                }
                return View("Error");
            }
            catch
            {
                return View();
            }
        }
    }
}
