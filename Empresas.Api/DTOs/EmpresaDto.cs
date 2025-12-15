using System.ComponentModel.DataAnnotations;

namespace Empresas.Api.DTOs
{
    public class EmpresaDto
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(
            200,
            MinimumLength = 3,
            ErrorMessage = "O nome precisa ter entre 3 e 200 caracteres."
        )]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Endereço é obrigatório.")]
        [StringLength(
            300,
            MinimumLength = 5,
            ErrorMessage = "O endereço precisa ter entre 5 e 300 caracteres.")]
        public required string Endereco { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório.")]
        [RegularExpression(
            @"^\d{2}\s\d{4,5}-\d{4}$",
            ErrorMessage = "Telefone inválido."
        )]
        public required string Telefone { get; set; }
    }
}