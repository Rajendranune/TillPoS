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
using System.Net.Http.Formatting;
using System.Text;

namespace TillPoS.Controllers
{
    public class CategoryController : Controller
    {


        string url = "http://webapi20170117015441.azurewebsites.net/api/Category";
        // GET: Category
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage response = await client.GetAsync(url+"/get");
                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;

                    var Category = JsonConvert.DeserializeObject<List<CategoryModel>>(responseData);
                    return View(Category);
                }
            }
            return View("Error");
        }

       // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

       // POST: Category/Create
       [HttpPost]
        public async Task<ActionResult> Create(CategoryModel collection)
        {
            try
            {
                collection.Id = Guid.NewGuid().ToString();
                HttpClient client = new HttpClient();
                var content = JsonConvert.SerializeObject(collection);

                HttpResponseMessage response = await client.PostAsJsonAsync(url + "/Post/", collection);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await client.GetAsync(url + "/get/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    var Category = JsonConvert.DeserializeObject<CategoryModel>(responseData);

                    return View(Category);
                }
            }
            return View("Error");
        }

        //POST: Category/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(string id, CategoryModel m)
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
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
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

                    var Employee = JsonConvert.DeserializeObject<CategoryModel>(responseData);
                    ViewBag.messag = Convert.ToString(TempData["message"]);
                    return View(Employee);
                }
            }
            return View("Error");
        }

        // POST: Category/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(string id, CategoryModel collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage responseMessage = await client.GetAsync("http://webapi20170117015441.azurewebsites.net/api/validation/get/"  + collection.Id);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        TempData["message"] = "u can't delete";
                        return RedirectToAction("Delete","Category",id);
                    }
                    else
                    {
                        responseMessage = await client.DeleteAsync(url + "/Delete/" + id);
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }

                        }
                        
                }
                return RedirectToAction("Error");
            }
            catch
            {
                return View();
            }
        }
    }
}
