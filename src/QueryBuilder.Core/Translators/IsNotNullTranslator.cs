﻿using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

public readonly ref struct IsNotNullTranslator
{
    public void Run(QueryBuilderSource source)
    {
        source.Query.Append(" is not null");
    }
}
