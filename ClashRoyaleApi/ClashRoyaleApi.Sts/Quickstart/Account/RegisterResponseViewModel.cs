using ClashRoyaleApi.Sts.Models;

namespace ClashRoyaleApi.Sts.Quickstart.Account
{
    public class RegisterResponseViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public RegisterResponseViewModel(ApplicationUser user)
        {
            Id = user.Id;
            Name = user.UserName;
            Email = user.Email;
        }
    }
}
