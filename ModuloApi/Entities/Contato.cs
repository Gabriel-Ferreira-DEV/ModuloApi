using System.ComponentModel.DataAnnotations;

namespace ModuloApi.Entities
{
    public class Contato
    {
        public int Id { get; set; }
        [MaxLength(100)] // define o tamanho da coluna na tabela no sqlserver
        public string  Nome { get; set; }
        [MaxLength(20)]
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
    }
}
