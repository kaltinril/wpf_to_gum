using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_to_gum_visual2_namespaces
{
    public class ComparisonItem
    {
        public Rectangle ItemBounds { get; set; }
        public string ItemName { get; set; }

        public Texture2D GoodImage { get; set; }
        public Texture2D NewImage { get; set; }
        public Texture2D ResultsImage { get; set; }

        GraphicsDevice GraphicsDevice { get; set; }

        public ComparisonItem()
        {

        }

        public ComparisonItem(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
        }

        public ComparisonItem(GraphicsDevice graphicsDevice, Texture2D goodImage)
        {
            GraphicsDevice = graphicsDevice;
            GoodImage = goodImage;
        }


        public void GetCurrentScreenImage()
        {
            int w = GraphicsDevice.PresentationParameters.BackBufferWidth;
            int h = GraphicsDevice.PresentationParameters.BackBufferHeight;

            var pixels = new Color[w * h];
            GraphicsDevice.GetBackBufferData(pixels);

            NewImage = new Texture2D(GraphicsDevice, w, h);
            NewImage.SetData(pixels);
        }

        public void GenerateResultsImage()
        {
            int w = GoodImage.Width;
            int h = GoodImage.Height;
            int n = w * h;

            // Snag the byte data from the Teture2D and place it in a large Color array
            Color[] goodImageData = new Color[n];
            Color[] newImageData = new Color[n];
            GoodImage.GetData(goodImageData);
            NewImage.GetData(newImageData);

            Color[] resultsImageData = new Color[n];

            // Generate a pixel color to use to overlay ontop
            Color missingHighlightColor = Color.Magenta;
            var missingHighlightColorPacked = missingHighlightColor.PackedValue;

            // Loop over all the Pixel Color Data from the array and compare agains the other image
            for (int i = 0; i < n; i++)
            {
                if (goodImageData[i].PackedValue != newImageData[i].PackedValue)
                {
                    resultsImageData[i].PackedValue = missingHighlightColor.PackedValue;
                }
                else
                {
                    resultsImageData[i].PackedValue = goodImageData[i].PackedValue;
                }
            }

            // Generate an image the size of the other images
            ResultsImage = new Texture2D(GraphicsDevice, GoodImage.Width, GoodImage.Height);
            ResultsImage.SetData(resultsImageData);
        }

    }
}
