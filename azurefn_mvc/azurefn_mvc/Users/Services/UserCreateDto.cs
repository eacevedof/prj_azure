
namespace Fn.Users.Services
{
    public sealed class UserCreateDto
    {
        private string _search;

        public UserCreateDto(string search)
        {
            _search = search;
        }

        public static UserCreateDto FromPrimitives(string searchText)
        {
            return new UserCreateDto(searchText);
        }

        public string Search
        {
            get { return _search; }
        }
    }
}