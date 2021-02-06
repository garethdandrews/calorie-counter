using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace backend_api.Extensions
{
    public static class ModelStateExtensions
    {
        /// <summary></summary>
        /// <param name="dictionary"></param>
        /// <returns>
        /// A list of error messages from the model state dictionary
        /// </returns>
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(m => m.Value.Errors)
                             .Select(m => m.ErrorMessage)
                             .ToList();
        }
    }
}