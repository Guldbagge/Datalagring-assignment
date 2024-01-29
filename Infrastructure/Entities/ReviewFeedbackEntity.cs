

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class ReviewFeedbackEntity
{
     
    [Key]
    public int FeedbackId { get; set; }
    public int UserId { get; set; }
    public string Feedback { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual UserEntity User { get; set; } = null!;
}

