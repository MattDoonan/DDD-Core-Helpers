﻿using Core.Results.Base.Enums;

namespace Core.Results.Base.Interfaces;

public interface IResultFailure
{
    public bool IsFailure { get; }
    public IReadOnlyList<string> ErrorMessages { get; }
}

public interface IResultStatus : IResultFailure
{
    public bool IsSuccessful => !IsFailure;
    public FailureType FailureType { get; }
    public FailedLayer FailedLayer { get; }
    public string MainError => FailureType.ToMessage();
}