﻿using Core.Results.Advanced.Interfaces;

namespace Core.Results.Basic.Interfaces;

public interface IMapperConvertable : IInfraConvertable
{
    public MapperResult ToMapperResult();
}

public interface IMapperConvertable<T> : IMapperConvertable, IInfraConvertable<T>
{
    public MapperResult<T> ToTypedMapperResult();
}