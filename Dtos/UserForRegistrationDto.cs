namespace DOTNETAPI.Dtos
{
    public partial class UserForRegistrationDto
    {
        public string Firstname { get; set; } = "";
        public string Lastname { get; set; } = "";
        public string Email { get; set; } = "";

        public string Password { get; set; } = "";

        public string PasswordConfirm { get; set; } = "";

        public string Gender { get; set; } = "";

    }
}