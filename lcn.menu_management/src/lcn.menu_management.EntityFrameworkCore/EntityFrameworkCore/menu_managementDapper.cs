using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Dapper;
using Dapper;

namespace lcn.menu_management.EntityFrameworkCore
{
    public class menu_managementDapper : DapperRepository<menu_managementDbContext>, Imenu_managementDapper
    {
        public menu_managementDapper(IDbContextProvider<menu_managementDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<IEnumerable<T>> Query<T>(string sql, object param = null)
        {
            return await (await GetDbConnectionAsync()).QueryAsync<T>(sql, param);
        }

        public async Task<T> QueryFirstOrDefault<T>(string sql, object param = null)
        {
            return await (await GetDbConnectionAsync()).QueryFirstOrDefaultAsync<T>(sql, param);
        }
    }
}
