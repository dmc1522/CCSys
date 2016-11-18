﻿using System;

namespace LasMargaritas.Models
{
    [Flags]
    public enum RancherError
    {
        None = 0,
        ApiCommunicationError = 1,
        InvalidName = 2,
        InvalidState = 4,
        InvalidId = 8,
    }
}
