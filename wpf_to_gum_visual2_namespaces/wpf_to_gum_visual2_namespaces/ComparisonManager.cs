using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RenderingLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Runtime.InteropServices.Marshalling;
using System.Linq;
using RenderingLibrary; // for the .Any


namespace wpf_to_gum_visual2_namespaces
{
    public class ComparisonManager
    {
        public List<ComparisonItem> ComparisonItems { get; set; }
        public Texture2D GoodImage { get; set; }
        public Texture2D NewImage { get; set; }
        public Texture2D ResultsImage { get; set; }

        public List<ComparisonItem> MovedComparisonItems { get; set; }

        GraphicsDevice GraphicsDevice { get; set; }

        public ComparisonManager(): this(null, null)
        {

        }

        public ComparisonManager(GraphicsDevice graphicsDevice) : this(graphicsDevice, null)
        {

        }

        public ComparisonManager(GraphicsDevice graphicsDevice, Texture2D goodImage)
        {
            GraphicsDevice = graphicsDevice;
            GoodImage = goodImage;
            ComparisonItems = new List<ComparisonItem>();
            MovedComparisonItems = new List<ComparisonItem>();
        }

        public void BuildComparisonItemsList(InteractiveGue root)
        {
            foreach (var child in root.Children)
            {
                BuildComparisonItemsRecusrive((IRenderableIpso)child, nameof(InteractiveGue), root.Name);
            }
        }


        string[] IgnoreTypes = { "InteractiveGue" };
        private void BuildComparisonItemsRecusrive(IRenderableIpso element, string typePath, string namePath)
        {
            string newTypePath = typePath;
            string newNamePath = namePath;

            bool containsAny = IgnoreTypes.Any(opt => ! element.GetType().Name.Contains(opt, StringComparison.OrdinalIgnoreCase));
            if (containsAny)
            {
                ComparisonItem item = new ComparisonItem();
                item.Bounds = new Rectangle((int)element.GetAbsoluteX(), (int)element.GetAbsoluteY(), (int)element.Width, (int)element.Height);
                item.Name = element.Name;

                newTypePath = typePath + "/" + element.GetType().Name;
                item.TypePath = newTypePath;

                newNamePath = namePath + "/" + element.Name;
                item.NamePath = newNamePath;

                ComparisonItems.Add(item);
            }

                foreach (var child in element.Children)
                {
                    BuildComparisonItemsRecusrive((IRenderableIpso)child, newTypePath, newNamePath);
                }
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

            // Loop over all the Pixel Color Data from the array and compare against the other image
            for (int i = 0; i < n; i++)
            {
                if (goodImageData[i].PackedValue != newImageData[i].PackedValue)
                {
                    resultsImageData[i].PackedValue = missingHighlightColor.PackedValue;
                    int y = (int)(i / w);
                    int x = (int)(i % w);

                    var foundItems = FindMovedElement(new Vector2(x, y));
                    if (foundItems != null && foundItems.Count > 0)
                    {
                        foreach (var item in foundItems)
                        {
                            if (!MovedComparisonItems.Contains(item))
                            {
                                MovedComparisonItems.Add(item);
                            }
                        }
                    }
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

        public List<ComparisonItem> FindMovedElement(Vector2 point)
        {
            List<ComparisonItem> found = new List<ComparisonItem>();
            foreach(var item in ComparisonItems)
            {
                if (item.Bounds.Contains(point))
                {
                    found.Add(item);
                }
            }
            return found;
        }

        // Converter so System.Text.Json knows how to handle MonoGame's Rectangle
        public sealed class RectangleConverter : JsonConverter<Rectangle>
        {
            public override Rectangle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                using var doc = JsonDocument.ParseValue(ref reader);
                var e = doc.RootElement;
                return new Rectangle(
                    e.GetProperty("X").GetInt32(),
                    e.GetProperty("Y").GetInt32(),
                    e.GetProperty("Width").GetInt32(),
                    e.GetProperty("Height").GetInt32()
                );
            }

            public override void Write(Utf8JsonWriter writer, Rectangle value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();
                writer.WriteNumber("X", value.X);
                writer.WriteNumber("Y", value.Y);
                writer.WriteNumber("Width", value.Width);
                writer.WriteNumber("Height", value.Height);
                writer.WriteEndObject();
            }
        }


        static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true,
            Converters = { new RectangleConverter() }
        };

        public void SaveMovedList(string path)
        {
            var json = JsonSerializer.Serialize(MovedComparisonItems, _jsonOptions);
            File.WriteAllText(path, json);
        }

        public void SaveComparisonList(string path)
        {
            var json = JsonSerializer.Serialize(ComparisonItems, _jsonOptions);
            File.WriteAllText(path, json);
        }

        public void LoadComparisonList(string path)
        {
            if (!File.Exists(path))
            {
                ComparisonItems = new List<ComparisonItem>();
                return;
            }

            var json = File.ReadAllText(path);
            ComparisonItems = JsonSerializer.Deserialize<List<ComparisonItem>>(json, _jsonOptions)
                                ?? new List<ComparisonItem>();
        }


    }
}
