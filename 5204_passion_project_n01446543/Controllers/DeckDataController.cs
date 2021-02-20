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
    

        /// <summary>
        /// Adds a new Deck to the database
        /// </summary>
        /// <param name="Deck">Deck as a JSON object bind to modelstate</param>
        /// <returns>Ok status and the deckID if successful, otherwise a BadRequest object</returns>
        [ResponseType(typeof(Deck))]
        [HttpPost]
        public IHttpActionResult AddDeck([FromBody] Deck Deck)
        {

            // invalid model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // add and save the changes
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
            // get the list of decks from the database
            List<Deck> Decks = db.Decks.ToList();

            // create an empty Deck data transfer object
            List<DeckDto> DeckDtos = new List<DeckDto> { };
            Debug.WriteLine("In GetDecks");


            // for each deck create a new DeckDto and push it to the list of DeckDtos.
            foreach (var deck in Decks)
            {
                DeckDto newDeck = new DeckDto
                {
                    DeckID = deck.DeckID,
                    DeckTitle = deck.DeckTitle
                };

                DeckDtos.Add(newDeck);

            }
            Debug.WriteLine(DeckDtos);

            // send back the DeckDtos
            return Ok(DeckDtos);
        }

        /// <summary>
        /// Finds a deck based on the deckID
        /// </summary>
        /// <param name="id">DeckID</param>
        /// <returns>returns a DeckDto object if found, otherwise NotFound object</returns>
        [HttpGet]
        [ResponseType(typeof(DeckDto))]
        public IHttpActionResult FindDeck(int id)
        {

            // find the deck in the database
            Deck deck = db.Decks.Find(id);

            // if deck isnt found
            if (deck == null)
            {
                return NotFound();
            }

            // create a data transfer object to send back
            DeckDto DeckDto = new DeckDto
            {
                DeckID = deck.DeckID,
                DeckTitle = deck.DeckTitle
            };
            return Ok(DeckDto);
        }

        /// <summary>
        /// gets all the cards associated with a specific deck
        /// </summary>
        /// <param name="id">DeckID</param>
        /// <returns>a list of CardDto objects associated with the specific deck</returns>
        [ResponseType(typeof(IEnumerable<CardDto>))]
        public IHttpActionResult GetCardsForDeck(int id)
        {
            // create a list of cards that belong to the specified deck
            List<Card> cards = db.Cards.Where(c => c.DeckID == id).ToList();

            // empty cardDto list
            List<CardDto> cardDtos = new List<CardDto> { };


            // for each card that belongs to the deck, create a new CardDto and push it to the list
            foreach(var card in cards)
            {
                CardDto newCard = new CardDto
                {
                    CardID = card.CardID,
                    CardAnswer = card.CardAnswer,
                    CardQuestion = card.CardQuestion,
                    DeckID = card.DeckID
                };
                cardDtos.Add(newCard);
            }

            return Ok(cardDtos);
        }

    }
}