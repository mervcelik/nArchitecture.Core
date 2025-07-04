﻿using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.SeriLog;

public abstract class LoggerServiceBase
{
    protected ILogger Logger { get; set; }
    protected LoggerServiceBase()
    {
        Logger = null;
    }

    protected LoggerServiceBase(ILogger logger)
    {
        Logger = logger;
    }

    public void Verbose(string message) => Logger.Verbose(message);

    public void Fatal(string message) => Logger.Fatal(message);

    public void Info(string message) => Logger.Information(message);

    public void Warn(string message) => Logger.Warning(message);

    public void Debug(string message) => Logger.Debug(message);

    public void Error(string message) => Logger.Error(message);
}
