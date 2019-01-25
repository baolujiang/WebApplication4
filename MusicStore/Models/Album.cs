using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MusicStore.Models
{
    public class Album
    {
        public Guid AlbumID { get; set; }

        public string Title { get; set; }

        public int ArtistID { get; set; }
        public virtual Artist Artist { get; set; }

        public virtual List<Reviewer> Reviewers { get; set; }

        public int Rating { get; set; }

    }
}