
using Microsoft.EntityFrameworkCore;

public class UserContext : PostGresContext
{
    public UserContext(IConfiguration configuration) : base(configuration)
    {
    }

    public List<User> GetUsers()
    {
        //  _logger.LogInformation("Getting Users");
        List<User> Users = User
        .Where(p => p.Mail != null)
        .ToList();

        return Users;
    }
    //fix update -> https://stackoverflow.com/questions/48202403/instance-of-entity-type-cannot-be-tracked-because-another-instance-with-same-key
    public User GetUserById(int id)
    {
        User user = User.AsNoTracking()
        .Where(p => p.Mail != null)
        .Where((User) => User.Id == id)
        .AsEnumerable()
        .First();

        return user;
    }

    public User GetUserByEmail(string mail)
    {
        User user = User.AsNoTracking()
        .Where(p => p.Mail != null)
        .Where((User) => User.Mail == mail)
        .AsEnumerable()
        .First();

        return user;
    }


    public async Task CreateUser(User user)
    {
        await User.AddAsync(user);
        await SaveChangesAsync();
    }


    public async Task UpdateUser(User user)
    {
        User.Update(user);
        await SaveChangesAsync();
    }




    public async Task DeleteUser(User user)
    {
        User.Remove(user);
        await SaveChangesAsync();
    }

    public async Task DeleteUserById(int id)
    {
        User UserToDelete = new User() { Id = id };
        Entry(UserToDelete).State = EntityState.Deleted;
        await SaveChangesAsync();
    }

    public async Task DeleteUserByEmail(string mail)
    {
        User? UserToDelete = await User.FirstOrDefaultAsync(user => user.Mail == mail);

        if (UserToDelete != null)
        {

            User.Remove(UserToDelete);
            await SaveChangesAsync();
        }
        // await User
        // .Where(u => u.Mail == mail)
        // .ExecuteDeleteAsync();
    }
}