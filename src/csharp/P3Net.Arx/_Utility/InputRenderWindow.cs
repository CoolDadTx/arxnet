/*
 * Copyright © Michael Taylor (P3Net)
 * All Rights Reserved
 *
 * http://www.michaeltaylorp3.net 
 */
using System;
using System.Collections.Generic;

using SFML.Graphics;
using SFML.Window;

namespace P3Net.Arx
{
    public class InputRenderWindow : RenderWindow
    {
        #region Construction

        public InputRenderWindow ( VideoMode mode, string title, Styles style ) : base(mode, title, style)
        {
            ListenEvents();
        }

        public InputRenderWindow ( VideoMode mode, string title, Styles style, ContextSettings settings ) : base(mode, title, style, settings)
        {
            ListenEvents();
        }
        #endregion

        public bool IsWindowClosing { get; private set; }

        /// <summary>Pop the event on top of the event queue, if any, and return it.</summary>
        /// <remarks>
        /// This function is not blocking: if there's no pending event then 
        /// it will return nothing.
        /// Note that more than one event may be present in the event queue,
        /// thus you should always call this function in a loop
        /// to make sure that you process every pending event.
        /// </remarks>
        /// <returns>The event, if any.</returns>
        public EventArgs PollEvents ()
        {
            DispatchEvents();

            lock (_events)
            {
                if (_events.Count > 0)
                    return _events.Dequeue();
            };

            return null;
        }

        #region Private Members

        private void ListenEvents ()
        {
            KeyPressed += OnKeyPressed;
            Closed += ( o, e ) => {                
                IsWindowClosing = true;
                UnlistenEvents();
            };
        }

        private void UnlistenEvents ()
        {
            KeyPressed -= OnKeyPressed;
        }

        private void OnKeyPressed ( object sender, SFML.Window.KeyEventArgs e )
        {
            if (IsWindowClosing)
                return;
            
            lock (_events)
            {
                _events.Enqueue(e);
            };
        }

        private readonly Queue<EventArgs> _events = new Queue<EventArgs>();

        #endregion
    }
}
