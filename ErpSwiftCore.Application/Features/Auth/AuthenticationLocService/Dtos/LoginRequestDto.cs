﻿namespace ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Dtos
{
    public class LoginRequestDto
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
