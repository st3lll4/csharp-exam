using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class NameQuestionnaire : PageModel
{
    [BindProperty(SupportsGet = true)] public string QuestionnaireTitle { get; set; } = default!;
    [BindProperty(SupportsGet = true)] public string? Message { get; set; } 
    
    public void OnGet()
    {
        
    }
    
    public IActionResult OnGetNext()
    {
        if (string.IsNullOrWhiteSpace(QuestionnaireTitle))
        {
            Message = "Please enter something";
            return Page();
        }
        return RedirectToPage("/ConfigureQuestion", new { questionnaireTitle = QuestionnaireTitle });
    }
}