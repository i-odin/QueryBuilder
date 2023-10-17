﻿using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using QueryBuilder.Ms.Queries;

namespace QueryBuilder.Ms;

public partial class MsQueryBuilder : Core.QueryBuilder
{
    public MsQueryBuilder(QueryBuilderSource source) : base(source) { }

    public MsDeleteQueryBuilder<T> Delete<T>()
        where T : ITableBuilder
        => MsDeleteQueryBuilder<T>.Make(Source).Delete();

    public MsSelectQueryBuilder<T> Select<T>(Action<MsSelectBuilder<T>> inner)
        where T : ITableBuilder 
        => MsSelectQueryBuilder<T>.Make(Source).Select(inner);

    public MsSelectQueryBuilder<TOne, TTwo> Select<TOne, TTwo>(Action<MsSelectBuilder<TOne, TTwo>> inner)
        where TOne : ITableBuilder
        where TTwo : ITableBuilder 
        => MsSelectQueryBuilder<TOne, TTwo>.Make(Source).Select(inner);
}