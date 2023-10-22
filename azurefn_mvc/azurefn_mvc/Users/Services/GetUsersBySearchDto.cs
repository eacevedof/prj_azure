
namespace Fn.Users.Services
{

    public sealed class GetUsersBySearchDto
    {
        private string _search;

        public GetUsersBySearchDto(string search)
        {
            _search = search;
        }

        public static GetUsersBySearchDto FromPrimitives(string searchText)
        {
            return new GetUsersBySearchDto(searchText);
        }

        public string Search
        {
            get { return _search; }
        }

    }
}