namespace DatingAppApi.Entities
{
    public class UserLike
    {
        public AppUser SourceUser { get; set; }
        public int SourceUserID { get; set; }
        public AppUser LikedUser { get; set; }
        public int LikedUserID { get; set; }
    }
}