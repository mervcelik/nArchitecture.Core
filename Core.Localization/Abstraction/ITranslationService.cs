using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Localization.Abstraction;

public interface ITranslationService
{
    public Task<string> TranslateAsync(string text, string to, string from = "en");
}