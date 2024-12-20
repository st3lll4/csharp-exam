using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class NameQuestionnaire : PageModel
{
    [BindProperty(SupportsGet = true)] public string QuestionnaireName { get; set; } = default!;
    
    public void OnGet()
    {
        
    }
    
    public IActionResult OnPost()
    {
        return RedirectToPage("/Question", new { name = QuestionnaireName });
    }
}