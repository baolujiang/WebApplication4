using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MusicStore.Models
{
    [Table("Reviewer")]
    public class Reviewer
    {
        public int ReviewerID { get; set; }
        public string Name { get; set; }

        public virtual List<Album> Albums { get; set; }
    }
}