﻿namespace eCommerce.Core.Utilities.Results
{
    public interface IResult
    {
        bool Success { get; }
        object Message { get; }
    }
}
