using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Cocktail.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAllConfigurationsAssemblyNamespace( this ModelBuilder modelBuilder, Assembly assembly, string configNamespace )
    {
        var applyGenericMethods = typeof( ModelBuilder ).GetMethods( BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy );
        var applyGenericApplyConfigurationMethods = applyGenericMethods.Where( m => m.IsGenericMethod && m.Name.Equals( "ApplyConfiguration", StringComparison.OrdinalIgnoreCase ) );
        var applyGenericMethod = applyGenericApplyConfigurationMethods.FirstOrDefault( m => m.GetParameters().FirstOrDefault()?.ParameterType.Name == "IEntityTypeConfiguration`1" );

        var applicableTypes = assembly
                             .GetTypes()
                             .Where( c => c.IsClass && !c.IsAbstract && !c.ContainsGenericParameters )
                             .Where( c => c.Namespace == configNamespace );

        foreach ( var type in applicableTypes )
        foreach ( var typeInterface in type.GetInterfaces().Distinct() )
        {
            // check if type implements interface IEntityTypeConfiguration<SomeEntity>
            if ( !typeInterface.IsConstructedGenericType || typeInterface.GetGenericTypeDefinition() != typeof( IEntityTypeConfiguration<> ) ) continue;

            // make concrete ApplyConfiguration<SomeEntity> method
            if ( applyGenericMethod is not null )
            {
                var applyConcreteMethod =
                    applyGenericMethod.MakeGenericMethod( typeInterface.GenericTypeArguments[ 0 ] );
                // and invoke that with fresh instance of your configuration type
                applyConcreteMethod.Invoke( modelBuilder, new[] { Activator.CreateInstance( type ) } );
                Console.WriteLine( $"Model builder : applied model {type.Name}" );
            }

            break;
        }
    }
}