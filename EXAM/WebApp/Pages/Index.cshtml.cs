using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;
    
    [BindProperty(SupportsGet = true)] 
    public string? Search { get; set; }
    
    public List<Questionnaire> Questionnaires { get; set; } = new();
    
    [BindProperty(SupportsGet = true)] 
    public string? Message { get; set; }

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        IQueryable<Questionnaire> query = _context.Questionnaires
            .Include(q => q.Questions);

        if (!string.IsNullOrWhiteSpace(Search))
        {
            query = query.Where(q => 
                q.QuestionnaireName.ToLower().Contains(Search.ToLower()));
        }

        Questionnaires = await query.ToListAsync();
        
        return Page();
    }
}