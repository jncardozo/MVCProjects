using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DBFMvcMovies.Models
{
    public class UserAccountsModel
    {
        #region Constructors
        public UserAccountsModel() { }
        #endregion

        #region Properties        
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombres")]        
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = @"Correo Electrónico")]
        [DataType(DataType.EmailAddress, ErrorMessage = @"Dirección de correo electrónico no válida")]
        public string Email { get; set; }

        [Display(Name = "Usuario")]
        [Required]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]        
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirmar contraseña")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        #endregion

        #region Methods
        internal UserAccount GetDocumentoTipo()
        {
            var userAccount = new UserAccount()
            {                
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Username = Username,
                Password = Password,
                ConfirmPassword = ConfirmPassword
            };
            return userAccount;
        }
        #endregion
    }
}