using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Answer : BaseEntity
{
    [MaxLength(128)] public string AnswerTitle { get; set; } = default!;
    
    public bool IsCorrect { get; set; }  = false; 
    public int AnswerResponseCount { get; set; } = 0; 
    
    public int QuestionId { get; set; }
    public Question? Question { get; set; } 
}