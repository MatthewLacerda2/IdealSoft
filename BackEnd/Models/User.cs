using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models;
public class User {
    public int Id { get; set; }

    [Required]
    public string name { get; set; } = string.Empty;

    [Required]
    public string surname { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^\+\d{1,3}\s?\(\d{1,4}\)\s?\d{6,8}$", ErrorMessage = "Telefone inválido. Use o formato: +xx (xxxx) xxxxxxxx")]
    public string telephone { get; set; } = string.Empty;
}