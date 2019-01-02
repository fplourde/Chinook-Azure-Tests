using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Albums.Database
{
    [Table("albums")]
    public class Album
    {
        [Key]
        public int AlbumId { get; set; } 
        public string Title { get; set; }
        public int ArtistId { get; set; }
    }
}