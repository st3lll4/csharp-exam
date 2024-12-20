using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace WebApp.Pages;

public class AnswerQuestions : PageModel
{
    private readonly AppDbContext _context;

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public int CurrentQuestionIndex { get; set; } = 0;
    
    public Questionnaire? Questionnaire { get; set; }
    public Question? CurrentQuestion { get; set; }
    
    [BindProperty]
    public int TotalQuestions { get; set; }
    
    [BindProperty]
    public int SelectedAnswerId { get; set; }

    public AnswerQuestions(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Questionnaire = await _context.Questionnaires
            .Include(q => q.Questions!)
            .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == Id);

        if (Questionnaire == null || !Questionnaire.Questions!.Any())
        {
            return NotFound();
        }

        TotalQuestions = Questionnaire.Questions!.Count;
        CurrentQuestion = Questionnaire.Questions.ElementAt(CurrentQuestionIndex);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var questionnaire = await _context.Questionnaires
            .Include(q => q.Questions!)
            .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == Id);

        if (questionnaire == null || !questionnaire.Questions!.Any())
        {
            return NotFound();
        }

        var currentQuestion = questionnaire.Questions!.ElementAt(CurrentQuestionIndex);

        var answer = await _context.Answers.FindAsync(SelectedAnswerId);
        if (answer != null)
        {
            answer.AnswerResponseCount++;
            currentQuestion.QuestionResponseCount++;
            await _context.SaveChangesAsync();
        }

        if (CurrentQuestionIndex < TotalQuestions - 1)
        {
            CurrentQuestionIndex++;
            return RedirectToPage("/AnswerQuestions", new 
            { 
                Id = Id,
                CurrentQuestionIndex = CurrentQuestionIndex
            });
        }

        questionnaire.QuestionnaireResponseCount++;
        await _context.SaveChangesAsync();

        TempData["Message"] = "Thank you for completing the questionnaire!";
        return RedirectToPage("/Index");
    }
}