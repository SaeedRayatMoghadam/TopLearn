using System.ComponentModel.DataAnnotations;

namespace TopLearn.Data.Models;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }

    public DateTime RegisterDate { get; set; }
}