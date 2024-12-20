using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class EnterQuestionAmountPage : PageModel
{
    private readonly AppDbContext _context;

    [BindProperty(SupportsGet = true)] public string? Message { get; set; }

    [BindProperty(SupportsGet = true)] public string QuestionnaireName { get; set; } = default!;
    [BindProperty(SupportsGet = true)] public int PollCount { get; set; }
    [BindProperty(SupportsGet = true)] public int MultipleChoiceCount { get; set; }

    public EnterQuestionAmountPage(AppDbContext context)
    {
        _context = context;
    }

    public void OnPost()
    {
        
    }
}