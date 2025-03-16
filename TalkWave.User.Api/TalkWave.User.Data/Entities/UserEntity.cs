namespace TalkWave.User.Data.Entities {

    public class UserEntity : BaseEntity {

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string? LastName { get; set; }

    }

}
