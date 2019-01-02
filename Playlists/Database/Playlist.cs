using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Database
{
    [Table("playlists")]
    public class Playlist
    {
        [Key]
        public int PlaylistId { get; set; } 
        public string Name { get; set; }
    }
}