using ARMCommon.Model;
using AxiApi.DTOs;

namespace AxiApi.Interfaces
{
    public interface IUserFavouritesRepository
    {
         Task<List<UserFavouritesDTO>> GetUserFavouritesByUsername(string username, string appname);
         Task<NonQueryResult> CreateUserFavourites(UserFavouritesDTO userFavouritesDTO, string appname);
         Task<NonQueryResult> DeleteUserFavouritesByCmd(UserFavouritesDTO userFavouritesDTO, string appname);

        Task<NonQueryResult> UpdateUserFavourties(Guid favouritesId, string appname, string commandText); 
    }
}
