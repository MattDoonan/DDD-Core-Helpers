﻿using Base.ValueObjects.Identifiers.Base;

namespace Base.Enitities.Regular;

public interface IEntity;
public interface IEntity<out TId> : IEntity
    where TId : IIdentifier
{
    public TId Id { get; }
}