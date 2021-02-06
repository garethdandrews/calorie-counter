using Microsoft.AspNetCore.Mvc;

namespace backend_api.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IMapper mapper, IAuthenticationService authenticationService)
        {
            _mapper;
            _authenticationService = authenticationService;
        }
    }
}