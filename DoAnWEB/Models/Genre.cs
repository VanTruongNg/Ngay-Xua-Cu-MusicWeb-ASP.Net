using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnWEB.Models
{
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Tên thể loại")]
        public int GenreId { get; set; }
        [Required]
        [DisplayName("Tên thể loại")]
        public string GenreName { get; set; }
        public List<Song> Songs { get; set; }
    }
}
