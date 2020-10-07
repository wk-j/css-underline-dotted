using System;
using System.IO;
using DinkToPdf;

namespace Convert {
    class Program {
        static void White() {
            // https://qwerty.dev/whitespace/
            char c = (char)127;

            var str = $"__ __";

            foreach (var item in str) {
                Console.WriteLine($"{item} = {(int)item}");
            }
        }

        static void Main(string[] args) {

            var doc = new HtmlToPdfDocument() {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4Plus,
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = File.ReadAllText("index.html"),
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 8, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };

            var converter = new SynchronizedConverter(new PdfTools());
            var bytes = converter.Convert(doc);
            File.WriteAllBytes("__output__.pdf", bytes);
        }
    }
}