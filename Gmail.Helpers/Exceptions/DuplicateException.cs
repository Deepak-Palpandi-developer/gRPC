﻿namespace Gmail.Helpers.Exceptions;

public class DuplicateException : Exception
{
    public DuplicateException(string message) : base(message)
    {
    }
}
