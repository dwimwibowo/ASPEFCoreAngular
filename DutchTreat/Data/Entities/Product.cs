using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR(MAX)")]
        public string Category { get; set; }
        [Column(TypeName = "VARCHAR(MAX)")]
        public string Size { get; set; }
        [Column(TypeName = "NUMERIC(18,4)")]
        public decimal Price { get; set; }
        [Column(TypeName = "VARCHAR(MAX)")]
        public string Title { get; set; }
        [Column(TypeName = "VARCHAR(MAX)")]
        public string ArtDescription { get; set; }
        [Column(TypeName = "VARCHAR(MAX)")]
        public string ArtDating { get; set; }
        [Column(TypeName = "VARCHAR(MAX)")]
        public string ArtId { get; set; }
        [Column(TypeName = "VARCHAR(MAX)")]
        public string Artist { get; set; }
        public DateTime ArtistBirthDate { get; set; }
        public DateTime ArtistDeathDate { get; set; }
        [Column(TypeName = "VARCHAR(MAX)")]
        public string ArtistNationality { get; set; }
    }
}
