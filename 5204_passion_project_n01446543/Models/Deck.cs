using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5204_passion_project_n01446543.Models
{
    public class Deck
    {
        [Key]
        public int DeckID { get; set; }

        public string DeckTitle { get; set; }
        

        // each deck is a collection of cards
        public ICollection<Card> Cards { get; set; }
    }

    public class DeckDto
    {
        public int DeckID { get; set; }
        public string DeckTitle { get; set; }
    }
}