﻿namespace Penman.EpubSharp.Cli;

public interface IConsoleWriter
{
    void WriteSuccess(string message);
    void WriteError(string message);
}