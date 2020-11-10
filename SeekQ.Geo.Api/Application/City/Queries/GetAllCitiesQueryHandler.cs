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
using SeekQ.Geo.Api.Application.City.ViewModel;

namespace SeekQ.Geo.Api.Application.City.Queries
{
    public class GetAllCitiesQueryHandler
    {
        public class Query : IRequest<IEnumerable<CityViewModel>>
        {
            public Query(string stateId)
            {
                StateId = stateId;
            }

            public string StateId { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<CityViewModel>>
        {
            private CommonGlobalAppSingleSettings _commonGlobalAppSingleSettings;

            public Handler(CommonGlobalAppSingleSettings commonGlobalAppSingleSettings)
            {
                _commonGlobalAppSingleSettings = commonGlobalAppSingleSettings;
            }

            public async Task<IEnumerable<CityViewModel>> Handle(
                Query query,
                CancellationToken cancellationToken)
            {
                try
                {
                    using (IDbConnection conn = new SqlConnection(_commonGlobalAppSingleSettings.MssqlConnectionString))
                    {
                        string sql =
                            @"
                        SELECT  CityId,
                                CityName
                        FROM Cities
                        WHERE StateId = @StateId";

                        var result = await conn.QueryAsync<CityViewModel>(sql, new { query.StateId });

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