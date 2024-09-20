﻿namespace authentication_security.Core.Application.Common;
public class Result
{
    public bool IsSuccess { get; }
    public string Error { get; }

    protected Result(bool isSuccess, string errorMessage)
    {
        IsSuccess = isSuccess;
        Error = errorMessage;
    }

    public static Result Success() => new(true, string.Empty);

    public static Result Failure(string errorMessage) => new(false, errorMessage);
}

public class Result<T> : Result
{
    public T Value { get; }

    protected Result(bool isSuccess, T value, string errorMessage) : base(isSuccess, errorMessage)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(true, value, string.Empty);

    public static new Result<T> Failure(string errorMessage) => new(false, default!, errorMessage);
}

