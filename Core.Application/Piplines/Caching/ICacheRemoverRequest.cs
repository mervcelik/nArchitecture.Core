﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Piplines.Caching;

public interface ICacheRemoverRequest
{
    string? CacheKey { get; }
    bool BypassCache { get; }
    string? CacheGroupKey { get; }
}
