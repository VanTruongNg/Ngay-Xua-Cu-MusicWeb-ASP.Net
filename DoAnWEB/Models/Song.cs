using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnWEB.Models
{
    public class Song
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Mã bài hát")]
        public int Id { get; set; }
        [Required]
        [DisplayName("Tên bài hát")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Tác giả")]
        public string Artist { get; set; }

        [DisplayName("Audio")]
        public string? mp3FilePath { get; set; }

        [DisplayName("Ảnh bìa")]
        public string? imgFilePath { get; set; }
        public int GenreID { get; set; }

        [DisplayName("Thể loại")]
        public Genre? Genre { get; set; }
    }
}
