using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GymManagementSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritaban� Ba�lant�s� (SQL Server)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
           .ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning))); // Buraya dikkat

// 2. Identity Servisleri (Rol ve �ifre Politikalar�)
builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
.AddRoles<IdentityRole>() // Rol y�netimini aktif ettik
.AddEntityFrameworkStores<ApplicationDbContext>();

// 3. MVC ve Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// 4. AI Servisi (Varsa - Daha sonra i�eri�ini yazaca��z)
// builder.Services.AddScoped<GeminiApiService>(); 

var app = builder.Build();

// 5. Otomatik Migration (Uygulama kalkarken veritaban�n� g�nceller)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    // E�er veritaban� yoksa olu�turur ve bekleyen migrationlar� uygular
    context.Database.Migrate();
}

// 6. HTTP Pipeline (Middleware) Ayarlar�
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// SIRA �OK �NEML�: Kimlik do�rulama her zaman yetkilendirmeden �NCE gelmelidir.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Identity sayfalar� i�in gerekli

app.Run();