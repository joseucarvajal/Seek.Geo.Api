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
using SeekQ.Geo.Api.Application.Location.ViewModel;

namespace SeekQ.Geo.Api.Application.Profile.Queries
{
    public class GetProfileLocationQueryHandler
    {
        public class Query : IRequest<IEnumerable<ProfileLocationViewModel>>
        {
            public Query(Guid userId)
            {
                UserId = userId;
            }

            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<ProfileLocationViewModel>>
        {
            private CommonGlobalAppSingleSettings _commonGlobalAppSingleSettings;

            public Handler(CommonGlobalAppSingleSettings commonGlobalAppSingleSettings)
            {
                _commonGlobalAppSingleSettings = commonGlobalAppSingleSettings;
            }

            public async Task<IEnumerable<ProfileLocationViewModel>> Handle(
                Query query,
                CancellationToken cancellationToken)
            {
                try
                {
                    using (IDbConnection conn = new SqlConnection(_commonGlobalAppSingleSettings.MssqlConnectionString))
                    {
                        string sql =
                            @"
                        SELECT  pl.UserId,
                                pl.Latitud,
                                pl.Longitud,
                                pl.ZipCode,
                                s.StateId,
                                s.StateName,
                                c.CityId,
                                c.CityName
                        FROM ProfileLocations pl
                            INNER JOIN cities c ON c.CityId = pl.CityId
                            INNER JOIN states s ON s.StateId = c.StateId
                        WHERE UserId = @UserId";

                        var result = await conn.QueryAsync<ProfileLocationViewModel>(sql, new { query.UserId });

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
