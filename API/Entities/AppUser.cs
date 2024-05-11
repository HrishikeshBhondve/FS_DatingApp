namespace API.Entities;

public class AppUser
{
    public int Id { get; set; }
    public string UserName { get; set; }  //disable nullable flag to avoid string (required) error--api.csproj
}
