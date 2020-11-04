using System.Collections.Generic;

namespace RPG.Stats
{
    interface IModifierProvider
    {
        IEnumerable<float> GetAdditiveModifiers(Stat stat);
        IEnumerable<float> GetPersentageModifiers(Stat stat);
    }
}
    
