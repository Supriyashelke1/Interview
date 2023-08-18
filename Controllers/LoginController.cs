using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using Interview.Data;


namespace Interview.Controllers;

public class LoginController : Controller
{
    private readonly InterviewDbContext _dbContext; // Replace with your DbContext

    public LoginController(InterviewDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    //[AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    //[AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLogin data)
    {
        if (ModelState.IsValid)
        {
            // Validate user credentials against the database
            var user = _dbContext.UserLogins.FirstOrDefault(u => u.Username == data.Username && u.Password == data.Password);
            if (user!= null)
            {

            //var user = _dbContext.UserLogins.FirstOrDefault(u => u.Username == data.Username);
            var role= _dbContext.Roles.FirstOrDefault(r=>r.Role1 ==user.Role);

            if (role != null)
            {
                // Create claims for the authenticated user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, role.Role1) // Role from the database
                };

                // Create claims identity
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Sign in the user
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                // Redirect to the appropriate dashboard based on the user's role
                //[Authorize]
                if (user.Role == "Asylum Seeker")
                {
                    return RedirectToAction("Appointment", "Details");
                }
                else if (user.Role == "Interviewer")
                {
                    return RedirectToAction("Home", "Index");
                }
            }
            else
            {
                //dataState.AdddataError("", "Invalid username or password");
            }
        }
        else{

        }
        }

        return View(data);
    }


[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> logout()
{
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    // Perform logout logic, e.g., sign out the user
    // Redirect to the home page or another appropriate page
    return RedirectToAction("Index", "Home");
}
}