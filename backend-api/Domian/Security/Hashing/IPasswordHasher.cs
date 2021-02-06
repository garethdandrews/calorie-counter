namespace backend_api.Domain.Security.Hashing
{
    /// <summary>
    /// This password hasher is the same used by ASP.NET Identity.
    /// Explanation: https://stackoverflow.com/questions/20621950/asp-net-identity-default-password-hasher-how-does-it-work-and-is-it-secure
    /// Full implementation: https://gist.github.com/malkafly/e873228cb9515010bdbe
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hashes a password
        /// </summary>
        /// <param name="password"></param>
        /// <returns>
        /// The hashed password
        /// </returns>
        string HashPassword(string password);

        /// <summary>
        /// Checks if the provided password matches the hashed password
        /// </summary>
        /// <param name="providedPassword"></param>
        /// <param name="passwordHash"></param>
        /// <returns>
        /// boolean
        /// </returns>
        bool PasswordMatches(string providedPassword, string passwordHash);
    }
}