﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Domain.Service
{
    public interface ISyncDatabaseService
    {
        Task Sync();
    }
}
