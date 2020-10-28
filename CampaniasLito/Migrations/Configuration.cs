namespace CampaniasLito.Migrations
{
    using CampaniasLito.Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<CampaniasLitoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "CampaniasLito.Models.CampaniasLitoContext";
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CampaniasLitoContext context)
        {
        }
    }
}
