using backend_api.Domain.Models;

namespace backend_api.Domain.Services.Communication
{
    public class UserResponse : BaseResponse
    {
        public User User { get; private set; }

        private UserResponse(bool success, string message, User user) : base(success, message)
        {
            User = user;
        }

        // Creates a success response
        public UserResponse(User user) : this(true, string.Empty, user)
        {
        }

        // Creates an error response
        public UserResponse(string message) : this(false, message, null)
        {
        }
    }
}