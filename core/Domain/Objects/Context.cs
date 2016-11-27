namespace cm.frontend.core.Domain.Objects
{
    public class Context
    {
        public string Username { get; set; }

        public string SchoolName { get; set; }

        public bool IsAuthenticated { get; set; }

        public Domain.Objects.Token AccessToken { get; set; }
    }
}
