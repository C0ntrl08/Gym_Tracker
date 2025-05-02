namespace GymTrackerApi.Pages.Admin
{
    public class AdminPanelModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Ensure that a JWT exists in session; if not, redirect to login.
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("Index");
            }
            return Page();
        }

        public IActionResult OnPostLogout()
        {
            // Remove the JWT token on logout.
            HttpContext.Session.Remove("JwtToken");
            return RedirectToPage("Index");
        }
    }
}
