using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using Pet_store.Models;
using Pet_store.Resources.Base;
using System.Collections.Generic;
using System.Linq;

namespace Pet_store.Resources
{
    public class DocumentCheck : DocumentBase
    {
        private Order _order;
        public DocumentCheck(string path, Order order) : base(path) => _order = order;

        public override void BeginDocumentCreation()
        {
            List<(XWPFParagraph paragraph, CT_Bookmark bookmark)> marks = GetBookmarks();

            FillDocumentPlaceholders(marks, _order);

            SaveDocument();
        }

        private void FillDocumentPlaceholders(List<(XWPFParagraph paragraph, CT_Bookmark bookmark)> marks, Order order)
        {
            foreach (var mark in marks)
                switch (mark.bookmark.name)
                {
                    case "IdOrder":
                        mark.paragraph.ReplaceText("{Id Order}", order.Id.ToString());
                        break;

                    case "DateOfOrder":
                        mark.paragraph.ReplaceText("{Date of Order}", order.DateOfOrder.ToString());
                        break;

                    case "Customer":
                        mark.paragraph.ReplaceText("{Customer}", order.FullNameUser);
                        break;

                    case "TotalSum":
                        mark.paragraph.ReplaceText("{TotalSum}", $"{order.Price:0,00}");
                        break;

                    case "ProductName":
                        InsertInfoAboutProductName(mark.paragraph, order);
                        break;

                    case "Count":
                        InsertInfoAboutProductCount(mark.paragraph, order);
                        break;

                    case "Price":
                        InsertInfoAboutProductCost(mark.paragraph, order);
                        break;
                }
        }

        #region Функции заполнения табличных данных.

        private void InsertInfoAboutProductName(XWPFParagraph paragraph, Order order)
        {
            foreach (Product product in order.Baskets.Select(b => b.IdProductNavigation))
            {
                XWPFRun run = paragraph.CreateRun();
                run.SetFontFamily("Courier New", FontCharRange.HAnsi);
                run.FontSize = 14;
                run.SetText(product.Name);

                if (product.Id != order.Baskets.Select(b => b.IdProductNavigation).LastOrDefault()?.Id)
                    run.AddBreak(BreakType.TEXTWRAPPING);
            }
        }

        private void InsertInfoAboutProductCount(XWPFParagraph paragraph, Order order)
        {
            foreach (Product product in order.Baskets.Select(b => b.IdProductNavigation))
            {
                Basket basket = order.Baskets.FirstOrDefault(b => b.IdProduct == product.Id);

                XWPFRun run = paragraph.CreateRun();
                run.SetFontFamily("Courier New", FontCharRange.Ascii);
                run.FontSize = 14;
                run.SetText(basket?.Count.ToString() ?? "Неизвестно");

                if (product.Id != order.Baskets.Select(b => b.IdProductNavigation).LastOrDefault()?.Id)
                    run.AddBreak(BreakType.TEXTWRAPPING);
            }
        }

        private void InsertInfoAboutProductCost(XWPFParagraph paragraph, Order order)
        {
            foreach (Product product in order.Baskets.Select(b => b.IdProductNavigation))
            {
                XWPFRun run = paragraph.CreateRun();
                run.SetFontFamily("Courier New", FontCharRange.Ascii);
                run.FontSize = 14;
                run.SetText(product.Price.ToString("0,00"));

                if (product.Id != order.Baskets.Select(b => b.IdProductNavigation).LastOrDefault()?.Id)
                    run.AddBreak(BreakType.TEXTWRAPPING);
            }
        }
        #endregion
    }
}
