using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;
    
    [BindProperty(SupportsGet = true)] public string? Search { get; set; }
    public List<Questionnaire> Questionnaires { get; set; } = new();
    [BindProperty(SupportsGet = true)] public string? Message { get; set; }


    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        
    }
}