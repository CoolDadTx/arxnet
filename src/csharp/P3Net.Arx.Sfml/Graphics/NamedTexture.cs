using System;

using SFML.Graphics;

namespace P3Net.Arx.Sfml.Graphics
{
    public class NamedTexture : Texture
    {
        #region Construction

        public NamedTexture ( string name, string filename ) : base(filename)
        {
            Name = name ?? System.IO.Path.GetFileNameWithoutExtension(filename);
        }
        #endregion

        public string Name { get; }
    }
}
