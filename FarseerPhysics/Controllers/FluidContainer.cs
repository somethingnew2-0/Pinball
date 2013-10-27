using System;

using Microsoft.Xna.Framework;

using FarseerGames.FarseerPhysics.Mathematics;
using FarseerGames.FarseerPhysics.Collisions;

namespace FarseerGames.FarseerPhysics.Controllers
{
    public interface IFluidContainer
    {
        bool Intersect(AABB aabb);
        bool Contains(ref Vector2 vector);
    }
}
