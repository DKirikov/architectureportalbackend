﻿namespace ArchitecturePortalBackend.BusinessLogic.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}