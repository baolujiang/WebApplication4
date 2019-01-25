using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MusicStore.Models
{
    public class MusicStoreDataContextInitializer : DropCreateDatabaseIfModelChanges<MusicStoreDataContext>
    {
        protected override void Seed(MusicStoreDataContext context)
        {

            //var first = new Artist { Name = "First Artist" };
            //context.Artists.Add(first);

            //context.Albums.AddRange( new[]
            //        {   new Album { Title = "First Album", Rating=1, Artist=first },
            //            new Album { Title ="Second Album", Rating=2, Artist=first }
            //        }
            //    );

            //context.Albums.Add(
            //    new Album { Title="Third Album", Rating=3,
            //                Artist=new Artist { Name="Second Artist"}
            //    }
            //    );

            //context.Artists.Add(new SoloArtist() { Name = "Solo Artist", Instrument = "Piano" });
            //context.SaveChanges();
        }
    }
}