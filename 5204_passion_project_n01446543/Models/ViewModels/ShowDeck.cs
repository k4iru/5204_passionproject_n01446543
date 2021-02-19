using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5204_passion_project_n01446543.Models.ViewModels
{
    public class ShowDeck
    {
        public DeckDto Deck { get; set; }

        // all the cards in the deck
        public IEnumerable<CardDto> deckCards { get; set; }
    }
}