using APPValper.Models;
using APPValper.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace APPValper.ViewModels
{
    class EditUserViewModel
    {

        OptionsService service = new OptionsService();
        public Language Language { get; set; }
        public string Url { get; private set; }

        private string LanguageSelected;
        public string EditUserTittle { get; set; }
        public string ChangeImageButtonText { get; set; }
        public string ActualNameText { get; set; }
        public string ChangeNameButtonText { get; set; }
        public string ActualPasswordText { get; set; }
        public string NewPasswordText { get; set; }
        public string ConfirmPasswordText { get; set; }
        public string ChangePasswordButtonText { get; set; }


        public EditUserViewModel()
        {
            Update();
            CheckLanguage();
        }

        private void CheckLanguage()
        {
            if (!string.IsNullOrEmpty(LanguageSelected))
            {
                if (LanguageSelected.Equals("Spanish"))
                {
                    EditUserTittle = "Configuración de usuario";
                    ChangeImageButtonText = "Cambiar imagen";
                    ActualNameText = "Nombre actual";
                    ChangeNameButtonText = "Cambiar nombre";
                    ActualPasswordText = "Contraseña actual";
                    NewPasswordText = "Contraseña nueva";
                    ConfirmPasswordText = "Confirmar contraseña";
                    ChangePasswordButtonText = "Cambiar contraseña";
                }
                else
                {
                    EditUserTittle = "User configuration";
                    ChangeImageButtonText = "Change image";
                    ActualNameText = "Actual name";
                    ChangeNameButtonText = "Change name";
                    ActualPasswordText = "Actual password";
                    NewPasswordText = "New password";
                    ConfirmPasswordText = "Confirm password";
                    ChangePasswordButtonText = "Change password";
                }
            }
        }

        private void Update()
        {
            Url = service.ConsultLocal().Url;
            LanguageSelected = service.ConsultLanguage().Name;
        }

    }

}
