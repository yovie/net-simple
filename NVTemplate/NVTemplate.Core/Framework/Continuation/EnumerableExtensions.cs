﻿using System.Linq;
using NVTemplate.Core.Framework.Continuation;

namespace System.Collections.Generic
{
    public static class EnumerableExtensions
    {
        public static IContinuationEnumerable<TEntity> ToContinuationEnumerable<TEntity>(this IEnumerable<TEntity> values, IContinuationQuery continuationQuery)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (continuationQuery is null)
            {
                throw new ArgumentNullException(nameof(continuationQuery));
            }

            var pagination = LimitOffsetContinuationToken.FromContinuationQuery(continuationQuery);

            var paginatedResults = values
                .Select(x => new
                {
                    Item = x,
                    TotalCount = values.Count(),
                })
                .Skip(pagination.Offset)
                .Take(pagination.Limit)
                .ToArray();

            var totalCount = paginatedResults.FirstOrDefault()?.TotalCount ?? 0;
            var items = paginatedResults.Select(x => x.Item).ToList();

            return new ContinuationEnumerable<TEntity>(
                items,
                (items.Count + pagination.Offset) < totalCount ? pagination.GetNextPageContinuationToken() : null);
        }
    }
}
