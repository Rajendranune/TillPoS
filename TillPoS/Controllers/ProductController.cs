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
    public class ProductController : Controller
    {
        string url = "http://webapi20170117015441.azurewebsites.net/api/Product";
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

                    var SubCategory = JsonConvert.DeserializeObject<List<ProductModel>>(responseData);
                    return View(SubCategory);
                }
            }
            return View("Error");
        }

        // GET: Category/Create
        public async Task<ActionResult> Create()
        {
            ProductModel s = new ProductModel();
            string url = "http://webapi20170117015441.azurewebsites.net/api/SubCategory";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage response = await client.GetAsync(url + "/get");
                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    List<Dropdownlist> l = new List<Dropdownlist>();
                    var SubCategory = JsonConvert.DeserializeObject<List<SubCategoryModel>>(responseData);
                    // m.dep = Category;
                    for (int i = 0; i < SubCategory.Count; i++)
                    {
                        Dropdownlist d = new Dropdownlist();

                        d.Categoryid = SubCategory[i].Id;
                        d.Name = SubCategory[i].Name;
                        l.Add(d);
                    }
                    // s.dep = l;
                    ViewBag.Drpdwn = new SelectList(l, "Categoryid", "Name"); ;
                }
            }
            return View(s);
        }

        // POST: Category/Create
        [HttpPost]
        public async Task<ActionResult> Create(ProductModel collection)
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

            }
            return View();
        }

        // GET: Category/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url1 = "http://webapi20170117015441.azurewebsites.net/api/SubCategory";
                List<Dropdownlist1> l = new List<Dropdownlist1>();
                HttpResponseMessage response = await client.GetAsync(url1 + "/get");
                if (response.IsSuccessStatusCode)
                {
                    var responseData1 = response.Content.ReadAsStringAsync().Result;

                    var SubCategory = JsonConvert.DeserializeObject<List<SubCategoryModel>>(responseData1);
                    for (int i = 0; i < SubCategory.Count; i++)
                    {
                        Dropdownlist1 d = new Dropdownlist1();

                        d.SubCategoryid = SubCategory[i].Id;
                        d.Name = SubCategory[i].Name;
                        l.Add(d);
                    }
                    ViewBag.Drpdwn = new SelectList(l, "SubCategoryid", "Name"); ;
                }

                HttpResponseMessage responseMessage = await client.GetAsync(url + "/get/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    var Product = JsonConvert.DeserializeObject<ProductModel>(responseData);

                    return View(Product);
                }
            }
            return View("Error");
        }

        //POST: Category/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(string id, ProductModel m)
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
            catch (Exception Ex)
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

                    var Product = JsonConvert.DeserializeObject<ProductModel>(responseData);

                    return View(Product);
                }
            }
            return View("Error");
        }

        // POST: Category/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(string id, ProductModel collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/Delete/" + id);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }



                }
                return RedirectToAction("Error");
            }
            catch (Exception Ex)
            {
                return View();
            }
        }
    }
}