namespace TalkWave.User.Models.UserModels.Response {

    public class UserFullResponseModel {

        public Guid Id { get; set; }

        public string Login { get; set; }
        
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string? LastName { get; set; }

    }

}
