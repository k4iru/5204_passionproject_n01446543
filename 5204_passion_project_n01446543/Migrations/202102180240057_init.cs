namespace _5204_passion_project_n01446543.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        CardID = c.Int(nullable: false, identity: true),
                        CardQuestion = c.String(),
                        CardAnswer = c.String(),
                        DeckID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CardID)
                .ForeignKey("dbo.Decks", t => t.DeckID, cascadeDelete: true)
                .Index(t => t.DeckID);
            
            CreateTable(
                "dbo.Decks",
                c => new
                    {
                        DeckID = c.Int(nullable: false, identity: true),
                        DeckTitle = c.String(),
                    })
                .PrimaryKey(t => t.DeckID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "DeckID", "dbo.Decks");
            DropIndex("dbo.Cards", new[] { "DeckID" });
            DropTable("dbo.Decks");
            DropTable("dbo.Cards");
        }
    }
}
