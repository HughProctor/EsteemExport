using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace BusinessModel.Mappers
{
#pragma warning disable CS0618 // Type or member is obsolete
    public static class MappingExpressionExtensions
    {
        public static IMappingExpression<TSource, TDestination>
            IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = AutoMapper.Mapper.GetAllTypeMaps().First(x => x.SourceType.Equals(sourceType) && x.DestinationType.Equals(destinationType));
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
            return expression;
        }

        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExistingSource<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = AutoMapper.Mapper.GetAllTypeMaps().First(x => x.SourceType.Equals(sourceType) && x.DestinationType.Equals(destinationType));
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForSourceMember(property, opt => opt.Ignore());
            }
            return expression;
        }
    }

    public class TypeTypeConverter : ITypeConverter<string, Type>
    {
        public Type Convert(string source)
        {
            Type type = Assembly.GetExecutingAssembly().GetType(source);
            return type;
        }
    }

    public interface ITypeConverter<TSource, TDestination>
    {
        TDestination Convert(TSource source);
    }
#pragma warning restore CS0618 // Type or member is obsolete}
}