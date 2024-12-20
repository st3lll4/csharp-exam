using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class ConfigureQuestion : PageModel
{
    private readonly AppDbContext _context;
    [BindProperty(SupportsGet = true)] public string QuestionnaireTitle { get; set; } = default!;
    public List<ConfigureQuestion> Questions { get; set; } = default!;

    [BindProperty(SupportsGet = true)] public string Name { get; set; } = default!;
    [BindProperty(SupportsGet = true)] public EQuestionType Type { get; set; } = default!;
    [BindProperty(SupportsGet = true)] public string OptionAmount { get; set; } = default!;

    public SelectList TypeSelectList { get; set; } = default!;

    public ConfigureQuestion(AppDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        var selectListData = new List<EQuestionType> 
        { 
            EQuestionType.Poll, 
            EQuestionType.MultipleChoice 
        };

        TypeSelectList = new SelectList(selectListData);
    }

    public IActionResult OnGetNext()
    {
        return RedirectToPage("SaveQuestion", new
        {
            questionnaireTitle = QuestionnaireTitle,
            type = Type,
            options = OptionAmount
        });
    }
}