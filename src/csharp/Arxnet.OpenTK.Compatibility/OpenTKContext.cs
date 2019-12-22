/*
 * Copyright © Michael Taylor (P3Net)
 * All Rights Reserved
 *
 * http://www.michaeltaylorp3.net
 * 
 * Converted code from ARX C++ (http://www.landbeyond.net/arx/index.php)
 * Code converted using C++ to C# Code Converter, Tangible Software (https://www.tangiblesoftwaresolutions.com/)
 */
using System;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Platform;

namespace Arxnet.OpenTK.Compatibility
{
    public static class OpenTKContext
    {
        public static void Initialize ( IntPtr windowHandle )
        {
            Toolkit.Init();

            //FIX: Work around issue with OpenTK and SFML (https://en.sfml-dev.org/forums/index.php?topic=18276.0)
            var windowInfo = Utilities.CreateWindowsWindowInfo(windowHandle);
            s_context = new GraphicsContext(new ContextHandle(IntPtr.Zero), windowInfo);

            s_context.MakeCurrent(windowInfo);
            s_context.LoadAll();
        }

        private static GraphicsContext s_context;
    }
}
