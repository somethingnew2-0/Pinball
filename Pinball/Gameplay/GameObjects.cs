using System;
using System.Collections.Generic;
using System.Text;

namespace Pinball
{
    // By the way so there are two ways of detecting collsion
    // One is through this enum and the collsion event handler
    // And the other is throught the Collision Catorgories Enum
    // Where Cat1 is the ball, Cat2 is the flippers, and Cat 3
    // Is the rest, All Collide with Cat1, but that is it.

    // Here is the better way to do this, I'm going to use
    // Collision Categories:
    //
    // HighBall == Cat1
    // NormalBall == Cat2
    // LowBall == Cat3
    // Flipper == Cat4
    // Plunger == Cat5
    // Bumper == Cat6
    // Sensor == Cat7
    // HighWall == Cat8
    // NormalWall == Cat9
    // LowWall == Cat10
    // Border == Cat11
    //
    // instead of the old:
    //
    //public enum GameObjects
    //{
    //    HighBall = 1,
    //    NormalBall = 2,
    //    LowBall = 3,
    //    Flipper = 4,
    //    Plunger = 5,
    //    Bumper = 6,
    //    Sensor = 7,
    //    HighWall = 8,
    //    NormalWall = 9,
    //    LowWall = 10,
    //    Border = 11,
    //}
}