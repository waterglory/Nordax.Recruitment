using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.DataAccess.Entities.Option
{
	public class BindingPeriod
	{
		[Key][Precision(3)][DatabaseGenerated(DatabaseGeneratedOption.None)] public int Length { get; set; }

		[Precision(8, 5)] public decimal InterestRate { get; set; }
	}
}
