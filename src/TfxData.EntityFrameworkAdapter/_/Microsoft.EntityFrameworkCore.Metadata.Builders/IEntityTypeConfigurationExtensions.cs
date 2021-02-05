#if NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.Metadata.Builders
{

  public static class EntityTypeBuilderExtensions
  {

    public static void ManyToOne<TEntity, TRelatedEntity>(this EntityTypeBuilder<TEntity> builder,
      Expression<Func<TEntity, object>> foreignKeyExpression,
      Expression<Func<TEntity, TRelatedEntity>> fordwardNavigationExpression,
      Expression<Func<TRelatedEntity, object>> principalKeyExpression = null
    )
      where TEntity : class
      where TRelatedEntity : class
    {
      ReferenceCollectionBuilder<TRelatedEntity, TEntity> refBuilder = builder.HasOne(fordwardNavigationExpression)
        .WithMany()
        .HasForeignKey(foreignKeyExpression)
        .OnDelete(DeleteBehavior.Restrict);
      if (principalKeyExpression != null)
      {
        _ = refBuilder.HasPrincipalKey(principalKeyExpression);
      }
    }

    public static void ManyToOne<TEntity, TRelatedEntity>(this EntityTypeBuilder<TEntity> builder,
      Expression<Func<TEntity, object>> foreignKeyExpression,
      Expression<Func<TEntity, TRelatedEntity>> fordwardNavigationExpression,
      Expression<Func<TRelatedEntity, IEnumerable<TEntity>>> reversedNavigationExpression,
      Expression<Func<TRelatedEntity, object>> principalKeyExpression = null
    )
      where TEntity : class
      where TRelatedEntity : class
    {
      ReferenceCollectionBuilder<TRelatedEntity, TEntity> refBuilder = builder.HasOne(fordwardNavigationExpression)
        .WithMany(reversedNavigationExpression)
        .HasForeignKey(foreignKeyExpression)
        .OnDelete(DeleteBehavior.Restrict);
      if (principalKeyExpression != null)
      {
        _ = refBuilder.HasPrincipalKey(principalKeyExpression);
      }
    }

    public static void Index<TEntity, TProperty1>(this EntityTypeBuilder<TEntity> builder,
      Expression<Func<TEntity, TProperty1>> property1Expression,
      string name = null, bool? isUnique = null
    )
      where TEntity : class
    {
      string property1Name = property1Expression.GetPropertyName();
      string[] propertyNames = new string[]
      {
        property1Name
      };
      _ = builder.Index(propertyNames, name, isUnique);
    }

    public static void Index<TEntity, TProperty1, TProperty2>(this EntityTypeBuilder<TEntity> builder,
      Expression<Func<TEntity, TProperty1>> property1Expression,
      Expression<Func<TEntity, TProperty2>> property2Expression,
      string name = null, bool? isUnique = null
    )
      where TEntity : class
    {
      string property1Name = property1Expression.GetPropertyName();
      string property2Name = property2Expression.GetPropertyName();
      string[] propertyNames = new string[]
      {
        property1Name,
        property2Name
      };
      _ = builder.Index(propertyNames, name, isUnique);
    }

    public static void Index<TEntity, TProperty1, TProperty2, TProperty3>(this EntityTypeBuilder<TEntity> builder,
      Expression<Func<TEntity, TProperty1>> property1Expression,
      Expression<Func<TEntity, TProperty2>> property2Expression,
      Expression<Func<TEntity, TProperty3>> property3Expression,
      string name = null, bool? isUnique = null
    )
      where TEntity : class
    {
      string property1Name = property1Expression.GetPropertyName();
      string property2Name = property2Expression.GetPropertyName();
      string property3Name = property3Expression.GetPropertyName();
      string[] propertyNames = new string[]
      {
        property1Name,
        property2Name,
        property3Name
      };
      _ = builder.Index(propertyNames, name, isUnique);
    }

    public static void Index<TEntity, TProperty1, TProperty2, TProperty3, TProperty4>(this EntityTypeBuilder<TEntity> builder,
      Expression<Func<TEntity, TProperty1>> property1Expression,
      Expression<Func<TEntity, TProperty2>> property2Expression,
      Expression<Func<TEntity, TProperty3>> property3Expression,
      Expression<Func<TEntity, TProperty4>> property4Expression,
      string name = null, bool? isUnique = null
    )
      where TEntity : class
    {
      string property1Name = property1Expression.GetPropertyName();
      string property2Name = property2Expression.GetPropertyName();
      string property3Name = property3Expression.GetPropertyName();
      string property4Name = property4Expression.GetPropertyName();
      string[] propertyNames = new string[]
      {
        property1Name,
        property2Name,
        property3Name,
        property4Name
      };
      _ = builder.Index(propertyNames, name, isUnique);
    }

    public static IndexBuilder Index<TEntity>(this EntityTypeBuilder<TEntity> builder, string[] propertyNames, string name = null, bool? isUnique = null)
      where TEntity : class
    {
      IndexBuilder indexBuilder = builder.HasIndex(propertyNames);
      if (name != null)
      {
        indexBuilder = indexBuilder.HasName(name);
      }
      if (isUnique.HasValue)
      {
        indexBuilder = indexBuilder.IsUnique(isUnique.Value);
      }
      return indexBuilder;
    }

    public static void MapToTable<TEntity>(this EntityTypeBuilder<TEntity> builder, string name)
      where TEntity : class
    {
      _ = builder.ToTable(name);
    }

    public static void MapToTable<TEntity>(this EntityTypeBuilder<TEntity> builder, string name, string schema)
      where TEntity : class
    {
      _ = builder.ToTable(name, schema);
    }

    public static void MaxLength<TEntity, TProperty1>(this EntityTypeBuilder<TEntity> builder,
      Expression<Func<TEntity, TProperty1>> property1Expression,
      int maxLength
    )
      where TEntity : class
    {
      _ = builder.Property(property1Expression)
        .HasMaxLength(maxLength);
    }

    public static void OneToMany<TEntity, TRelatedEntity>(this EntityTypeBuilder<TEntity> builder,
      Expression<Func<TRelatedEntity, object>> foreignKeyExpression,
      Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> fordwardNavigationExpression,
      Expression<Func<TEntity, object>> principalKeyExpression = null
    )
      where TEntity : class
      where TRelatedEntity : class
    {
      ReferenceCollectionBuilder<TEntity, TRelatedEntity> refBuilder = builder.HasMany(fordwardNavigationExpression)
        .WithOne()
        .HasForeignKey(foreignKeyExpression)
        .OnDelete(DeleteBehavior.Restrict);
      if (principalKeyExpression != null)
      {
        _ = refBuilder.HasPrincipalKey(principalKeyExpression);
      }
    }

    public static void OneToMany<TEntity, TRelatedEntity>(this EntityTypeBuilder<TEntity> builder,
      Expression<Func<TRelatedEntity, object>> foreignKeyExpression,
      Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> fordwardNavigationExpression,
      Expression<Func<TRelatedEntity, TEntity>> reversedNavigationExpression,
      Expression<Func<TEntity, object>> principalKeyExpression = null
    )
      where TEntity : class
      where TRelatedEntity : class
    {
      ReferenceCollectionBuilder<TEntity, TRelatedEntity> refBuilder = builder.HasMany(fordwardNavigationExpression)
        .WithOne(reversedNavigationExpression)
        .HasForeignKey(foreignKeyExpression)
        .OnDelete(DeleteBehavior.Restrict);
      if (principalKeyExpression != null)
      {
        _ = refBuilder.HasPrincipalKey(principalKeyExpression);
      }
    }

    public static void OneToOne<TEntity, TRelatedEntity>(this EntityTypeBuilder<TEntity> builder,
      Expression<Func<TEntity, object>> foreignKeyExpression,
      Expression<Func<TEntity, TRelatedEntity>> fordwardNavigationExpression,
      Expression<Func<TRelatedEntity, object>> principalKeyExpression = null
    )
      where TEntity : class
      where TRelatedEntity : class
    {
      ReferenceReferenceBuilder<TEntity, TRelatedEntity> refBuilder = builder.HasOne(fordwardNavigationExpression)
        .WithOne()
        .HasForeignKey(foreignKeyExpression)
        .OnDelete(DeleteBehavior.Restrict);
      if (principalKeyExpression != null)
      {
        _ = refBuilder.HasPrincipalKey(principalKeyExpression);
      }
    }

    public static void OneToOne<TEntity, TRelatedEntity>(this EntityTypeBuilder<TEntity> builder,
      Expression<Func<TEntity, object>> foreignKeyExpression,
      Expression<Func<TEntity, TRelatedEntity>> fordwardNavigationExpression,
      Expression<Func<TRelatedEntity, TEntity>> reversedNavigationExpression,
      Expression<Func<TRelatedEntity, object>> principalKeyExpression = null
    )
      where TEntity : class
      where TRelatedEntity : class
    {
      ReferenceReferenceBuilder<TEntity, TRelatedEntity> refBuilder = builder.HasOne(fordwardNavigationExpression)
        .WithOne(reversedNavigationExpression)
        .HasForeignKey(foreignKeyExpression)
        .OnDelete(DeleteBehavior.Restrict);
      if (principalKeyExpression != null)
      {
        _ = refBuilder.HasPrincipalKey(principalKeyExpression);
      }
    }

    public static void PrimaryKey<TEntity, TProperty1>(this EntityTypeBuilder<TEntity> builder,
      Expression<Func<TEntity, TProperty1>> property1Expression,
      string name = null, bool? isAutoGenerated = null
    )
      where TEntity : class
    {
      string property1Name = property1Expression.GetPropertyName();
      string[] propertyNames = new string[]
      {
        property1Name
      };
      builder.PrimaryKey(propertyNames, name, isAutoGenerated);
    }

    public static void PrimaryKey<TEntity, TProperty1, TProperty2>(this EntityTypeBuilder<TEntity> builder,
      Expression<Func<TEntity, TProperty1>> property1Expression,
      Expression<Func<TEntity, TProperty2>> property2Expression,
      string name = null, bool? isAutoGenerated = null
    )
      where TEntity : class
    {
      string property1Name = property1Expression.GetPropertyName();
      string property2Name = property2Expression.GetPropertyName();
      string[] propertyNames = new string[]
      {
        property1Name,
        property2Name
      };
      builder.PrimaryKey(propertyNames, name, isAutoGenerated);
    }

    public static void PrimaryKey<TEntity, TProperty1, TProperty2, TProperty3>(this EntityTypeBuilder<TEntity> builder,
      Expression<Func<TEntity, TProperty1>> property1Expression,
      Expression<Func<TEntity, TProperty2>> property2Expression,
      Expression<Func<TEntity, TProperty3>> property3Expression,
      string name = null, bool? isAutoGenerated = null
    )
      where TEntity : class
    {
      string property1Name = property1Expression.GetPropertyName();
      string property2Name = property2Expression.GetPropertyName();
      string property3Name = property3Expression.GetPropertyName();
      string[] propertyNames = new string[]
      {
        property1Name,
        property2Name,
        property3Name
      };
      builder.PrimaryKey(propertyNames, name, isAutoGenerated);
    }

    public static void PrimaryKey<TEntity, TProperty1, TProperty2, TProperty3, TProperty4>(this EntityTypeBuilder<TEntity> builder,
      Expression<Func<TEntity, TProperty1>> property1Expression,
      Expression<Func<TEntity, TProperty2>> property2Expression,
      Expression<Func<TEntity, TProperty3>> property3Expression,
      Expression<Func<TEntity, TProperty4>> property4Expression,
      string name = null, bool? isAutoGenerated = null
    )
      where TEntity : class
    {
      string property1Name = property1Expression.GetPropertyName();
      string property2Name = property2Expression.GetPropertyName();
      string property3Name = property3Expression.GetPropertyName();
      string property4Name = property4Expression.GetPropertyName();
      string[] propertyNames = new string[]
      {
        property1Name,
        property2Name,
        property3Name,
        property4Name
      };
      builder.PrimaryKey(propertyNames, name, isAutoGenerated);
    }

    public static void PrimaryKey<TEntity>(this EntityTypeBuilder<TEntity> builder, string[] propertyNames, string name = null, bool? isAutoGenerated = null)
      where TEntity : class
    {
      KeyBuilder keyBuilder = builder.HasKey(propertyNames);
      if (name != null)
      {
        _ = keyBuilder.HasName(name);
      }
      if (isAutoGenerated.HasValue)
      {
        foreach(string propertyName in propertyNames)
        {
          _ = isAutoGenerated.Value
            ? builder.Property(propertyName).ValueGeneratedOnAdd()
            : builder.Property(propertyName).ValueGeneratedNever();
        }
      }
    }

    public static void Require<TEntity>(this EntityTypeBuilder<TEntity> builder,
      Expression<Func<TEntity, object>> propertyExpression
    )
      where TEntity : class
    {
      _ = builder.Property(propertyExpression)
        .IsRequired();
    }

    public static void RowVersion<TEntity>(this EntityTypeBuilder<TEntity> builder,
      Expression<Func<TEntity, object>> propertyExpression
    )
      where TEntity : class
    {
      _ = builder.Property(propertyExpression)
        .IsConcurrencyToken(true)
        .ValueGeneratedOnAddOrUpdate();
    }

    private static string GetPropertyName<TEntity, TProperty>(this Expression<Func<TEntity, TProperty>> expression)
    {
      if (expression.NodeType == ExpressionType.Lambda)
      {
        LambdaExpression lamdaExpression = (LambdaExpression)expression;
        Expression body = lamdaExpression.Body;
        return GetPropertyNameFromExpression(body);
      }
      return null;
    }

    private static string GetPropertyNameFromExpression(Expression expression)
    {
      if (expression.NodeType == ExpressionType.Lambda)
      {
        LambdaExpression lamdaExpression = (LambdaExpression)expression;
        Expression body = lamdaExpression.Body;
        return GetPropertyNameFromExpression(body);
      }
      if (expression.NodeType == ExpressionType.MemberAccess)
      {
        MemberExpression memberExpression = (MemberExpression)expression;
        MemberInfo memberInfo = memberExpression.Member;
        if (memberInfo.MemberType == MemberTypes.Property)
        {
          PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
          return propertyInfo.Name;
        }
      }
      return null;
    }
  }
}
#endif
