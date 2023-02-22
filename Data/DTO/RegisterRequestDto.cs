﻿using Data.Models.DTO.Base;

namespace Data.Models.DTO;

public class RegisterRequestDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public BaseCurrencyDto Currency { get; set; }
}