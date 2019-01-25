namespace MusicStore.Migrations
{
    using MusicStore.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MusicStore.Models.MusicStoreDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MusicStore.Models.MusicStoreDataContext context)
        {

            var first = new Artist { Name = "First Artist" };
            context.Artists.Add(first);

            context.Albums.AddRange(new[]
                    {   new Album { Title = "First Album", Artist=first },
                        new Album { Title ="Second Album", Artist=first }
                    }
                );

            context.Albums.Add(
                new Album
                {
                    Title = "Third Album",
                    Artist = new Artist { Name = "Second Artist" }
                }
                );

            context.Artists.Add(new SoloArtist() { Name = "Solo Artist", Instrument = "Piano" });
            context.SaveChanges();
            
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
