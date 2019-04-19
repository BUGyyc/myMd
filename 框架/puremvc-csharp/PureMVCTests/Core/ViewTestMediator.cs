﻿//
//  PureMVC C# Standard
//
//  Copyright(c) 2017 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using PureMVC.Patterns.Mediator;

namespace PureMVC.Core
{
    /// <summary>
    /// A Mediator class used by ViewTest.
    /// </summary>
    /// <seealso cref="ViewTest"/>
    public class ViewTestMediator : Mediator
    {
        // The Mediator name
        public static new string NAME = "ViewTestMediator";

        // Constructor
        public ViewTestMediator(object viewComponent) : base(NAME, viewComponent)
        {
        }

        // // be sure that the mediator has some Observers created
        // in order to test removeMediator
        public override string[] ListNotificationInterests()
        {
            return new string[3] { "ABC", "DEF", "GHI"};
        }
    }
}
