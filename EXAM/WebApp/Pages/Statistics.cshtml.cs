using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class Statistics : PageModel
{
    private readonly AppDbContext _context;

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }
    
    public Questionnaire? Questionnaire { get; set; }
    
    public Statistics(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Questionnaire = await _context.Questionnaires
            .Include(q => q.Questions!)
            .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == Id);

        if (Questionnaire == null)
        {
            return NotFound();
        }

        return Page();
    }
}