namespace DOTNETAPI.Dtos
{
    public partial class UserToAddDto
    {
        public string Firstname { get; set; } = "";
        public string Lastname { get; set; } = "";
        public string Email { get; set; } = "";

        public string Gender { get; set; } = "";

        public bool Active { get; set; }


    }
}