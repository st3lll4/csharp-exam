using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class SaveQuestion : PageModel
{
    private readonly AppDbContext _context;
    [BindProperty(SupportsGet = true)] public string QuestionnaireTitle { get; set; } = default!;
    [BindProperty(SupportsGet = true)] public string QuestionTitle { get; set; } = default!;
    [BindProperty(SupportsGet = true)] public EQuestionType Type { get; set; }
    [BindProperty(SupportsGet = true)] public string OptionAmount { get; set; } = default!;

    [BindProperty] public List<string> AnswerTexts { get; set; } = new();
    [BindProperty] public int CorrectAnswerIndex { get; set; }

    [TempData] public string? Message { get; set; }

    public int NumberOfOptions => int.TryParse(OptionAmount, out int result) ? result : 4;

    public bool IsMultipleChoice => Type == EQuestionType.MultipleChoice;

    public SaveQuestion(AppDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        if (NumberOfOptions > 0)
        {
            AnswerTexts = Enumerable.Repeat(string.Empty, NumberOfOptions).ToList();
        }
    }

    public async Task<IActionResult> OnPostAsync(string? action)
    {
        if (!ModelState.IsValid) return Page();

        var questionnaire = await _context.Questionnaires
                                .FirstOrDefaultAsync(q => q.QuestionnaireName == QuestionnaireTitle)
                            ?? new Questionnaire
                            {
                                QuestionnaireName = QuestionnaireTitle,
                                QuestionnaireResponseCount = 0
                            };

        if (questionnaire.Id == 0)
        {
            await _context.Questionnaires.AddAsync(questionnaire);
            await _context.SaveChangesAsync();
        }

        var question = new Question
        {
            QuestionTitle = QuestionTitle,
            QuestionType = Type,
            QuestionResponseCount = 0,
            QuestionnaireId = questionnaire.Id
        };
        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();

        for (var i = 0; i < AnswerTexts.Count; i++)
        {
            var answerText = AnswerTexts[i];
            if (!string.IsNullOrWhiteSpace(answerText))
            {
                var answer = new Answer
                {
                    AnswerTitle = answerText,
                    AnswerResponseCount = 0,
                    QuestionId = question.Id,
                    IsCorrect = IsMultipleChoice && i == CorrectAnswerIndex
                };
                await _context.Answers.AddAsync(answer);
            }
        }

        await _context.SaveChangesAsync();

        if (action == "addmore")
        {
            return RedirectToPage("/ConfigureQuestion", new { questionnaireTitle = QuestionnaireTitle });
        }

        Message = "Questionnaire saved successfully!";
        return RedirectToPage("/Index", new { message = Message });
    }
}