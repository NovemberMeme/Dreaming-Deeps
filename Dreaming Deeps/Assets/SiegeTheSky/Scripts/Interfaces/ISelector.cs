using UnityEngine;

namespace SiegeTheSky
{
    public interface ISelector
    {
        void Check(Ray ray);
    }
}