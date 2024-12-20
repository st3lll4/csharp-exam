using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class Question : PageModel
{
    private readonly AppDbContext _context;
    [BindProperty(SupportsGet = true)] public string QuestionnaireTitle { get; set; } = default!;

    public Question(AppDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        
    }
}