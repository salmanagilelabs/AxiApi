using ARMCommon.Model;
using AxExtend.Interface;
using AxiApi.DTOs;
using AxiApi.Interfaces;
using System.Data;
using System.Threading.Tasks;

namespace AxiApi.Repositories
{
    public class UserFavouritesRepository: IUserFavouritesRepository
    {
        private readonly ILogger<GrammarRepository> _logger;
        private readonly IAxExtend _axExtend;

        public UserFavouritesRepository(IAxExtend axExtend, ILogger<GrammarRepository> logger)
        {
            //_connectionString = configuration.GetConnectionString("Postgres")!;

            _axExtend = axExtend;
            _logger = logger;
        }

        public async Task<NonQueryResult> CreateUserFavourites(UserFavouritesDTO userFavouritesDTO, string appname)
        {
            string sqlQuery = "INSERT INTO Axi_UserFavourites\r\n(username, commandtext, favorder, targeturl)\r\nVALUES\r\n(:username, :commandtext, :favorder, :targeturl);";

            string[] paramNames = { ":username", ":commandtext", ":favorder", ":targeturl" };
            DbType[] paramTypes = { DbType.String, DbType.String, DbType.Int64, DbType.String };
            object[] paramValues = { userFavouritesDTO.Username, userFavouritesDTO.CommandText, userFavouritesDTO.FavOrder, userFavouritesDTO.TargetURL };

            NonQueryResult sqlResult = new();
            

            var isDbConnected = await _axExtend.OpenDBConnectionAsync(appname);

            if (isDbConnected)
            {
                var db = await _axExtend.GetDB();

                sqlResult = await db.ExecuteNonQueryAsync(sqlQuery, paramNames, paramTypes, paramValues);

               
            }

            return sqlResult; 




        }

        public async Task<NonQueryResult> DeleteUserFavouritesByCmd(UserFavouritesDTO userFavouritesDTO, string appname)
        {
            string sqlQuery = "DELETE FROM Axi_UserFavourites\r\nWHERE\r\n    username = :username\r\n    AND commandtext = :commandtext;";

            string[] paramNames = { ":username", ":commandtext" };
            DbType[] paramTypes = { DbType.String, DbType.String };
            object[] paramValues = { userFavouritesDTO.Username, userFavouritesDTO.CommandText };

            NonQueryResult sqlResult = new();


            var isDbConnected = await _axExtend.OpenDBConnectionAsync(appname);

            if (isDbConnected)
            {
                var db = await _axExtend.GetDB();

                sqlResult = await db.ExecuteNonQueryAsync(sqlQuery, paramNames, paramTypes, paramValues);


            }

            return sqlResult;

        }

        public async Task<List<UserFavouritesDTO>> GetUserFavouritesByUsername(string username, string appname)
        {
            string sqlQuery = "SELECT * FROM Axi_UserFavourites WHERE username = :username ORDER BY createdon DESC";

            string[] paramNames = { ":username" };
            DbType[] paramTypes = { DbType.String };
            object[] paramValues = { username };

            SQLResult sqlResult = new();
            List<UserFavouritesDTO> favouritesDTOs = new(); 

            var isDbConnected = await _axExtend.OpenDBConnectionAsync(appname); 

            if (isDbConnected)
            {
                var db = await _axExtend.GetDB();



                sqlResult = await db.ExecuteSQLAsync(sqlQuery, paramNames, paramTypes, paramValues);
            }


            if (sqlResult != null && sqlResult.data != null && sqlResult.data.Rows.Count > 0)
            {
                foreach (DataRow row in sqlResult.data.Rows)
                {
                    var fav = new UserFavouritesDTO
                    {
                        FavouritesId = row["id"] is DBNull ? Guid.Empty : (Guid)row["id"],
                        Username = row["username"]?.ToString(),
                        CommandText = row["commandtext"]?.ToString(),
                        FavOrder = row["favorder"] is DBNull ? 0 : Convert.ToInt32(row["favorder"]),
                        TargetURL  = row["targeturl"]?.ToString(),
                        CreatedOn = row["createdon"] is DBNull ? DateTime.MinValue : (DateTime)row["createdon"],
                        
                    };

                    favouritesDTOs.Add(fav);
                }
            }

            return favouritesDTOs; 



        }
    }
}
