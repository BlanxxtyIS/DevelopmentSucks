﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentSucks.Application.Contracts.DTO;

public class RefreshRequestDto
{
    public string RefreshToken { get; set; } = string.Empty;
}
