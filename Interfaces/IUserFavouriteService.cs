using ARMCommon.Model;
using AxiApi.DTOs;

namespace AxiApi.Interfaces
{
    public interface IUserFavouriteService
    {
        Task<List<UserFavouritesDTO>> GetUserFavouritesByUsernameAsync(string username, string appname);
        Task<NonQueryResult> ToggleUserFavouritesAsync(UserFavouritesRequestDTO userFavouritesRequestDTO,  string appname); 
        
    }
}
