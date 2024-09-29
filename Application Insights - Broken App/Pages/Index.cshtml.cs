using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

public class IndexModel : PageModel
{
    public void OnGet()
    {
    }

    // Trigger NullReferenceException
    public IActionResult OnGetNullReference()
    {
        string? nullString = null;
        var length = nullString.Length;  
        return Page();
    }

    // Trigger DivideByZeroException
    public IActionResult OnGetDivideByZero()
    {
        int zero = 0;
        var result = 1 / zero;  
        return Page();
    }

    // Trigger a Custom Exception
    public IActionResult OnGetCustomException()
    {
        throw new InvalidOperationException("This is a custom exception for demo purposes.");
    }

    // Simulate a broken database connection
    public IActionResult OnGetDatabaseError()
    {
        string connectionString = "Server=tcp:brokenserver.database.windows.net;Database=NonExistentDB;User Id=baduser;Password=badpassword;";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); 
            }
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException("Database connection failed.", ex);
        }

        return Page();
    }
}
