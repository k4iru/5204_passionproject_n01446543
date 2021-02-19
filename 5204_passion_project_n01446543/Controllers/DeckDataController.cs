using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Diagnostics;
using _5204_passion_project_n01446543.Models;

namespace _5204_passion_project_n01446543.Controllers
{
    public class DeckDataController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();
    

        [ResponseType(typeof(Deck))]
        [HttpPost]
        public IHttpActionResult AddDeck([FromBody] Deck Deck)
        {

            // invalid model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Decks.Add(Deck);
            db.SaveChanges();

            return Ok(Deck.DeckID);
        }

        /// <summary>
        /// Get a list of decks in the database alongside ok code (200)
        /// </summary>
        /// <returns>A list of decks</returns>
        /// GET: api/DeckData/GetDecks
        [ResponseType(typeof(IEnumerable<DeckDto>))]
        public IHttpActionResult GetDecks()
        {
            List<Deck> Decks = db.Decks.ToList();
            List<DeckDto> DeckDtos = new List<DeckDto> { };


            foreach (var deck in Decks)
            {
                DeckDto newDeck = new DeckDto
                {
                    DeckID = deck.DeckID,
                    DeckTitle = deck.DeckTitle
                };

                DeckDtos.Add(newDeck);

            }

            return Ok(DeckDtos);
        }

    }
}