using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Question : BaseEntity
{
    [MaxLength(500)] public string QuestionTitle { get; set; } = default!;
    public EQuestionType QuestionType { get; set; } 
    public int QuestionResponseCount { get; set; } = 0; 

    
    public int QuestionnaireId { get; set; }
    public Questionnaire? Questionnaire { get; set; }
    
    public ICollection<Answer>? Answers { get; set; }

}