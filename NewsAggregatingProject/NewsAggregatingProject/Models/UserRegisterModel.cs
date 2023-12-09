namespace NewsAggregatingProject.Models
{
    public class UserRegisterModel
    {
        public string Email {  get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation {  get; set; }
        public int Age { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
    }
}
