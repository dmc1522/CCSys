using PdfSharp.Fonts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.BL.Utils
{
    class FontResolver : IFontResolver
    {

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            // Ignore case of font names.
            var name = familyName.ToLower().Replace("#bi", string.Empty);
            name = name.ToLower().Replace("#b", string.Empty);
            name = name.ToLower().Replace("#", string.Empty);
           
            // Deal with the fonts we know.
            switch (name)
            {
                case "arial":
                    if (isBold)
                    {
                        if (isItalic)
                            return new FontResolverInfo("Arial#bi");
                        return new FontResolverInfo("Arial#b");
                    }
                    if (isItalic)
                        return new FontResolverInfo("Arial#i");
                    return new FontResolverInfo("Arial#");
                case "times new roman":
                    if (isBold)
                    {
                        if (isItalic)
                            return new FontResolverInfo("Times#bi");
                        return new FontResolverInfo("Times#b");
                    }
                    if (isItalic)
                        return new FontResolverInfo("Times#i");
                    return new FontResolverInfo("Times#");
                case "times":
                    if (isBold)
                    {
                        if (isItalic)
                            return new FontResolverInfo("Times#bi");
                        return new FontResolverInfo("Times#b");
                    }
                    if (isItalic)
                        return new FontResolverInfo("Times#i");
                    return new FontResolverInfo("Times#");

                case "tahoma":
                    if (isBold)
                    {
                        if (isItalic)
                            return new FontResolverInfo("Tahoma#bi");
                        return new FontResolverInfo("Tahoma#b");
                    }
                    if (isItalic)
                        return new FontResolverInfo("Tahoma#i");
                    return new FontResolverInfo("Tahoma#");
            }

            // We pass all other font requests to the default handler.
            // When running on a web server without sufficient permission, you can return a default font at this stage.
            return PlatformFontResolver.ResolveTypeface(familyName, isBold, isItalic);
        }

        public byte[] GetFont(string faceName)
        {
            switch (faceName)
            {
                case "Arial#":
                    return LoadFontData("LasMargaritas.BL.Fonts.arial.ttf");

                case "Arial#b":
                    return LoadFontData("LasMargaritas.BL.Fonts.arialbd.ttf");

                case "Arial#i":
                    return LoadFontData("LasMargaritas.BL.Fonts.ariali.ttf");

                case "Arial#bi":
                    return LoadFontData("LasMargaritas.BL.Fonts.arialbi.ttf");
                case "Times#":
                    return LoadFontData("LasMargaritas.BL.Fonts.times.ttf"); 

                case "Times#b":
                    return LoadFontData("LasMargaritas.BL.Fonts.timesbd.ttf");

                case "Times#i":
                    return LoadFontData("LasMargaritas.BL.Fonts.timesi.ttf");

                case "Times#bi":
                    return LoadFontData("LasMargaritas.BL.Fonts.timesbi.ttf");

                case "Tahoma#":
                    return LoadFontData("LasMargaritas.BL.Fonts.tahoma.ttf");

                case "Tahoma#b":
                    return LoadFontData("LasMargaritas.BL.Fonts.tahomabd.ttf");

                case "Tahoma#i":
                    return LoadFontData("LasMargaritas.BL.Fonts.tahomai.ttf");

                case "Tahoma#bi":
                    return LoadFontData("LasMargaritas.BL.Fonts.tahomabi.ttf");
            }

            return null;
        }

        /// <summary>
        /// Returns the specified font from an embedded resource.
        /// </summary>
        private byte[] LoadFontData(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // Test code to find the names of embedded fonts - put a watch on "ourResources"
            //var ourResources = assembly.GetManifestResourceNames();

            using (Stream stream = assembly.GetManifestResourceStream(name))
            {
                if (stream == null)
                    throw new ArgumentException("No resource with name " + name);

                int count = (int)stream.Length;
                byte[] data = new byte[count];
                stream.Read(data, 0, count);
                return data;
            }
        }

        internal static FontResolver OurGlobalFontResolver = null;

        /// <summary>
        /// Ensure the font resolver is only applied once (or an exception is thrown)
        /// </summary>
        internal static void Apply()
        {
            if (OurGlobalFontResolver == null || GlobalFontSettings.FontResolver == null)
            {
                if (OurGlobalFontResolver == null)
                    OurGlobalFontResolver = new FontResolver();

                GlobalFontSettings.FontResolver = OurGlobalFontResolver;
            }
        }

    }
}

