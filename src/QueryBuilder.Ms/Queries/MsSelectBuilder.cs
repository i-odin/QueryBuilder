﻿using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;
using QueryBuilder.Ms.Translators;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace QueryBuilder.Ms.Queries;

public interface IMsSelectBuilder<T>
    where T : ITableBuilder
{
    IMsSelectBuilder<TDto> Bind<TDto>() where TDto : ITableBuilder;
    IMsSelectBuilder<T> All();
    IMsSelectBuilder<T> Column<TField>([NotNull] Expression<Func<T, TField>> column);
    IMsSelectBuilder<T> Column(string column);
    IMsSelectBuilder<T> As(string value);
    IMsSelectBuilder<T> IsNullFunc<TField>([NotNull] Expression<Func<T, TField>> column, TField value);
}

public class MsSelectBuilder<T> : QueryBuilderCore, IMsSelectBuilder<T>
    where T : ITableBuilder
{
    public MsSelectBuilder(QueryBuilderSource source) : base(source) {}

    public IMsSelectBuilder<T> All()
    {
        new CommaSelectTranslator().Run(Source);
        new AllTranslator(T.GetTable()).Run(Source);
        return this;
    }

    public IMsSelectBuilder<T> As(string value)
    {
        new AsTranslator(value).Run(Source);
        return this;
    }

    public IMsSelectBuilder<T> Column<TField>([NotNull] Expression<Func<T, TField>> column)
    {
        Column(CommonExpression.GetColumnName(column));
        return this;
    }

    public IMsSelectBuilder<T> Column(string column)
    {
        new CommaSelectTranslator().Run(Source);
        new ColumnTranslator(column, T.GetTable()).Run(Source);
        return this;
    }

    public IMsSelectBuilder<TDto> Bind<TDto>() 
        where TDto : ITableBuilder
    {
        return MsSelectBuilder<TDto>.Make(Source, null);
    }

    public IMsSelectBuilder<T> IsNullFunc<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        new CommaSelectTranslator().Run(Source);
        new IsNullFuncTranslator(CommonExpression.GetColumnName(column), value, T.GetTable()).Run(Source);
        return this;
    }

    public static MsSelectBuilder<T> Make(QueryBuilderSource source, Action<MsSelectBuilder<T>> inner)
    {
        var obj = new MsSelectBuilder<T>(source);
        inner?.Invoke(obj);
        return obj;
    }
}