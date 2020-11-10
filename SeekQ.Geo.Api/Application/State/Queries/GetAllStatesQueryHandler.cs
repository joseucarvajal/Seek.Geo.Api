using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Common.Exceptions;
using App.Common.SeedWork;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using SeekQ.Geo.Api.Application.State.ViewModel;

namespace SeekQ.Geo.Api.Application.State.Queries
{
    public class GetAllStatesQueryHandler
    {
        public class Query : IRequest<IEnumerable<StateViewModel>>
        {

        }

        public class Handler : IRequestHandler<Query, IEnumerable<StateViewModel>>
        {
            private CommonGlobalAppSingleSettings _commonGlobalAppSingleSettings;

            public Handler(CommonGlobalAppSingleSettings commonGlobalAppSingleSettings)
            {
                _commonGlobalAppSingleSettings = commonGlobalAppSingleSettings;
            }

            public async Task<IEnumerable<StateViewModel>> Handle(
                Query query,
                CancellationToken cancellationToken)
            {
                try
                {
                    using (IDbConnection conn = new SqlConnection(_commonGlobalAppSingleSettings.MssqlConnectionString))
                    {
                        string sql =
                            @"
                        SELECT  StateId,
                                StateName
                        FROM States";

                        var result = await conn.QueryAsync<StateViewModel>(sql);

                        return result.AsEnumerable();
                    }
                }
                catch (Exception e)
                {
                    throw new AppException(e.Message);
                }
            }
        }
    }
}