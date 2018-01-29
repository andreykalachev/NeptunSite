using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Neptun.Models.Enum
{
    public enum ProductType
    {
        [Display(Name = "ВЧ Обработка")]
        HfProcessing = 0,

        [Display(Name = "ВЧ Связь и Защита")]
        HfCommunicationAndProtection = 1,

        [Display(Name = "Другое")]
        Other = 2
    }
}