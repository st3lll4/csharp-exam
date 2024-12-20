using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Questionnaire : BaseEntity
{
    [MaxLength(128)] public string QuestionnaireName { get; set; } = default!;
    public int QuestionnaireResponseCount { get; set; } = 0; 
    
    public ICollection<Question>? Questions { get; set; }
}