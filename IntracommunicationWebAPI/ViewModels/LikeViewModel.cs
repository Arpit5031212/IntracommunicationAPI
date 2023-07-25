using System.ComponentModel.DataAnnotations;

namespace IntraCommunicationWebApi.ViewModels
{
    public class LikeViewModel
    {
        [Required]
        public int postId { get; set; }
        [Required]
        public int userId { get; set; }
    }
}
