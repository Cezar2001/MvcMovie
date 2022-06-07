using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class SuperValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validation)
        {
            string fieldValue = (string) value;
            if (fieldValue == null || fieldValue.Trim().IndexOf(" ") == -1)
            {
                return new ValidationResult("Il campo deve contenere almeno due parole");
            }
            return ValidationResult.Success;
        }
    }

    public class Impiegato
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string EmailAddress { get; set; }
        public string PersonalWebSite { get; set; }
        public string Photo { get; set; }
        public string AlternateText { get; set; }
    }

    public class ImpiegatoFile
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Il campo FullName è obbligatorio")]
        [SuperValidation]
        public string FullName { get; set; }
        public string Gender { get; set; }
        
        [Required(ErrorMessage = "Il campo City è obbligatorio")]
        [StringLength(5, ErrorMessage = "La città non può avere più di 5 caratteri")]
        public string City { get; set; }
        
        [EmailAddress(ErrorMessage = "La mail che hai inserito non è valida")]
        [Required(ErrorMessage = "La mail è obbligatoria")]
        [StringLength(20, ErrorMessage = "La mail non può avere più di 20 caratteri")]
        public string EmailAddress { get; set; }
        public string PersonalWebSite { get; set; }
        public string Photo { get; set; }
        public string AlternateText { get; set; }
        public IFormFile File { get; set; }
    }

    public class EsitoOperazionePut
    {
        public string sEsito { get; set; }
    }
}
