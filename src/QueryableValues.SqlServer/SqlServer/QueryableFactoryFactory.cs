﻿#if EFCORE
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazarTech.QueryableValues.SqlServer
{
    internal sealed class QueryableFactoryFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly QueryableValuesSqlServerOptions _options;

        public QueryableFactoryFactory(IServiceProvider serviceProvider, IDbContextOptions dbContextOptions)
        {
            _serviceProvider = serviceProvider;
            _options = (dbContextOptions.FindExtension<QueryableValuesSqlServerExtension>()?.Options) ?? throw new InvalidOperationException();
        }

        public IQueryableFactory Create()
        {
            if (_options.WithSerializationOptions == SerializationOptions.UseJson)
            {
                return _serviceProvider.GetRequiredService<JsonQueryableFactory>();
            }
            else
            {
                return _serviceProvider.GetRequiredService<XmlQueryableFactory>();
            }
        }
    }
}
#endif
