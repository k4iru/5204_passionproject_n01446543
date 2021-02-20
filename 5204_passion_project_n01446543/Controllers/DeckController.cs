using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _5204_passion_project_n01446543.Models;
using _5204_passion_project_n01446543.Models.ViewModels;
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
                Debug.WriteLine("Deck ID: " + Deckid);
                //TODO
                return RedirectToAction("Edit", new { id = Deckid });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        // GET: Deck/List
        /// <summary>
        /// Get a list of all Decks
        /// </summary>
        /// <returns>returns a list of Decks</returns>
        public ActionResult List()
        {
            // api string
            string url = "DeckData/GetDecks";
            
            //http request to the url
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {

                // read the result.
                IEnumerable<DeckDto> Decks = response.Content.ReadAsAsync<IEnumerable<DeckDto>>().Result;

                // send the list to the view
                return View(Decks);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // Get: Deck/Edit/5
        // breaks server currently server 500 error
        /// <summary>
        /// Get Details about a specific Deck. Should go to a view 
        /// which you can manage cards associated with the deck
        /// </summary>
        /// <param name="id">DeckID</param>
        /// <returns>Return the view from which you can edit the Deck details and cards</returns>
        public ActionResult Details(int id)
        {
            ShowDeck ViewModel = new ShowDeck();
            string url = "DeckData/FindDeck/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                DeckDto deck = response.Content.ReadAsAsync<DeckDto>().Result;
                ViewModel.Deck = deck;

                url = "DeckData/GetCardsForDeck/" + id;
                response = client.GetAsync(url).Result;
                IEnumerable<CardDto> cards = response.Content.ReadAsAsync<IEnumerable<CardDto>>().Result;
                ViewModel.deckCards = cards;

                return View(ViewModel);

            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        // error view
        public ActionResult Error()
        {
            return View();
        }

    }
}
