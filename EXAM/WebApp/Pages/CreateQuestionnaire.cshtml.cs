using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class CreateQuestionnaire : PageModel
{
    private readonly AppDbContext _context;
    
    [BindProperty(SupportsGet = true)] public string? Message { get; set; }


    public CreateQuestionnaire(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> OnPost()
    {
        return Page();
    }
}