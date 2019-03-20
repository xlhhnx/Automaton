using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automoton.Common
{
    public static class ListExtensions
    {
        public static IEnumerable<T> Add<T>( this IEnumerable<T> collection , T value )
        {
            collection.ToList().Add( value );
            return collection;
        }

        public static IEnumerable<T> AddRange<T>( this IEnumerable<T> collection , IEnumerable<T> values )
        {
            collection.ToList().AddRange( values );
            return collection;
        }

        public static IEnumerable<T> Remove<T>( this IEnumerable<T> collection , T value )
        {
            collection.ToList().Remove( value );
            return collection;
        }

        public static IEnumerable<T> RemoveRange<T>( this IEnumerable<T> collection , IEnumerable<T> values )
        {
            collection.ToList().RemoveRange( values );
            return collection;
        }

        public static IEnumerable<T> Reduce<T>( this IEnumerable<T> collection , int limit )
        {
            if ( collection.Count() <= limit )
                return collection;
            
            return collection
                    .Skip( collection.Count() - limit );
        }
    }
}
