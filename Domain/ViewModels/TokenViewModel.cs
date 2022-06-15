using TODOLIST.Domain.ViewModels;

namespace Domain.ViewModels
{

    public class TokenViewModel
    {
        public TokenViewModel(bool authenticated, string created, string expiration, string accessToken, UserViewModel userDto)
        {
            Authenticated = authenticated;
            Created = created;
            Expiration = expiration;
            AccessToken = accessToken;
            User = userDto;
        }
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public virtual UserViewModel User { get; set; }
    }
}
