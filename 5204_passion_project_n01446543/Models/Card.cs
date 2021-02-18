using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5204_passion_project_n01446543.Models
{
    public class Card
    {
        [Key]
        public int CardID { get; set; }

        public string CardQuestion { get; set; }

        public string CardAnswer { get; set; }


        [ForeignKey("Deck")]
        public int DeckID { get; set; }
        public virtual Deck Deck { get; set; }
    }

    public class CardDto
    {
        public int CardID { get; set; }

        [DisplayName("Card Question")]
        public string CardQuestion { get; set; }

        [DisplayName("Card Answer")]
        public string CardAnswer { get; set; }
    }
}