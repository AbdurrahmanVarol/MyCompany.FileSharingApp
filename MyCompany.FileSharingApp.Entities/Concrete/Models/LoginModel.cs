﻿using System.ComponentModel.DataAnnotations;

namespace MyCompany.FileSharingApp.Entities.Concrete
{
    public class LoginModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Lütfen kullanıcı adını giriniz.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Lütfen kullanıcı adını giriniz.")]
        public string Password { get; set; }
        public bool IsKeepLoggedIn { get; set; }
    }
}
