using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Pages;

public class EnterQuestionAmountPage : PageModel
{
    private readonly AppDbContext _context;

    [BindProperty(SupportsGet = true)] 
    public string? Message { get; set; }

    [BindProperty(SupportsGet = true)]
    [Required(ErrorMessage = "Name is required")]
    [StringLength(128, ErrorMessage = "Name cannot be longer than 128 characters")]
    public string QuestionnaireName { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    [Range(0, 100, ErrorMessage = "Please enter a number between 0 and 100")]
    public int PollCount { get; set; }

    [BindProperty(SupportsGet = true)]
    [Range(0, 100, ErrorMessage = "Please enter a number between 0 and 100")]
    public int MultipleChoiceCount { get; set; }

    public EnterQuestionAmountPage(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (PollCount + MultipleChoiceCount != 0)
            return RedirectToPage("/CreateQuestionnaire", new
            {
                questionnaireName = QuestionnaireName,
                pollCount = PollCount,
                multipleChoiceCount = MultipleChoiceCount
            });
        Message = "Please add at least one question";
        return Page();
    }
}