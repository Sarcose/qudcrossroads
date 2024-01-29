using System;
using XRL;
using XRL.Core;
using XRL.Rules;

namespace QudCrossroads.Utilities
{
    [HasGameBasedStaticCache]
    public static class QudCrossroads_Random
    {
        private static Random _rand;
        public static Random Rand
        {
            get
            {
                if (_rand == null)
                {
                    if (XRLCore.Core?.Game == null)
                    {
                        throw new Exception("QudCrossroads mod attempted to retrieve Random, but Game is not created yet.");
                    }
                    else if (XRLCore.Core.Game.IntGameState.ContainsKey("QudCrossroads:Random"))
                    {
                        int seed = XRLCore.Core.Game.GetIntGameState("QudCrossroads:Random");
                        _rand = new Random(seed);
                    }
                    else
                    {
                        _rand = Stat.GetSeededRandomGenerator("QudCrossroads");
                    }
                    XRLCore.Core.Game.SetIntGameState("QudCrossroads:Random", _rand.Next());
                }
                return _rand;
            }
        }

        [GameBasedCacheInit]
        public static void ResetRandom()
        {
            _rand = null;
        }

        public static int Next(int minInclusive, int maxInclusive)
        {
            return Rand.Next(minInclusive, maxInclusive + 1);
        }
    }
}