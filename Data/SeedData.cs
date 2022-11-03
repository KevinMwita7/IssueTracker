using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IssueTracker.Authorization;
using IssueTracker.Data;

static class SeedData {
    public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw) {
        using(var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>())) {
            // For sample purposes seed both with the same password.
            // Password is set with the following:
            // dotnet user-secrets set SeedUserPW <pw>
            // The admin user can do anything

            await EnsureRoles(serviceProvider);
            var adminID = await EnsureUser(serviceProvider, testUserPw, "admin");

            // SeedDB(context, adminID);
        }
    }

    private static async Task < string > EnsureUser(IServiceProvider serviceProvider,
        string testUserPw, string UserName) {
        var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

        if (userManager == null)
        {
           throw new Exception("userManager is null");
        }

        var user = await userManager.FindByNameAsync(UserName);
        if (user == null) {
            user = new ApplicationUser {
                UserName = UserName,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(user, testUserPw);
        }

        if (user == null) {
            throw new Exception("The password is probably not strong enough!");
        }

        await userManager.AddToRoleAsync(user, Constants.ROLES["AdministratorsRole"]);

        return user.Id;
    }

    // Ensure default roles such as Admin and ProjectManager exist
    private static async Task < IdentityResult[]> EnsureRoles(IServiceProvider serviceProvider) {
        var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

        if (roleManager == null) {
            throw new Exception("roleManager null");
        }

        var tasks = new List<Task<IdentityResult>>();

        foreach (string key in Constants.ROLES.Keys) {
            string role = Constants.ROLES[key];

            if (!await roleManager.RoleExistsAsync(role)) {
                tasks.Add(
                    roleManager.CreateAsync(new IdentityRole(role))
                );
            }
        }

        return (await Task.WhenAll(tasks));
    }

    /*public static void SeedDB(ApplicationDbContext context, string adminID)
        {
            if (context.Contact.Any())
            {
                return;   // DB has been seeded
            }

            context.Contact.AddRange(
                new Contact
                {
                    Name = "Debra Garcia",
                    Address = "1234 Main St",
                    City = "Redmond",
                    State = "WA",
                    Zip = "10999",
                    Email = "debra@example.com"
                },
                new Contact
                {
                    Name = "Thorsten Weinrich",
                    Address = "5678 1st Ave W",
                    City = "Redmond",
                    State = "WA",
                    Zip = "10999",
                    Email = "thorsten@example.com"
                },
             new Contact
             {
                 Name = "Yuhong Li",
                 Address = "9012 State st",
                 City = "Redmond",
                 State = "WA",
                 Zip = "10999",
                 Email = "yuhong@example.com"
             },
             new Contact
             {
                 Name = "Jon Orton",
                 Address = "3456 Maple St",
                 City = "Redmond",
                 State = "WA",
                 Zip = "10999",
                 Email = "jon@example.com"
             },
             new Contact
             {
                 Name = "Diliana Alexieva-Bosseva",
                 Address = "7890 2nd Ave E",
                 City = "Redmond",
                 State = "WA",
                 Zip = "10999",
                 Email = "diliana@example.com"
             }
             );
            context.SaveChanges();
        }
    }*/
}