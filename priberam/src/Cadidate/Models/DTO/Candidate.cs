using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace priberam.Models.DTO
{
    public class Candidate : EntityInterface
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "This field is required...")]
        [DisplayName("Firstname")]
        public string Firstname { get; set; }

        [Column(TypeName = "nvarchar(190)")]
        [Required(ErrorMessage = "This field is required...")]
        [DisplayName("Lastname")]
        public string Lastname { get; set; }

        [DisplayName("Application Date")]
        public DateTime Date { get; set; }

        public int Experience { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Position { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Avatar { get; set; }
       
    }
}
