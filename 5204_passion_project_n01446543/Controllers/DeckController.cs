using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _5204_passion_project_n01446543.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace _5204_passion_project_n01446543.Controllers
{
    public class DeckController : Controller
    {


        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;

        static DeckController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };
            client = new HttpClient(handler);
            //change this to match your own local port number

            //[44328]
            client.BaseAddress = new Uri("https://localhost:44328/api/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));



        }


        // GET: Deck/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Deck/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Deck deckInfo)
        {

            // send request to DeckDataController addDeck route
            string url = "DeckData/AddDeck";
            HttpContent content = new StringContent(jss.Serialize(deckInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {

                int Deckid = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Error");
                //TODO
                //RedirectToAction("Details", new { id = Deckid });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        // GET: Deck/List
        public ActionResult List()
        {
            string url = "DeckData/GetDecks";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {

                // get the Deck Data transfer object list as a result
                IEnumerable<DeckDto> Decks = response.Content.ReadAsAsync<IEnumerable<DeckDto>>().Result;

                // send the list to the view
                return View(Decks);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Error()
        {
            return View();
        }

        
    }
}
