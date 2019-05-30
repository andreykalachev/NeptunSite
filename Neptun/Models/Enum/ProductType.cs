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

        [Display(Name = "ВЧ Связь")]
        HfCommunication = 1,

        [Display(Name = "ВЧ Защита")]
        HfProtection = 2,

        [Display(Name = "Другое")]
        Other = 3,

        [Display(Name = "Очистные сооружения")]
        Cleaning = 4
    }
}