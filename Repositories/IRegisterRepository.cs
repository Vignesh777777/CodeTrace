public interface IRegisterRepository{
    Task<User> CreateUser(User user);
    Task<User> GetUser(string email);
    Task<bool> SetUserRole(string email, string role);
}