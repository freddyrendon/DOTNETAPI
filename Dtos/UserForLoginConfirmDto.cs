namespace DOTNETAPI.Dtos
{
    public partial class UserForLoginConfirmDto
    {
        public byte[] PasswordHash { get; set; } = new byte[0];

        public byte[] PasswordSalt { get; set; } = new byte[0];
    }
}