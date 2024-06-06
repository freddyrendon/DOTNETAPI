namespace DOTNETAPI.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Firstname { get; set; } = "";
        public string Lastname { get; set; } = "";
        public string Email { get; set; } ="";

        public string Gender { get; set; } ="";

        public bool Active { get; set; }

         
    }
}