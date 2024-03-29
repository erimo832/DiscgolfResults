﻿namespace Results.Domain.Common.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> If<T>(this IQueryable<T> source, bool condition, Func<IQueryable<T>, IQueryable<T>> transform)
        {
            return condition ? transform(source) : source;
        }
    }
}
